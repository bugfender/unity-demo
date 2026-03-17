#!/usr/bin/env bash
# View Android logcat filtered for Unity and Bugfender (debugging when logs don't appear in the dashboard).
# Usage: ./logcat.sh [extra args for adb logcat, e.g. | tee out.txt]
# Requires: device connected, adb in PATH

set -e
cd "$(dirname "${BASH_SOURCE[0]}")"

if ! adb devices | grep -q 'device$'; then
  echo "No Android device connected. Connect a device and enable USB debugging." >&2
  exit 1
fi

echo "Listening for Unity and Bugfender logs (Ctrl+C to stop)..."
echo "  Unity = C# logs; BF/DEBUG = native SDK debug when bugfender_debug.txt is 'true'"
exec adb logcat -s Unity:V "BF/DEBUG:V" "*:S" "$@"
