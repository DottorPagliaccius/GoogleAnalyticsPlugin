using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public abstract class AbstractGoogleAnalytics : IGoogleAnalytics
    {
        public abstract void TrackPage(string pageName);
        public abstract void TrackEvent(string category, string action, string label, long value);
        public abstract void Report(string message, Severity warningLevel = Severity.Warning);

        public void Report(Exception exception, Severity warningLevel = Severity.Warning)
        {
            Report(FormatException(exception), warningLevel);
        }

        public void Report(Exception exception, IDictionary<string, string> extraData, Severity warningLevel = Severity.Warning)
        {
            var message = FormatException(exception);

            if (extraData != null || extraData.Count > 0)
            {
                var stringBuilder = new StringBuilder(message);

                stringBuilder.AppendLine();
                stringBuilder.AppendLine("******** User Data ********");
                stringBuilder.AppendLine();

                foreach (var data in extraData)
                {
                    stringBuilder.AppendLine($"- {data.Key} : {data.Value}");
                }

                message = stringBuilder.ToString();
            }

            Report(message, warningLevel);
        }

        public static string FormatException(Exception exception)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Type {exception.GetType()}");
            builder.AppendLine($"Message {exception.Message}");
            builder.AppendLine($"{exception.StackTrace}");

            if (exception.InnerException != null)
                builder.AppendLine(FormatException(exception.InnerException));

            return builder.ToString();
        }
    }
}
