// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Player.Models;

using Woohoo.Noise.Core;

public sealed class UserSettings
{
    public bool ShowClock { get; set; } = true;

    public bool ShowControls { get; set; } = true;

    public bool ShowTrackTitle { get; set; } = true;

    public NoiseType NoiseType { get; set; } = NoiseType.White;
}
