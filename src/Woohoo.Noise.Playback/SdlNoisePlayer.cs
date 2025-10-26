// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Playback;

using System.Diagnostics.CodeAnalysis;
using Woohoo.Noise.Core;
using Woohoo.Sdl3;

public sealed class SdlNoisePlayer
{
    private static readonly Dictionary<NoiseType, ISampleProvider> Generators = new()
    {
        { NoiseType.White, new NoiseGenerator(NoiseType.White, 0.5f) },
        { NoiseType.Brown, new NoiseGenerator(NoiseType.Brown, 2.0f) },
        { NoiseType.Pink, new NoiseGenerator(NoiseType.Pink, 0.5f) },
        { NoiseType.Blue, new NoiseGenerator(NoiseType.Blue, 0.5f) },
        { NoiseType.Violet, new NoiseGenerator(NoiseType.Violet, 0.5f) },
        { NoiseType.Gray, new NoiseGenerator(NoiseType.Gray, 0.5f) },
    };

    // We can't allocate buffer of correct length until we get a callback,
    // the amount of data that is requested varies by operating system.
    private float[] buffer = [];

    private SdlAudioStream? audioDeviceStream;
    private bool initialized;

    public bool IsPlaying { get; private set; }

    public NoiseType Noise { get; set; } = NoiseType.White;

    public void Play()
    {
        this.IsPlaying = false;

        this.Initialize();

        this.Resume();
    }

    public void Pause()
    {
        this.VerifyDeviceNotNull();

        this.audioDeviceStream.Pause();

        this.IsPlaying = false;
    }

    public void Resume()
    {
        this.VerifyDeviceNotNull();

        this.audioDeviceStream.Resume();

        this.IsPlaying = true;
    }

    private void Initialize()
    {
        if (this.initialized)
        {
            return;
        }

        SdlAudio.Initialize();

        this.audioDeviceStream = SdlAudio.DefaultDevices.Playback.OpenStream(SdlAudioFormat.SDL_AUDIO_F32LE, 1, 44100, this.AudioRequested);
        this.initialized = true;
    }

    private void AudioRequested(SdlAudioStream device, int additionalAmount, int totalAmount)
    {
        int bufferLength = additionalAmount / sizeof(float);
        if (this.buffer.Length < bufferLength)
        {
            this.buffer = new float[bufferLength];
        }

        Generators[this.Noise].Read(this.buffer, 0, bufferLength);

        device.PutStreamData(this.buffer, bufferLength);
    }

    [MemberNotNull(nameof(audioDeviceStream))]
    private void VerifyDeviceNotNull()
    {
        if (this.audioDeviceStream is null)
        {
            throw new InvalidOperationException("Stream device not set.");
        }
    }
}
