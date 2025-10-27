// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Player.Models;

using System.Text.Json;

public sealed class UserSettingsManager
{
    private readonly string settingsPath;

    public UserSettingsManager(string settingsPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(settingsPath);

        this.settingsPath = settingsPath;
    }

    public void SaveSettings(UserSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(this.settingsPath) ?? string.Empty);

        using (var fs = new FileStream(this.settingsPath, FileMode.Create))
        {
            JsonSerializer.Serialize(fs, settings);
        }
    }

    public UserSettings LoadSettings()
    {
        if (!File.Exists(this.settingsPath))
        {
            return new UserSettings();
        }

        using (var fs = new FileStream(this.settingsPath, FileMode.Open))
        {
            return JsonSerializer.Deserialize<UserSettings>(fs) ?? new UserSettings();
        }
    }
}
