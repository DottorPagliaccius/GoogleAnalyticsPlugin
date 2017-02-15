using System;
using Foundation;
using Google.Analytics;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Implementation for GoogleAnalytics
    /// </summary>
    public class GoogleAnalyticsImplementation : AbstractGoogleAnalytics
    {
        private const string AllowTrackingKey = "AllowTracking";

        private static ITracker _tracker;

        public GoogleAnalyticsImplementation()
        {
            if (_tracker == null)
                CrossGoogleAnalytics.NotInitialized();
        }

        public static void Init(string trackerName, string trackingId)
        {
            if (trackingId == null)
                throw new ArgumentNullException(nameof(trackingId));

            var optionsDict = NSDictionary.FromObjectAndKey(new NSString("YES"), new NSString(AllowTrackingKey));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(optionsDict);

            Gai.SharedInstance.OptOut = !NSUserDefaults.StandardUserDefaults.BoolForKey(AllowTrackingKey);
            Gai.SharedInstance.DispatchInterval = 10;
            Gai.SharedInstance.TrackUncaughtExceptions = true;

            _tracker = Gai.SharedInstance.GetTracker(trackerName, trackingId);
        }

        public override void Report(string message, Severity warningLevel = Severity.Warning)
        {
            _tracker.Send(DictionaryBuilder.CreateException(message, warningLevel != Severity.Warning).Build());
        }

        public override void TrackPage(string pageName)
        {
            Gai.SharedInstance.DefaultTracker.Set(GaiConstants.ScreenName, pageName);
            Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateScreenView().Build());
        }

        public override void TrackEvent(string category, string action, string label, long value)
        {
            Gai.SharedInstance.DefaultTracker.Send(DictionaryBuilder.CreateEvent(category, action, label, new NSNumber(value)).Build());
            Gai.SharedInstance.Dispatch();
        }
    }
}