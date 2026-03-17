#!/usr/bin/env bash
# Run the Bugfender Unity demo on iOS Simulator and/or Android device.
# Usage:
#   ./run.sh ios              Run on iOS Simulator (builds if needed)
#   ./run.sh android          Run on connected Android device (builds if needed)
#   ./run.sh ios --build      Force Unity + Xcode/APK build before running
#   ./run.sh android --build  Force Unity + APK build before running

set -e
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

UNITY_EDITOR="/Applications/Unity/Hub/Editor/6000.3.11f1/Unity.app/Contents/MacOS/Unity"
IOS_SIM="iPhone 16 Pro"
IOS_SIM_OS="18.6"
BUNDLE_ID="com.Bugfender.BugfenderGame"

do_build_ios() {
  echo "==> Building for iOS (Unity)..."
  "$UNITY_EDITOR" -quit -batchmode -projectPath . -executeMethod iOSBuild.Build -logFile - >/dev/null 2>&1
  echo "==> Building for iOS Simulator (Xcode)..."
  xcodebuild -project Builds/iOS/Unity-iPhone.xcodeproj -scheme Unity-iPhone \
    -sdk iphonesimulator -configuration Debug -derivedDataPath Builds/iOS/build \
    -destination "platform=iOS Simulator,name=$IOS_SIM,OS=$IOS_SIM_OS" build \
    -quiet 2>/dev/null || xcodebuild -project Builds/iOS/Unity-iPhone.xcodeproj -scheme Unity-iPhone \
    -sdk iphonesimulator -configuration Debug -derivedDataPath Builds/iOS/build \
    -destination "platform=iOS Simulator,name=$IOS_SIM,OS=$IOS_SIM_OS" build
}

do_build_android() {
  echo "==> Building for Android (Unity)..."
  "$UNITY_EDITOR" -quit -batchmode -projectPath . -executeMethod iOSBuild.BuildAndroid -logFile - >/dev/null 2>&1
}

run_ios() {
  if [[ "$FORCE_BUILD" == "1" ]] || [[ ! -d Builds/iOS/Unity-iPhone.xcodeproj ]]; then
    do_build_ios
  else
    # Ensure app is built
    if [[ ! -f Builds/iOS/build/Build/Products/Debug-iphonesimulator/BugfenderGame.app/BugfenderGame ]]; then
      do_build_ios
    fi
  fi
  local APP="$SCRIPT_DIR/Builds/iOS/build/Build/Products/Debug-iphonesimulator/BugfenderGame.app"
  echo "==> Booting simulator and installing app..."
  xcrun simctl boot "$IOS_SIM" 2>/dev/null || true
  xcrun simctl install "$IOS_SIM" "$APP"
  xcrun simctl launch "$IOS_SIM" "$BUNDLE_ID"
  open -a Simulator
  echo "==> Launched on iOS Simulator ($IOS_SIM)."
}

run_android() {
  local APK="$SCRIPT_DIR/Library/Bee/Android/Prj/IL2CPP/Gradle/launcher/build/outputs/apk/release/launcher-release.apk"
  if [[ "$FORCE_BUILD" == "1" ]] || [[ ! -f "$APK" ]]; then
    do_build_android
  fi
  if [[ ! -f "$APK" ]]; then
    echo "Error: APK not found. Run with --build to build first." >&2
    exit 1
  fi
  if ! adb devices | grep -q 'device$'; then
    echo "Error: No Android device connected. Connect a device and enable USB debugging." >&2
    exit 1
  fi
  echo "==> Installing and launching on Android device..."
  adb install -r "$APK"
  adb shell monkey -p "$BUNDLE_ID" -c android.intent.category.LAUNCHER 1 >/dev/null 2>&1
  echo "==> Launched on Android device."
}

FORCE_BUILD="0"
for arg in "$@"; do
  if [[ "$arg" == "--build" ]]; then
    FORCE_BUILD="1"
    break
  fi
done

case "${1:-}" in
  ios)
    run_ios
    ;;
  android)
    run_android
    ;;
  *)
    echo "Usage: $0 ios|android [--build]"
    echo "  ios     Run on iOS Simulator ($IOS_SIM)."
    echo "  android Run on connected Android device."
    echo "  --build Force Unity (and platform) build before running."
    exit 1
    ;;
esac
