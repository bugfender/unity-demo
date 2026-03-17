# Bugfender Game

This is a sample project to illustrate how to use [Bugfender](https://bugfender.com/) in a Unity3D project.

[More information](https://docs.bugfender.com/docs/platforms/hybrid-platforms/bugfender-for-unity)

## Running the game

From the project root you can build and run on device or simulator using the `run.sh` script:

```bash
./run.sh ios              # Run on iOS Simulator (iPhone 16 Pro)
./run.sh android          # Run on connected Android device
./run.sh ios --build       # Force full rebuild, then run on iOS
./run.sh android --build   # Force full rebuild, then run on Android
```

- **iOS**: Requires Unity and Xcode. The script builds for the iPhone 16 Pro simulator (OS 18.6), installs the app, and opens the Simulator. Close the Unity Editor before running, as only one Unity instance can have the project open.
- **Android**: Requires Unity and a device with USB debugging enabled. The script installs the APK and launches the app.

The app uses the Bugfender app key from `Assets/Resources/bugfender_app_key.txt` (or the key set in the Bugfender component in the Main scene).

## Debugging – not seeing logs in Bugfender

### 1. Enable “print to console” (logcat / Xcode)

The demo is set up so that **print to console** is on: `Assets/Resources/bugfender_print_to_console.txt` contains `true`. That makes the Bugfender SDK mirror logs to:

- **Android**: `adb logcat` (see below)
- **iOS**: Xcode console when running from Xcode, or Console.app for the Simulator

If you see `[BF]` and Bugfender messages there but not in the dashboard, the SDK is running and the issue is likely network or app key.

### 2. Android: get logcat

With the device connected and the app installed, run:

```bash
# All app and Bugfender-related logs (recommended)
adb logcat -s Unity:V "BF/DEBUG:V" "*:S"

# Or use the helper script (see below)
./logcat.sh
```

Look for:

- `[BF] *** INITIALIZING BUGFENDER ***` – Unity SDK init started
- `Unity` – C# `Debug.Log` and, when print-to-console is on, Bugfender log mirroring
- `BF/DEBUG` – native Android SDK debug logs (device status, networking, etc.). Enable by setting `Assets/Resources/bugfender_debug.txt` to `true`; the demo has this enabled so you can see why logs might not reach the dashboard.

To capture to a file:

```bash
adb logcat -s Unity:V "BF/DEBUG:V" "*:S" | tee bugfender_logcat.txt
```

### 3. Things to check

- **App key**: Matches the app in the [Bugfender dashboard](https://dashboard.bugfender.com/); no extra spaces in `bugfender_app_key.txt`.
- **Network**: Device/simulator can reach the internet; no firewall blocking Bugfender.
- **Delay**: Logs are batched and sent periodically; wait a minute or trigger a send (e.g. open an issue) and refresh the dashboard.
- **Device in dashboard**: Open the app and check that the device appears under the app in the dashboard; then look at that device’s logs.