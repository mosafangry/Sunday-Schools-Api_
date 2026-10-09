using BusinessLogic.IServicesBL;
using DataAccess.Entities.FdaValidation;
using DataAccess.IRepository;
using DataAccess.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using BusinessLogic.Services;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Edge;

namespace BusinessLogic.ServicesBL
{
    public class ScraperService : IScraperService
    {
        private readonly IScrapingRepository _scrapingRepository;
        private readonly ILogger<ScraperService> _logger;

        public ScraperService(IScrapingRepository scrapingRepository, ILogger<ScraperService> logger)
        {
            _scrapingRepository = scrapingRepository;
            _logger = logger;
        }
        //"--headless"
        public async Task<FDAValidationResult> ScrapeAndValidateAsync(IchIcsrXMLModel model)
        {
            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("--headless", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage");

            //using var driver =new ChromeDriver(chromeOptions);// 
            var chromeOptions = new EdgeOptions();
            // chromeOptions.AddArguments("--headless", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage");
            chromeOptions.AddArguments("--disable-gpu", "--headless", "--start-maximized", "--ignore-certificate-errors", "--no-sandbox", "--disable-dev-shm-usage");

            using var driver = new EdgeDriver(@"C:\WebDriver\msedgedriver.exe", chromeOptions);// ConfigureChrome();
            var response = new FDAValidationResult();
            response.SafetyReportId = model.SafetyReport.SafetyReportId;
            try
            {
                var xmlContent = SerializeToXml(model);
                var url = @"https://faers2-validator.preprod.fda.gov/LSMV/Validator?readStatus=1";
                driver.Navigate().GoToUrl(url);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("document.getElementById('outputSrc').value = arguments[0];", xmlContent);

                var validateButton = driver.FindElement(By.Id("btnValidate"));
                validateButton.Click();

                Thread.Sleep(2000);

                // Safe check: does collapseTwo exist?
                IWebElement? statusElement = null;
                try
                {
                    var collapseDiv = driver.FindElement(By.Id("collapseTwo"));
                    statusElement = collapseDiv.FindElements(By.TagName("h5")).FirstOrDefault();
                }
                catch (NoSuchElementException)
                {
                    // Element not found — proceed with defaults
                }

                response.StatusText = statusElement?.Text.Trim() ?? "";

                // If valid XML format, skip table extraction
                if (!string.IsNullOrEmpty(response.StatusText))
                {
                    return response;
                }

                // Table might not exist either — handle safely
                var tableRows = driver.FindElements(By.XPath("//*[@id='validationTable']/tbody/tr"));
                foreach (var row in tableRows)
                {
                    var cols = row.FindElements(By.TagName("td"));
                    if (cols.Count < 5) continue;

                    response.Errors.Add(new FDAValidationErrors
                    {
                        TagId = cols[0].Text.Trim(),
                        TagLabel = cols[1].Text.Trim(),
                        OriginalXmlValue = cols[2].Text.Trim(),
                        Severity = cols[3].Text.Trim(),
                        Message = cols[4].Text.Trim()
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ICSR with safety report");

                response.StatusText = "Error: " + ex.Message;
            }
            finally
            {
                await _scrapingRepository.AddAsync(response);
                driver.Quit();
            }
            return response;
        }
        private string SerializeToXml(IchIcsrXMLModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            try
            {
                // Create XML serializer for the IchIcsrXMLModel type
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(IchIcsrXMLModel));

                // Create XML writer settings to control formatting
                var settings = new XmlWriterSettings
                {
                    Indent = true,             // Add indentation
                    IndentChars = "\t",        // Use tabs for indentation
                    Encoding = Encoding.UTF8,  // Use UTF-8 encoding
                    OmitXmlDeclaration = false // Include XML declaration
                };

                // Use StringBuilder to build the XML string
                using (var stringWriter = new StringWriter())
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    // Add XML declaration manually to ensure proper encoding specification
                    xmlWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    // Create a blank namespace to avoid default namespaces
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    // Serialize the model to XML
                    serializer.Serialize(xmlWriter, model, ns);

                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error serializing IchIcsrXMLModel to XML: {ex.Message}", ex);
            }
        }
        private IWebDriver ConfigureChrome()
        {
            try
            {
                // Full path to ChromeDriver and Portable Chrome
                string chromeDriverPath = @"D:\New folder (2)\Pharma_20250518\Pharmacovigilance\Tenders\ChromeDriver";
                string chromePath = @"D:\New folder (2)\Pharma_20250518\Pharmacovigilance\Tenders\Chrome\GoogleChromePortable.exe";

                if (!File.Exists(chromePath))
                    throw new FileNotFoundException($"Portable Chrome not found at {chromePath}");

                var options = new ChromeOptions();

                // Must be set when using portable Chrome
                options.BinaryLocation = chromePath;

                // Headless is required for IIS/server"--headless=new",
                options.AddArguments( "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage");

                var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
                service.HideCommandPromptWindow = true;

                return new ChromeDriver(service, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw;
            }
        }

        private IWebDriver ConfigureChrome_6()
        {
            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string chromeDriverPath = Path.Combine(appDirectory, "ChromeDriver");
                string chromePath = Path.Combine(appDirectory, "Chrome", "GoogleChromePortable.exe");

                if (!File.Exists(chromePath))
                {
                    throw new FileNotFoundException($"Chrome portable executable not found at: {chromePath}");
                }

                var options = new ChromeOptions();
                options.BinaryLocation = chromePath;

                // Required for headless (IIS/non-GUI environments)
                options.AddArguments("--headless=new", "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage");

                var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
                service.HideCommandPromptWindow = true;

                return new ChromeDriver(service, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                throw;
            }
        }

        private IWebDriver ConfigureChrome_5()
        {
            try
            {
                // KillExistingChromeProcesses();
                // Define paths relative to your application
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string chromeDriverPath = Path.Combine(appDirectory, "ChromeDriver");
                string chromePath = Path.Combine(appDirectory, "Chrome", "GoogleChromePortable.exe");

                // Verify paths exist
                if (!File.Exists(chromePath))
                {
                    throw new FileNotFoundException($"Chrome portable executable not found at: {chromePath}");
                }

                var options = new ChromeOptions();
                options.BinaryLocation = chromePath;

                options.AddArguments("--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage");
                // Additional options for better stability
                //options.AddArgument("--disable-extensions");
                //options.AddArgument("--no-sandbox");
                //options.AddArgument("--disable-dev-shm-usage");
                //options.AddArgument("--disable-gpu");
                //options.AddArgument("--disable-popup-blocking");
                //options.AddArgument("--disable-notifications");

                // Create and configure ChromeDriver service
                var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
                service.HideCommandPromptWindow = true;

                   return new ChromeDriver(service, options);
               // return new ChromeDriver(@"C:\WebDriver\chromedriver.exe", options);
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"Error configuring Chrome: {ex.Message}");
                throw;
            }
        }
        private IWebDriver ConfigureChrome_4()
        {
            // Define paths relative to your application
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string chromeDriverPath = Path.Combine(appDirectory, "ChromeDriver");
            string chromePath = Path.Combine(appDirectory, "Chrome", "GoogleChromePortable.exe");// @"D:\New folder (2)\Pharma_20250518\Pharmacovigilance\Tenders\Chrome\GoogleChromePortable.exe";

            var options = new ChromeOptions();
            options.BinaryLocation = chromePath;
            //     options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");

            var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
            service.HideCommandPromptWindow = true;

            return new ChromeDriver(service, options);
        }
        private IWebDriver ConfigureChrome3()
        {
            try
            {
                // Clean up any existing Chrome processes that might cause conflicts
                try
                {
                    foreach (var process in System.Diagnostics.Process.GetProcessesByName("chrome"))
                    {
                        try { process.Kill(); } catch { /* Best effort */ }
                    }
                    foreach (var process in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
                    {
                        try { process.Kill(); } catch { /* Best effort */ }
                    }
                }
                catch { /* Continue even if cleanup fails */ }

                // Define paths relative to your application
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string chromeDriverPath = Path.Combine(appDirectory, "ChromeDriver");
                string chromePath = Path.Combine(appDirectory, "Chrome", "chrome.exe");

                // Create a unique temporary directory for user data
                string tempUserDataDir = Path.Combine(Path.GetTempPath(), "chrome_data_" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempUserDataDir);

                var options = new ChromeOptions();
                options.BinaryLocation = chromePath;

                // Add the unique user data directory argument (this solves your issue)
                options.AddArgument($"--user-data-dir={tempUserDataDir}");

                // Keep your existing arguments
                options.AddArgument("--headless");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");

                // Add some additional helpful arguments for server environments
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-browser-side-navigation");
                options.AddArgument("--disable-infobars");
                options.AddArgument("--window-size=1920,1080");

                var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
                service.HideCommandPromptWindow = true;

                // Add cleanup for resources when application stops
                AppDomain.CurrentDomain.ProcessExit += (s, e) =>
                {
                    try
                    {
                        if (Directory.Exists(tempUserDataDir))
                        {
                            Directory.Delete(tempUserDataDir, true);
                        }
                    }
                    catch
                    {
                        // Best effort cleanup
                    }
                };

                return new ChromeDriver(service, options);
            }
            catch (Exception ex)
            {
                // Log the exception for easier troubleshooting
                System.Diagnostics.Debug.WriteLine($"Chrome initialization error: {ex.Message}");
                // You might want to log this to a file or other logging system as well
                throw;
            }
        }
        private IWebDriver ConfigureChrome_()
        {
            // Set a longer timeout for driver commands (10 minutes)
            TimeSpan commandTimeout = TimeSpan.FromMinutes(10);

            // Define paths relative to your application
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string chromeDriverPath = Path.Combine(appDirectory, "ChromeDriver");
            string chromePath = Path.Combine(appDirectory, "Chrome", "chrome.exe");

            // Kill any existing chrome processes
            KillExistingChromeProcesses();

            // Create a unique temporary directory for user data
            string tempUserDataDir = Path.Combine(Path.GetTempPath(), "chrome_data_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempUserDataDir);

            // Configure Chrome options
            var options = new ChromeOptions();
            options.BinaryLocation = chromePath;
            options.AddArgument($"--user-data-dir={tempUserDataDir}");
            options.AddArgument("--headless=new");  // Use the new headless mode
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-web-security");
            options.AddArgument("--allow-running-insecure-content");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--window-size=1920,1080");

            // Add performance optimizations
            options.AddArgument("--disable-background-networking");
            options.AddArgument("--disable-background-timer-throttling");
            options.AddArgument("--disable-breakpad");
            options.AddArgument("--disable-client-side-phishing-detection");
            options.AddArgument("--disable-default-apps");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-domain-reliability");
            options.AddArgument("--disable-features=TranslateUI");
            options.AddArgument("--disable-hang-monitor");
            options.AddArgument("--disable-ipc-flooding-protection");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-prompt-on-repost");
            options.AddArgument("--disable-renderer-backgrounding");
            options.AddArgument("--disable-sync");
            options.AddArgument("--metrics-recording-only");
            options.AddArgument("--no-first-run");
            options.AddArgument("--password-store=basic");
            options.AddArgument("--use-mock-keychain");

            // Configure ChromeDriver service
            var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
            service.HideCommandPromptWindow = true;

            // Enable verbose logging for debugging
            service.LogPath = Path.Combine(appDirectory, "chromedriver.log");
            service.EnableVerboseLogging = true;

            // Add a retry mechanism for starting Chrome
            int maxRetries = 3;
            Exception lastException = null;

            for (int retry = 0; retry < maxRetries; retry++)
            {
                try
                {
                    // Clean up between retries
                    if (retry > 0)
                    {
                        System.Threading.Thread.Sleep(5000); // Wait 5 seconds before retry
                        KillExistingChromeProcesses();
                    }

                    // Create a new driver with extended timeout
                    return new ChromeDriver(service, options, commandTimeout);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    System.Diagnostics.Debug.WriteLine($"Chrome initialization failed (attempt {retry + 1}/{maxRetries}): {ex.Message}");

                    // On last retry, try without headless mode as a fallback
                    if (retry == maxRetries - 1)
                    {
                        try
                        {
                            options = new ChromeOptions();
                            options.BinaryLocation = chromePath;
                            options.AddArgument($"--user-data-dir={tempUserDataDir}");
                            options.AddArgument("--no-sandbox");
                            options.AddArgument("--disable-dev-shm-usage");

                            return new ChromeDriver(service, options, commandTimeout);
                        }
                        catch (Exception fallbackEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Chrome fallback initialization failed: {fallbackEx.Message}");
                        }
                    }
                }
            }

            // If we get here, all retries failed
            throw new InvalidOperationException("Failed to initialize Chrome after multiple attempts", lastException);
        }

        private void KillExistingChromeProcesses()
        {
            try
            {
                // Kill Chrome processes
                foreach (var process in System.Diagnostics.Process.GetProcessesByName("chrome"))
                {
                    try { process.Kill(); } catch { /* Best effort */ }
                }

                // Kill ChromeDriver processes
                foreach (var process in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
                {
                    try { process.Kill(); } catch { /* Best effort */ }
                }

                // Short delay to ensure processes are fully terminated
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error killing Chrome processes: {ex.Message}");
            }
        }
    }



}
