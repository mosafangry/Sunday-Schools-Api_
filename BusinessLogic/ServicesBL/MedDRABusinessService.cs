using BusinessLogic.DTOs.MedDRA;
using BusinessLogic.IServicesBL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class MedDRAConfiguration
    {
        public string MedAsciiPath { get; set; }
        public int CacheExpirationHours { get; set; } = 24;
        public bool EnableCaching { get; set; } = true;
    }

    public class MedDRABusinessService : IMedDRAService
    {
        private readonly ILogger<MedDRABusinessService> _logger;
        private readonly MedDRAConfiguration _config;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly string _meddraFolderPath;
        // In-memory cache for better performance
        private static List<MedDRAHierarchyDto> _cachedHierarchy;
        private static DateTime _lastCacheUpdate;

        public MedDRABusinessService(
            ILogger<MedDRABusinessService> logger,
            IOptions<MedDRAConfiguration> config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config?.Value ?? throw new ArgumentNullException(nameof(config));
            _meddraFolderPath = Path.Combine(AppContext.BaseDirectory, "MedDRAFiles");

            ValidateConfiguration();
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(_meddraFolderPath))
                throw new ArgumentException("MedAsciiPath is required");

            if (!Directory.Exists(_meddraFolderPath))
                throw new DirectoryNotFoundException($"MedAscii directory not found: {_meddraFolderPath}");
        }
        public async Task<MedDRASearchResponse> SearchOrGetTermAsync(MedDRASearchUnifiedRequest request)
        {
            try
            {
                var hierarchy = await GetCachedHierarchyAsync(request.PrimarySOCOnly);

                if (string.IsNullOrWhiteSpace(request.TermCode) && string.IsNullOrWhiteSpace(request.SearchTerm))
                    throw new ArgumentException("You must provide either TermCode/TermLevel or SearchTerm.");

                var level = request.TermLevel?.ToUpper();
                if (string.IsNullOrWhiteSpace(level))
                    throw new ArgumentException("TermLevel is required when searching by TermCode or SearchTerm.");

                List<MedDRAHierarchyDto> filtered = new();

                // --- Case 1: Search by Code ---
                if (!string.IsNullOrWhiteSpace(request.TermCode))
                {
                    _logger.LogInformation("Searching MedDRA term by code: {TermCode}, Level: {TermLevel}",
                        request.TermCode, request.TermLevel);

                    filtered = level switch
                    {
                        "LLT" => hierarchy.Where(h => h.LLT_Code == request.TermCode).ToList(),
                        "PT" => hierarchy.Where(h => h.PT_Code == request.TermCode).ToList(),
                        "HLT" => hierarchy.Where(h => h.HLT_Code == request.TermCode).ToList(),
                        "HLGT" => hierarchy.Where(h => h.HLGT_Code == request.TermCode).ToList(),
                        "SOC" => hierarchy.Where(h => h.SOC_Code == request.TermCode).ToList(),
                        _ => throw new ArgumentException($"Invalid term level: {request.TermLevel}")
                    };

                    if (filtered.Count == 0)
                    {
                        _logger.LogWarning("No term found for code: {TermCode}, Level: {TermLevel}", request.TermCode, request.TermLevel);
                        return new MedDRASearchResponse { Results = [], TotalCount = 0 };
                    }

                    var paged = filtered.Take(request.MaxResults).ToList();

                    return new MedDRASearchResponse
                    {
                        Results = paged,
                        TotalCount = filtered.Count,
                        SearchTerm = request.TermCode,
                        HasMore = filtered.Count > paged.Count
                    };
                }

                // --- Case 2: Search by Name ---
                _logger.LogInformation("Searching MedDRA terms for: {SearchTerm}", request.SearchTerm);

                filtered = level switch
                {
                    "LLT" => hierarchy.Where(h => h.LLT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                    "PT" => hierarchy.Where(h => h.PT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                    "HLT" => hierarchy.Where(h => h.HLT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                    "HLGT" => hierarchy.Where(h => h.HLGT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                    "SOC" => hierarchy.Where(h => h.SOC_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList(),
                    _ => throw new ArgumentException($"Invalid term level: {request.TermLevel}")
                };

                var pagedResults = filtered.Take(request.MaxResults).ToList();

                return new MedDRASearchResponse
                {
                    Results = pagedResults,
                    TotalCount = filtered.Count,
                    SearchTerm = request.SearchTerm,
                    HasMore = filtered.Count > pagedResults.Count
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in unified MedDRA search.");
                throw;
            }
        }

        public async Task<MedDRASearchResponse> SearchTermsAsync(MedDRASearchRequest request)
        {
            try
            {
                _logger.LogInformation("Searching MedDRA terms for: {SearchTerm}", request.SearchTerm);

                var hierarchy = await GetCachedHierarchyAsync(request.PrimarySOCOnly);

                var results = hierarchy.Where(h =>
                    h.LLT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    h.PT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    h.HLT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    h.HLGT_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                    h.SOC_Name.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                var pagedResults = results.Take(request.MaxResults).ToList();

                return new MedDRASearchResponse
                {
                    Results = pagedResults,
                    TotalCount = results.Count,
                    SearchTerm = request.SearchTerm,
                    HasMore = results.Count > request.MaxResults
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching MedDRA terms for: {SearchTerm}", request.SearchTerm);
                throw;
            }
        }

        public async Task<MedDRAHierarchyDto> GetTermByCodeAsync(MedDRATermRequest request)
        {
            try
            {
                _logger.LogInformation("Getting MedDRA term by code: {TermCode}, Level: {TermLevel}",
                    request.TermCode, request.TermLevel);

                var hierarchy = await GetCachedHierarchyAsync(false);

                var result = request.TermLevel.ToUpper() switch
                {
                    "LLT" => hierarchy.FirstOrDefault(h => h.LLT_Code == request.TermCode),
                    "PT" => hierarchy.FirstOrDefault(h => h.PT_Code == request.TermCode),
                    "HLT" => hierarchy.FirstOrDefault(h => h.HLT_Code == request.TermCode),
                    "HLGT" => hierarchy.FirstOrDefault(h => h.HLGT_Code == request.TermCode),
                    "SOC" => hierarchy.FirstOrDefault(h => h.SOC_Code == request.TermCode),
                    _ => throw new ArgumentException($"Invalid term level: {request.TermLevel}")
                };

                if (result == null)
                {
                    _logger.LogWarning("Term not found: {TermCode}, Level: {TermLevel}",
                        request.TermCode, request.TermLevel);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting MedDRA term by code: {TermCode}", request.TermCode);
                throw;
            }
        }

        public async Task<MedDRASearchResponse> GetTermsBySOCAsync(MedDRASOCRequest request)
        {
            try
            {
                _logger.LogInformation("Getting MedDRA terms by SOC: {SOCCode}", request.SOCCode);

                var hierarchy = await GetCachedHierarchyAsync(request.PrimarySOCOnly);
                var results = hierarchy.Where(h => h.SOC_Code == request.SOCCode).ToList();

                return new MedDRASearchResponse
                {
                    Results = results,
                    TotalCount = results.Count,
                    SearchTerm = request.SOCCode,
                    HasMore = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting MedDRA terms by SOC: {SOCCode}", request.SOCCode);
                throw;
            }
        }

        public async Task<List<string>> GetAllSOCsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all SOCs");

                var hierarchy = await GetCachedHierarchyAsync(true);
                return hierarchy.Select(h => $"{h.SOC_Code} - {h.SOC_Name}")
                              .Distinct()
                              .OrderBy(s => s)
                              .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all SOCs");
                throw;
            }
        }

        public async Task<bool> ValidateTermCodeAsync(string termCode, string termLevel)
        {
            try
            {
                var request = new MedDRATermRequest { TermCode = termCode, TermLevel = termLevel };
                var result = await GetTermByCodeAsync(request);
                return result != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating term code: {TermCode}", termCode);
                return false;
            }
        }

        // New method to detect signals for a medication
        public async Task<List<MedicationSignalDto>> DetectMedicationSignalAsync(string medicationName)
        {
            _logger.LogInformation("Detecting signals for medication: {MedicationName}", medicationName);

            // Get MedDRA hierarchy
            var hierarchy = await GetCachedHierarchyAsync(primarySOCOnly: true);

            // Simulate adverse event data (replace with actual data source)
            var adverseEvents = LoadAdverseEventData(); // Placeholder: Load from database or file

            // Filter events for the specified medication
            var drugEvents = adverseEvents.Where(e => e.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase)).ToList();
            var otherEvents = adverseEvents.Where(e => !e.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase)).ToList();

            var signals = new List<MedicationSignalDto>();
            var ptGroups = drugEvents.GroupBy(e => e.PT_Code).ToList();

            foreach (var ptGroup in ptGroups)
            {
                var ptCode = ptGroup.Key;
                var ptInfo = hierarchy.FirstOrDefault(h => h.PT_Code == ptCode);
                if (ptInfo == null) continue;

                // Calculate PRR
                int a = ptGroup.Count(); // Drug + PT
                int b = drugEvents.Count - a; // Drug + Other PTs
                int c = otherEvents.Count(e => e.PT_Code == ptCode); // Other Drugs + PT
                int d = otherEvents.Count - c; // Other Drugs + Other PTs

                if (a < 3) continue; // Minimum case threshold
                if (b == 0 || c == 0 || d == 0) continue; // Avoid division by zero

                double prr = ((double)a / (a + b)) / ((double)c / (c + d));
                if (prr >= 2.0) // Signal threshold
                {
                    signals.Add(new MedicationSignalDto
                    {
                        PT_Code = ptCode,
                        PT_Name = ptInfo.PT_Name,
                        SOC_Name = ptInfo.SOC_Name,
                        CaseCount = a,
                        PRR = Math.Round(prr, 2),
                        SignalDetected = true
                    });
                }
            }

            _logger.LogInformation("Detected {Count} signals for {MedicationName}", signals.Count, medicationName);
            return signals.OrderByDescending(s => s.PRR).ToList();
        }

        // Placeholder for loading adverse event data (replace with actual implementation)
        private List<AdverseEventRecord> LoadAdverseEventData()
        {
            // Example: Replace with actual database query or file parsing
            return new List<AdverseEventRecord>
        {
            new AdverseEventRecord { MedicationName = "DrugA", PT_Code = "10007541" }, // Example PT: Cardiac arrest
            new AdverseEventRecord { MedicationName = "DrugA", PT_Code = "10007541" },
            new AdverseEventRecord { MedicationName = "DrugA", PT_Code = "10007541" },
            new AdverseEventRecord { MedicationName = "DrugB", PT_Code = "10018065" }, // Example PT: Headache
            new AdverseEventRecord { MedicationName = "DrugA", PT_Code = "10018065" }
        };
        }
        private async Task<List<MedDRAHierarchyDto>> GetCachedHierarchyAsync(bool primarySOCOnly)
        {
            if (_config.EnableCaching &&
                _cachedHierarchy != null &&
                DateTime.Now - _lastCacheUpdate < TimeSpan.FromHours(_config.CacheExpirationHours))
            {
                return primarySOCOnly ?
                    _cachedHierarchy.Where(h => h.IsPrimarySOC).ToList() :
                    _cachedHierarchy;
            }

            await _semaphore.WaitAsync();
            try
            {
                // Double-check pattern
                if (_config.EnableCaching &&
                    _cachedHierarchy != null &&
                    DateTime.Now - _lastCacheUpdate < TimeSpan.FromHours(_config.CacheExpirationHours))
                {
                    return primarySOCOnly ?
                        _cachedHierarchy.Where(h => h.IsPrimarySOC).ToList() :
                        _cachedHierarchy;
                }

                _logger.LogInformation("Building MedDRA hierarchy cache");
                _cachedHierarchy = await Task.Run(() => BuildCompleteHierarchy(false));
                _lastCacheUpdate = DateTime.Now;

                _logger.LogInformation("Cache updated with {Count} records", _cachedHierarchy.Count);
            }
            finally
            {
                _semaphore.Release();
            }

            return primarySOCOnly ?
                _cachedHierarchy.Where(h => h.IsPrimarySOC).ToList() :
                _cachedHierarchy;
        }

        private List<MedDRAHierarchyDto> BuildCompleteHierarchy(bool primarySOCOnly)
        {
            var lltRecords = ParseLLTFile();
            var mdHierRecords = ParseMDHierFile();

            if (primarySOCOnly)
            {
                mdHierRecords = mdHierRecords.Where(m => m.Primary_SOC_Flag == "Y").ToList();
            }

            var hierarchy = (from llt in lltRecords
                             join hier in mdHierRecords on llt.PT_Code equals hier.PT_Code
                             select new MedDRAHierarchyDto
                             {
                                 LLT_Code = llt.LLT_Code,
                                 LLT_Name = llt.LLT_Name,
                                 PT_Code = hier.PT_Code,
                                 PT_Name = hier.PT_Name,
                                 HLT_Code = hier.HLT_Code,
                                 HLT_Name = hier.HLT_Name,
                                 HLGT_Code = hier.HLGT_Code,
                                 HLGT_Name = hier.HLGT_Name,
                                 SOC_Code = hier.SOC_Code,
                                 SOC_Name = hier.SOC_Name,
                                 IsPrimarySOC = hier.Primary_SOC_Flag == "Y"
                             }).ToList();

            return hierarchy;
        }

        private List<LLTRecord> ParseLLTFile()
        {

            var lltPath = Path.Combine(_meddraFolderPath, "llt.asc");
            if (!File.Exists(lltPath))
                throw new FileNotFoundException($"LLT file not found: {lltPath}");

            var records = new List<LLTRecord>();
            var lines = File.ReadAllLines(lltPath, System.Text.Encoding.UTF8);

            foreach (var line in lines)
            {
                var fields = line.Split('$');
                if (fields.Length >= 11)
                {
                    records.Add(new LLTRecord
                    {
                        LLT_Code = fields[0],
                        LLT_Name = fields[1],
                        PT_Code = fields[2],
                        LLT_Whoart_Code = fields[3],
                        LLT_Harts_Code = fields[4],
                        LLT_Costart_Sym = fields[5],
                        LLT_Icd9_Code = fields[6],
                        LLT_Icd9cm_Code = fields[7],
                        LLT_Icd10_Code = fields[8],
                        LLT_Currency = fields[9],
                        LLT_Jart_Code = fields[10]
                    });
                }
            }

            return records;
        }

        private List<MDHierRecord> ParseMDHierFile()
        {
            var mdHierPath = Path.Combine(_meddraFolderPath, "mdhier.asc");
            if (!File.Exists(mdHierPath))
                throw new FileNotFoundException($"MDHier file not found: {mdHierPath}");

            var records = new List<MDHierRecord>();
            var lines = File.ReadAllLines(mdHierPath, System.Text.Encoding.UTF8);

            foreach (var line in lines)
            {
                var fields = line.Split('$');
                if (fields.Length >= 12)
                {
                    records.Add(new MDHierRecord
                    {
                        PT_Code = fields[0],
                        HLT_Code = fields[1],
                        HLGT_Code = fields[2],
                        SOC_Code = fields[3],
                        PT_Name = fields[4],
                        HLT_Name = fields[5],
                        HLGT_Name = fields[6],
                        SOC_Name = fields[7],
                        SOC_Abbrev = fields[8],
                        PT_SOC_Code = fields[9],
                        Primary_SOC_Flag = fields[11]
                    });
                }
            }

            return records;
        }
    }
}
