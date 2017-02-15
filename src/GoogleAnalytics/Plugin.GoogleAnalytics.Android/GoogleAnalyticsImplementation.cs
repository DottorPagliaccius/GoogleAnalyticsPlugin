using System;
using Android.Content;
using Android.Gms.Analytics;
using Plugin.GoogleAnalytics.Abstractions;

using GA = Android.Gms.Analytics.GoogleAnalytics;

namespace Plugin.GoogleAnalytics
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class GoogleAnalyticsImplementation : AbstractGoogleAnalytics
    {
        private static Tracker _tracker;

        public GoogleAnalyticsImplementation()
        {
            if (_tracker == null)
                CrossGoogleAnalytics.NotInitialized();
        }

        public static void Init(string trackingId, Context AppContext = null)
        {
            if (trackingId == null)
                throw new ArgumentNullException(nameof(trackingId));

            var gaInstance = GA.GetInstance(AppContext);
            gaInstance.SetLocalDispatchPeriod(10);

            _tracker = gaInstance.NewTracker(trackingId);

            _tracker.EnableExceptionReporting(true);
            _tracker.EnableAdvertisingIdCollection(true);
            _tracker.EnableAutoActivityTracking(true);
        }

        public override void Report(string message, Severity warningLevel = Severity.Warning)
        {
            var builder = new HitBuilders.ExceptionBuilder();
            builder.SetDescription(message);
            builder.SetFatal(warningLevel != Severity.Warning);

            _tracker.Send(builder.Build());
        }

        public override void TrackPage(string pageName)
        {
            _tracker.SetScreenName(pageName);
            _tracker.Send(new HitBuilders.ScreenViewBuilder().Build());
        }

        public override void TrackEvent(string category, string action, string label, long value)
        {
            var builder = new HitBuilders.EventBuilder();
            builder.SetCategory(category);
            builder.SetAction(action);
            builder.SetLabel(label);
            builder.SetValue(value);

            _tracker.Send(builder.Build());
        }
    }
}