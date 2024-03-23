using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Twabel.CrossCutting.Exceptions;

namespace Twabel.CrossCutting.Extensions
{
    public static class LoggerExtension
    {
        public static void TwabelWarning(this ILogger logger, Exception exception, string message)
        {
            logger.LogWarning(exception, "{alert_message}", message);
        }

        public static void TwabelWarningWithJson<T>(this ILogger logger, string message, T json)
        {
            logger.LogWarning("{alert_message} {json}", message, JsonSerializer.Serialize(json).TruncateLog());
        }

        public static void TwabelInformation(this ILogger logger, string message)
        {
            logger.LogInformation("{message}", message);
        }
        public static void TwabelInformationWithJson<T>(this ILogger logger, string message, T json)
        {
            logger.LogInformation("{message} {json}", message, JsonSerializer.Serialize(json).TruncateLog());
        }

        public static void TwabelWarning(this ILogger logger, string message)
        {
            logger.LogWarning("{alert_message}", message);
        }

        public static void TwabelWarning(this ILogger logger, string message, params object[] args)
        {
            logger.LogWarning("{alert_message}", message, args);
        }

        public static void TwabelError(this ILogger logger, Exception exception, string message)
        {
            logger.LogError(exception, "{alert_message}", message);
        }

        public static void TwabelError(this ILogger logger, string message)
        {
            logger.LogError("{alert_message}", message);
        }

        public static void TwabelError(this ILogger logger, string message, params object[] args)
        {
            logger.LogError("{alert_message}", message, args);
        }

        public static void TwabelErrorUnhandledException(this ILogger logger, Exception exception)
        {
            if (!(exception is BusinessLogicException))
            {
                logger.TwabelError(exception, exception.Message);
            }
        }
    }
}
/*

These are extension methods for the ILogger interface in .NET Core to provide custom log messages with standardized 
formatting.

TwabelWarning: logs a warning message with an optional exception and a message string formatted with "{alert_message}".
TwabelWarningWithJson: logs a warning message with an optional JSON object and a message string formatted with 
"{alert_message} {json}".
TwabelInformation: logs an information message with a message string formatted with "{message}".
TwabelInformationWithJson: logs an information message with a JSON object and a message string formatted with 
"{message} {json}".
TwabelError: logs an error message with an optional exception and a message string formatted with "{alert_message}".
TwabelErrorUnhandledException: logs an error message for an unhandled exception, ignoring the exception if it is a 
BusinessLogicException.

*/

