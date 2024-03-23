using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Twabel.CrossCutting.Extensions
{
    public interface ILoggerTiming
    {
        void Execute(Action action, [CallerFilePath] string? sourceFilePath = null, [CallerMemberName] string? methodCaller = null);
        T Execute<T>(Func<T> action, [CallerFilePath] string? sourceFilePath = null, [CallerMemberName] string? methodCaller = null);
    }

    public class LoggerTiming : ILoggerTiming
    {
        private readonly ILogger<LoggerTiming> _logger;

        public LoggerTiming(ILogger<LoggerTiming> logger)
        {
            _logger = logger;
        }

        public void Execute(Action action, [CallerFilePath] string? sourceFilePath = null, [CallerMemberName] string? methodCaller = null)
        {
            var fullCaller = GetFullCaller(sourceFilePath, methodCaller);
            var stopWatch = Stopwatch.StartNew();

            action();

            stopWatch.Stop();
            _logger.LogInformation("{IsLoggerTimer} {ElapsedTimeMs}ms {Caller}", true.ToString(), stopWatch.ElapsedMilliseconds, fullCaller);
        }

        public T Execute<T>(Func<T> action, [CallerFilePath] string? sourceFilePath = null, [CallerMemberName] string? methodCaller = null)
        {
            var fullCaller = GetFullCaller(sourceFilePath, methodCaller);
            var stopWatch = Stopwatch.StartNew();

            var result = action();

            stopWatch.Stop();
            _logger.LogInformation("{IsLoggerTimer} {ElapsedTimeMs}ms {Caller}", true.ToString(), stopWatch.ElapsedMilliseconds, fullCaller);

            return result;
        }

        private string GetFullCaller(string sourceFilePath, string methodCaller)
        {
            var filePathArray = sourceFilePath.Split('/');
            var lastPart = filePathArray[filePathArray.Length - 1];
            var fileNameArray = lastPart.Split('.');

            return $"{fileNameArray[0]}/{methodCaller}";
        }
    }
}

/*
This code defines an interface ILoggerTiming and its implementation LoggerTiming that provides timing functionality 
for logging. It has two methods:

Execute: Takes an Action and logs the time taken to execute it, along with the file path and method name where it 
was called from.
Execute<T>: Takes a Func<T> and logs the time taken to execute it, along with the file path and method name where 
it was called from.
The LoggerTiming constructor takes an instance of ILogger<LoggerTiming>, which it uses to log the execution time 
of the actions. It uses Stopwatch to measure the elapsed time of the actions and the CallerFilePath and CallerMemberName 
attributes to get the file path and method name of the calling code. The file path and method name are then concatenated 
to create a string representing the full caller information. Finally, the elapsed time and full caller information are 
logged using the logger.


*/