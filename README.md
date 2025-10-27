# Noise Player

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/huguesv/NoisePlayer/build-and-test.yml)

This is a noise player for Windows, Linux and MacOS.

It can play the following types of noise:
- White Noise
- Brown Noise
- Pink Noise
- Blue Noise
- Violet Noise

## Desktop Player

![Audio Player on Windows Screenshot](images/windows-dark.png?raw=true "Noise Player on Windows Screenshot")

## Console Player

![Windows Terminal](images/windows-cli.png?raw=true "Windows Terminal")

For more screenshots, see the [SCREENSHOTS.md](SCREENSHOTS.md) file.

## Compatibility

The application has been tested on the following operating systems, but may
work on earlier versions.

- Windows 11 24H2 (x64, ARM64)
- MacOS 15.3 (Apple Silicon)
- Ubuntu 24.10 (x64, ARM64)

## Releases

Download the [latest release here](https://github.com/huguesv/NoisePlayer/releases/latest).

Windows may prevent you from launching the application, since it is not signed.
You can still run it by clicking on "More info" and then "Run anyway".

## Usage (Desktop Player)

1. Select **Next Track** or **Previous Track** to change the select noise type.

1. Select **Play/Pause** to start or pause playback.

1. Press F11 or double-tap to view the player in full screen mode.

1. Right-click anywhere on the player window to open the context menu, where
   you can customize visibility of the clock, track title and media controls.

## Usage (Console Player)

1. Open a terminal window.
1. Press the following keys to control the player:
   - `Q` to quit.
   - `W` to play white noise.
   - `B` to play brown noise.
   - `P` to play pink noise.
   - `L` to play blue noise.
   - `V` to play violet noise.

## Building

Install the [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

To build the application, use the following command from the `\src` folder:

```
dotnet build
```

To run the desktop player, use the following command from the `\src` folder:
```
dotnet run --project Woohoo.Noise.Player
```

To run the unit tests, use the following command from the `\src` folder:

```
dotnet test
```

## License and Credits

This software is licensed under the MIT License. See the [LICENSE](LICENSE) file.

Copyright (c) 2025 Hugues Valois. All rights reserved.

This software uses the following libraries:

- [Avalonia](https://github.com/AvaloniaUI/Avalonia)
- [CommunityToolkit](https://github.com/CommunityToolkit/dotnet)
- [SDL3-CS from ppy](https://github.com/ppy/SDL3-CS)
- [SDL3-CS from flibitijibibo](https://github.com/flibitijibibo/SDL3-CS)
- [SDL3](https://github.com/libsdl-org/SDL)

This software uses assets from:

- [Vidstack](https://www.vidstack.io/)
