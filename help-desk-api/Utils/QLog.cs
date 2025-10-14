using Serilog.Sinks.Elasticsearch;
using Serilog;
using Models.AppSettings;
using Utils.SlackUtil;

namespace Utils
{
    public static class QLog
    {
        private static readonly ILogger _logger;
        private static readonly string _logFormatPrefix = $"qsmart-{AppSettings.Env.ToLower()}";
        static QLog()
        {
            _logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console() // Optionally log to console
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(AppSettings.ELKConfiguration.Uri))
                    {

                        AutoRegisterTemplate = true, // Automatically create index template in Elasticsearch
                        IndexFormat = _logFormatPrefix+"-logs-{0:yyyy.MM.dd}", // index format
                        ModifyConnectionSettings = connectionConfiguration =>
                            connectionConfiguration.BasicAuthentication(AppSettings.ELKConfiguration.Username, AppSettings.ELKConfiguration.Password)
                    })
                    .CreateLogger();
        }

        /// <summary>
        /// Will log Background process exceptions.
        /// </summary>
        /// <param name="featureName">Feature like: Quotation, Variation, Customer, CRM, BER, Upgrade Plan, Estimate, PDFGen, Accounts etc.</param>
        /// <param name="methodName">Name of the Background process method. Just send this value using nameof{YourMethodname}</param>
        /// <param name="additionalInfo">Paramters received by the method where error occured</param>
        /// <param name="ex">Exception object</param>
        public static void Error(string featureName, string methodName, string additionalInfo, Exception ex)
        {
            string formattedAdditionalInfo = string.IsNullOrWhiteSpace(additionalInfo) ? string.Empty : $", Additional Info: {additionalInfo}";
            var errorType = "Background Process";

            _logger.Error(ex, "An error occurred in a {ErrorType} of {FeatureName} at {methodName}. Error {ErrorMessage} Additional Info: {AdditionalInfo}",
            errorType, featureName, methodName, ex.Message, additionalInfo);

            //Sending Notification to Slack
            if (AppSettings.Env == "PROD" || AppSettings.Env == "TEST")
                _ = Task.Run(() =>
                    new SlackLogging(
                        AppSettings.Env == "PROD" ? "Background Process Error from Live" : "Background Process Error from Test",
                        $"Feature {featureName}, Method ${methodName} {formattedAdditionalInfo}",
                        ex?.Message ?? "Unknown error"
                    ).SendAsync());
        }

        public static void Info(string logMessage, string featureName)
        {
            _logger.Information("Feature: {FeatureName} - Message: {LogMessage}", featureName, logMessage);
        }

        /// <summary>
        /// Will log API exceptions, Don't call this method directly. It will only be used by the GlobalExceptionHandler.
        /// </summary>
        internal static void Error(Exception ex, string errorMessage, string featureName, string requestPath, string additionalInfo = null)
        {
            var errorType = "API";
            _logger.Error(ex, "An error occurred in a {ErrorType} of {FeatureName} at {RequestPath}. Error {ErrorMessage} Additional Info: {AdditionalInfo}",
            errorType, featureName, requestPath, errorMessage, additionalInfo);
        }
    }
}
