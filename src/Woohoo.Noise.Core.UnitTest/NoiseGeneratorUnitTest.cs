// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Core.UnitTest;

public class NoiseGeneratorUnitTest
{
    [Fact]
    public void Generate()
    {
        var length = 1024;
        var noise = new float[length];
        foreach (var noiseType in Enum.GetValues<NoiseType>())
        {
            NoiseGenerator.Generate(noise, length, noiseType);

            // Basic check: ensure that noise array is not all zeros
            Assert.Contains(noise, value => value != 0.0f);
        }
    }
}
