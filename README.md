
# Playwire Unity Sample App

This guide explains how to export and configure the Playwire Unity Sample App in Android Studio or Xcode. The app imports [Playwire Unity Package](https://github.com/intergi/playwire-unity-package) and loads ads using the plugin.

## Android Export

1. In Unity, go to **File \> Build Profiles**.  
2. Select **Android** and click **Switch Platform** (if not already selected).  
3. Ensure the **Export Project** checkbox is **checked**.  
4. Click **Export** and choose a destination folder.  
5. Open the exported folder in **Android Studio**.

### Required Configuration

Before running the app on a device/emulator, you must update the following files in the exported Android Studio project:

* `settings.gradle`: Locate the maven block inside dependencyResolutionManagement. Replace the placeholders 'USERNAME' and 'PASSWORD' with your actual artifact credentials.
  ```
  credentials {  
      username = 'USERNAME'  
      password = 'PASSWORD'  
  }
  ```

* `launcher/src/main/AndroidManifest.xml`: Find the `<meta-data\>` tag for AdMob (or add it inside the `<application\>` tag) and replace `YOUR_GAM_APP_ID` with your valid App ID or `ca-app-pub-6531503260671471~7633749100` for testing purposes.
  ```
  <meta-data
    android:name="com.google.android.gms.ads.APPLICATION_ID"
    android:value="ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy" /> 
  ```

## iOS Export

1. In Unity, go to **File \> Build Profiles**.  
2. Select **iOS** and click **Switch Platform** (if not already selected).  
3. Click **Build** and choose a destination folder.  
4. In the output folder, double-click **Unity-iPhone.xcworkspace** to open Xcode.

### Required Configuration

Before running the app on a Simulator or Device:

* `Info.plist`: In Xcode, locate Info.plist inside the Unity-iPhone folder. Find the entry for `GADApplicationIdentifier` (or add it if missing) and replace `YOUR_GAM_APP_ID` with your valid App ID or `ca-app-pub-6531503260671471~4637188748` for testing purposes.
