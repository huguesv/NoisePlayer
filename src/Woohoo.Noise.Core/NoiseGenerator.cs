// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Core;

using System;
using System.Linq;

public sealed class NoiseGenerator : ISampleProvider
{
    private readonly Random randomizer = new();
    private readonly float[] pinkBuffer = new float[7];
    private float lastBrown = 0.0f;
    private float lastWhite = 0.0f;

    public NoiseGenerator(NoiseType noiseType, float amplitude)
    {
        this.NoiseType = noiseType;
        this.Amplitude = amplitude;
    }

    public NoiseType NoiseType { get; }

    public float Amplitude { get; }

    public int Read(float[] buffer, int offset, int count)
    {
        switch (this.NoiseType)
        {
            case NoiseType.White:
                this.GenerateWhiteNoise(buffer, offset, count);
                break;
            case NoiseType.Brown:
                this.GenerateBrownNoise(buffer, offset, count);
                break;
            case NoiseType.Pink:
                this.GeneratePinkNoise(buffer, offset, count);
                break;
            case NoiseType.Blue:
                this.GenerateBlueNoise(buffer, offset, count);
                break;
            case NoiseType.Violet:
                this.GenerateVioletNoise(buffer, offset, count);
                break;
            case NoiseType.Gray:
                this.GenerateGrayNoise(buffer, offset, count);
                break;
            default:
                Debug.Assert(false, "Unexpected noise type");
                throw new InvalidOperationException();
        }

        return count;
    }

    private void GenerateWhiteNoise(float[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            buffer[offset + i] = this.GetNextNormalizedFloat() * this.Amplitude;
        }
    }

    private void GenerateBrownNoise(float[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var white = this.GetNextNormalizedFloat();

            this.lastBrown += white * 0.02f; // Smoothing factor
            this.lastBrown = Math.Clamp(this.lastBrown, -1.0f, 1.0f);
            this.lastBrown *= 0.998f; // Pulling factor
            buffer[offset + i] = this.lastBrown * this.Amplitude;
        }
    }

    private void GeneratePinkNoise(float[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var white = this.GetNextNormalizedFloat();

            this.pinkBuffer[0] = (0.99886f * this.pinkBuffer[0]) + (white * 0.0555179f);
            this.pinkBuffer[1] = (0.99332f * this.pinkBuffer[1]) + (white * 0.0750759f);
            this.pinkBuffer[2] = (0.96900f * this.pinkBuffer[2]) + (white * 0.1538520f);
            this.pinkBuffer[3] = (0.86650f * this.pinkBuffer[3]) + (white * 0.3104856f);
            this.pinkBuffer[4] = (0.55000f * this.pinkBuffer[4]) + (white * 0.5329522f);
            this.pinkBuffer[5] = (-0.7616f * this.pinkBuffer[5]) - (white * 0.0168980f);
            double pink = this.pinkBuffer.Sum() + (white * 0.5362f);
            this.pinkBuffer[6] = white * 0.115926f;
            buffer[offset + i] = (float)(pink / 5.0f * this.Amplitude);
        }
    }

    private void GenerateBlueNoise(float[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var white = this.GetNextNormalizedFloat() * this.Amplitude;
            buffer[offset + i] = white - this.lastWhite;
            this.lastWhite = white;
        }
    }

    private void GenerateVioletNoise(float[] buffer, int offset, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var white = this.GetNextNormalizedFloat() * this.Amplitude;
            buffer[offset + i] = white - (2 * this.lastWhite);
            this.lastWhite = white;
        }
    }

    private void GenerateGrayNoise(float[] buffer, int offset, int count)
    {
        // This doesn't work well, it causes popping sounds.
        // Probably due to the buffer count being small.
        for (var i = 0; i < count; i++)
        {
            var white = this.GetNextNormalizedFloat() * this.Amplitude;
            buffer[offset + i] = white * (1.0f - ((float)Math.Log10(i + 1) / (float)Math.Log10(count)));
        }
    }

    private float GetNextNormalizedFloat()
    {
        return (float)((this.randomizer.NextDouble() * 2.0) - 1.0);
    }
}
