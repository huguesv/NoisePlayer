// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Core.UnitTest;

public class NoiseGeneratorUnitTest
{
    [Theory]
    [InlineData(NoiseType.White)]
    [InlineData(NoiseType.Brown)]
    [InlineData(NoiseType.Pink)]
    [InlineData(NoiseType.Blue)]
    [InlineData(NoiseType.Violet)]
    [InlineData(NoiseType.Gray)]
    public void Generate(NoiseType noiseType)
    {
        // Arrange
        var length = 1024;
        var noise = new float[length];

        // Act
        NoiseGenerator.Generate(noise, length, noiseType);

        // Assert
        noise.Should().OnlyContain(value => value >= -3.0f && value <= 3.0f);
        noise.Where(value => Math.Abs(value) < 0.0001f).Should().HaveCountLessThan(length);
    }
}
