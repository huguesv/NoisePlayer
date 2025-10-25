// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Core;

using System;
using System.Linq;

public static class NoiseGenerator
{
    private static readonly Random Rand = new();
    private static float lastBrown = 0.0f;
    private static float lastWhite = 0.0f;
    private static float[] pinkBuffer = new float[7];

    public static void Generate(float[] noise, int length, NoiseType noiseType)
    {
        switch (noiseType)
        {
            case NoiseType.White:
                GenerateWhiteNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Brown:
                GenerateBrownNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Pink:
                GeneratePinkNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Blue:
                GenerateBlueNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Violet:
                GenerateVioletNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Gray:
                GenerateGrayNoise(noise, length, amplitude: 0.5f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(noiseType), noiseType, null);
        }
    }

    public static void GenerateWhiteNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0);
            noise[i] = white * amplitude;
        }
    }

    public static void GenerateBrownNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0);
            lastBrown += white * 0.02f; // Smoothing factor
            lastBrown = Math.Clamp(lastBrown, -1.0f, 1.0f);
            lastBrown *= 0.998f; // Pulling factor
            noise[i] = lastBrown * amplitude;
        }
    }

    public static void GeneratePinkNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0);
            pinkBuffer[0] = (0.99886f * pinkBuffer[0]) + (white * 0.0555179f);
            pinkBuffer[1] = (0.99332f * pinkBuffer[1]) + (white * 0.0750759f);
            pinkBuffer[2] = (0.96900f * pinkBuffer[2]) + (white * 0.1538520f);
            pinkBuffer[3] = (0.86650f * pinkBuffer[3]) + (white * 0.3104856f);
            pinkBuffer[4] = (0.55000f * pinkBuffer[4]) + (white * 0.5329522f);
            pinkBuffer[5] = (-0.7616f * pinkBuffer[5]) - (white * 0.0168980f);
            double pink = pinkBuffer.Sum() + (white * 0.5362f);
            pinkBuffer[6] = white * 0.115926f;
            noise[i] = (float)(pink / 5.0f * amplitude);
        }
    }

    public static void GenerateBlueNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
            noise[i] = white - lastWhite;
            lastWhite = white;
        }
    }

    public static void GenerateVioletNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
            noise[i] = white - (2 * lastWhite);
            lastWhite = white;
        }
    }

    public static void GenerateGrayNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
            noise[i] = white * (1.0f - ((float)Math.Log10(i + 1) / (float)Math.Log10(length)));
        }
    }
}
