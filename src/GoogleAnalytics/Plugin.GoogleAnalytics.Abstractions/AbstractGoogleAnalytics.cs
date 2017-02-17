using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.GoogleAnalytics.Abstractions
{
    public abstract class AbstractGoogleAnalytics : IGoogleAnalytics
    {
        public abstract bool EnableAdvertisingTracking { get; set; }

        public abstract void TrackPage(string pageName);
        public abstract void TrackEvent(string category, string action, string label, long value);
        public abstract void Report(string message, bool isFatal = false);

        public void Report(Exception exception, bool isFatal = false)
        {
            Report(FormatException(exception), isFatal);
        }

        public void Report(Exception exception, IDictionary<string, string> extraData, bool isFatal = false)
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

            Report(message, isFatal);
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
