# GoogleAnalyticsPlugin for Xamarin Forms
Simple Google Analytics Plugin for Xamarin Forms. 

This is my first plugin, it's a very basic Google Analytics utility for Xamarin Android, iOS and Forms. If you have any comments or suggestions, please let me know.

### Setup
* Available on NuGet: https://www.nuget.org/packages/Xamarin.Plugins.GoogleAnalytics/ [![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.Media.svg?label=NuGet)](https://www.nuget.org/packages/Xamarin.Plugins.GoogleAnalytics/)
* Install into your PCL project and Client projects.
* Please see the additional setup for each platforms permissions.

**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|iOS 8+|
|Xamarin.Android|Yes|API 15+|
|Windows Phone Silverlight|No||
|Windows Phone RT|No||
|Windows Store RT|No||
|Windows 10 UWP|No||
|Xamarin.Mac|No||


### API Usage

Call **CrossGoogleAnalytics.Current** from any project or PCL to gain access to APIs.

```csharp
public interface IGoogleAnalytics
{		
	bool EnableAdvertisingTracking {get;set;}

	void Report (string message, bool isFatal = false);
	void Report (Exception exception, bool isFatal = false);
	void Report (Exception exception, IDictionary<string, string> extraData, bool isFatal = false);
	void TrackEvent (string category, string action, string label, long value);
	void TrackPage (string pageName);
}
```

### IMPORTANT

You must first initialize plugin

## Android

On Android, in your main Activity's OnCreate (..) implementation, call:

```csharp
GoogleAnalyticsImplementation.Init("UA-XXXXXXX-X", this);
```
In your AppDelegate's FinishedLaunching (..) implementation, call:

## iOS

```csharp
GoogleAnalyticsImplementation.Init("trackerName", "UA-XXXXXXX-X");
```

### Usage
Simpliest way to report an exception:

```csharp
CrossGoogleAnalytics.Current.Report(ex);
```

Track a page:

```csharp
CrossGoogleAnalytics.Current.TrackPage("PageName");
```

Track an event:

```csharp
CrossGoogleAnalytics.Current.TrackEvent("category", "action", "label", 0);
```