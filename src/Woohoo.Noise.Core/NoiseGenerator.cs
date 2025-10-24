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

    public static void Generate(float[] noise, int length, NoiseType noiseType)
    {
        switch (noiseType)
        {
            case NoiseType.White:
                GenerateWhiteNoise(noise, length);
                break;
            case NoiseType.Brown:
                GenerateBrownNoise(noise, length);
                break;
            case NoiseType.Pink:
                GeneratePinkNoise(noise, length, amplitude: 0.5f);
                break;
            case NoiseType.Blue:
                GenerateBlueNoise(noise, length);
                break;
            case NoiseType.Violet:
                GenerateVioletNoise(noise, length);
                break;
            case NoiseType.Gray:
                GenerateGrayNoise(noise, length);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(noiseType), noiseType, null);
        }
    }

    public static void GenerateWhiteNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            noise[i] = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
        }
    }

    public static void GenerateBrownNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
            lastBrown += white * 0.02f; // Smoothing factor
            noise[i] = lastBrown;
        }
    }

    public static void GeneratePinkNoise(float[] noise, int length, float amplitude = 1.0f)
    {
        var b = new float[7];
        for (var i = 0; i < length; i++)
        {
            var white = (float)((Rand.NextDouble() * 2.0) - 1.0) * amplitude;
            b[0] = (0.99886f * b[0]) + (white * 0.0555179f);
            b[1] = (0.99332f * b[1]) + (white * 0.0750759f);
            b[2] = (0.96900f * b[2]) + (white * 0.1538520f);
            b[3] = (0.86650f * b[3]) + (white * 0.3104856f);
            b[4] = (0.55000f * b[4]) + (white * 0.5329522f);
            b[5] = (-0.7616f * b[5]) - (white * 0.0168980f);
            noise[i] = b.Sum() + (white * 0.5362f);
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
