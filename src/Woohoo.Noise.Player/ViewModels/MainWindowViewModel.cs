// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Player.ViewModels;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Woohoo.Noise.Core;
using Woohoo.Noise.Playback;

public partial class MainWindowViewModel : ViewModelBase
{
    private static readonly (NoiseType Type, string Title)[] Tracks =
    [
        (NoiseType.White, "White Noise"),
        (NoiseType.Brown, "Brown Noise"),
        (NoiseType.Pink, "Pink Noise"),
        (NoiseType.Blue, "Blue Noise"),
        (NoiseType.Violet, "Violet Noise"),
    ];

    private readonly SdlNoisePlayer player;

    private int currentTrack;

    public MainWindowViewModel()
    {
        this.CurrentTrackTitle = Tracks[this.currentTrack].Title;

        this.player = new SdlNoisePlayer
        {
            Noise = Tracks[this.currentTrack].Type,
        };

        this.UpdateClockText();
    }

    [ObservableProperty]
    public partial string ClockText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string CurrentTrackTitle { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsPlaying { get; set; }

    [ObservableProperty]
    public partial bool ShowClock { get; set; } = true;

    [ObservableProperty]
    public partial bool ShowControls { get; set; } = true;

    [ObservableProperty]
    public partial bool ShowTrackTitle { get; set; } = true;

    [ObservableProperty]
    public partial bool IsFullScreen { get; set; }

    public void UpdateClockText()
    {
        this.ClockText = DateTime.Now.ToString("HH:mm");
    }

    [RelayCommand]
    private void ToggleClock()
    {
        this.ShowClock = !this.ShowClock;
    }

    [RelayCommand]
    private void ToggleControls()
    {
        this.ShowControls = !this.ShowControls;
    }

    [RelayCommand]
    private void ToggleTrackTitle()
    {
        this.ShowTrackTitle = !this.ShowTrackTitle;
    }

    [RelayCommand]
    private void PlayPause()
    {
        if (this.IsPlaying)
        {
            this.player.Pause();
            this.IsPlaying = false;
        }
        else
        {
            this.player.Play();
            this.IsPlaying = true;
        }
    }

    [RelayCommand]
    private void PreviousTrack()
    {
        if (this.currentTrack > 0)
        {
            this.currentTrack--;
        }
        else
        {
            this.currentTrack = Tracks.Length - 1;
        }

        this.CurrentTrackTitle = Tracks[this.currentTrack].Title;

        this.player.Noise = Tracks[this.currentTrack].Type;
    }

    [RelayCommand]
    private void NextTrack()
    {
        if (this.currentTrack < Tracks.Length - 1)
        {
            this.currentTrack++;
        }
        else
        {
            this.currentTrack = 0;
        }

        this.CurrentTrackTitle = Tracks[this.currentTrack].Title;

        this.player.Noise = Tracks[this.currentTrack].Type;
    }
}
