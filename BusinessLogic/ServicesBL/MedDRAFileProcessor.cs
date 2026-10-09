using BusinessLogic.DTOs.MedDRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class MedDRAFileProcessor
    {
        private readonly string _medAsciiPath;

        public MedDRAFileProcessor(string medAsciiPath)
        {
            _medAsciiPath = medAsciiPath ?? throw new ArgumentNullException(nameof(medAsciiPath));

            if (!Directory.Exists(_medAsciiPath))
            {
                throw new DirectoryNotFoundException($"MedAscii directory not found: {_medAsciiPath}");
            }
        }

        // Parse LLT.asc file
        public List<LLTRecord> ParseLLTFile()
        {
            var lltPath = Path.Combine(_medAsciiPath, "llt.asc");

            if (!File.Exists(lltPath))
            {
                throw new FileNotFoundException($"LLT file not found: {lltPath}");
            }

            var records = new List<LLTRecord>();
            var lines = File.ReadAllLines(lltPath, Encoding.UTF8);

            foreach (var line in lines)
            {
                var fields = line.Split('$');

                if (fields.Length >= 11)
                {
                    records.Add(new LLTRecord
                    {
                        LLT_Code = fields[0],
                        LLT_Name = fields[1],
                        PT_Code = fields[2], // This is the key field for linking
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

        // Parse mdhier.asc file
        public List<MDHierRecord> ParseMDHierFile()
        {
            var mdHierPath = Path.Combine(_medAsciiPath, "mdhier.asc");

            if (!File.Exists(mdHierPath))
            {
                throw new FileNotFoundException($"MDHier file not found: {mdHierPath}");
            }

            var records = new List<MDHierRecord>();
            var lines = File.ReadAllLines(mdHierPath, Encoding.UTF8);

            foreach (var line in lines)
            {
                var fields = line.Split('$');

                if (fields.Length >= 12)
                {
                    records.Add(new MDHierRecord
                    {
                        PT_Code = fields[0], // Key field for linking with LLT
                        HLT_Code = fields[1],
                        HLGT_Code = fields[2],
                        SOC_Code = fields[3],
                        PT_Name = fields[4],
                        HLT_Name = fields[5],
                        HLGT_Name = fields[6],
                        SOC_Name = fields[7],
                        SOC_Abbrev = fields[8],
                        PT_SOC_Code = fields[9],
                        Primary_SOC_Flag = fields[11] // Column 12 (0-indexed as 11)
                    });
                }
            }

            return records;
        }

        // Build complete hierarchy by linking LLT and MDHier files
        public List<MedDRAHierarchy> BuildCompleteHierarchy(bool primarySOCOnly = true)
        {
            var lltRecords = ParseLLTFile();
            var mdHierRecords = ParseMDHierFile();

            // Filter for primary SOC only if requested
            if (primarySOCOnly)
            {
                mdHierRecords = mdHierRecords.Where(m => m.Primary_SOC_Flag == "Y").ToList();
            }

            // Join the records on PT_Code
            var hierarchy = (from llt in lltRecords
                             join hier in mdHierRecords on llt.PT_Code equals hier.PT_Code
                             select new MedDRAHierarchy
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

        // Search terms in the hierarchy
        public List<MedDRAHierarchy> SearchTerms(string searchTerm, bool primarySOCOnly = true)
        {
            var hierarchy = BuildCompleteHierarchy(primarySOCOnly);

            return hierarchy.Where(h =>
                h.LLT_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                h.PT_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                h.HLT_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                h.HLGT_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                h.SOC_Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        // Get terms by SOC
        public List<MedDRAHierarchy> GetTermsBySOC(string socCode, bool primarySOCOnly = true)
        {
            var hierarchy = BuildCompleteHierarchy(primarySOCOnly);

            return hierarchy.Where(h => h.SOC_Code == socCode).ToList();
        }

        // Get term by specific code
        public MedDRAHierarchy GetTermByCode(string termCode, string termLevel = "LLT")
        {
            var hierarchy = BuildCompleteHierarchy(false);

            return termLevel.ToUpper() switch
            {
                "LLT" => hierarchy.FirstOrDefault(h => h.LLT_Code == termCode),
                "PT" => hierarchy.FirstOrDefault(h => h.PT_Code == termCode),
                "HLT" => hierarchy.FirstOrDefault(h => h.HLT_Code == termCode),
                "HLGT" => hierarchy.FirstOrDefault(h => h.HLGT_Code == termCode),
                "SOC" => hierarchy.FirstOrDefault(h => h.SOC_Code == termCode),
                _ => throw new ArgumentException($"Invalid term level: {termLevel}")
            };
        }
    }

}
