// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Core;

public interface ISampleProvider
{
    /// <summary>
    /// Fill the buffer with samples.
    /// </summary>
    /// <param name="buffer">The buffer to fill.</param>
    /// <param name="offset">Offset into the buffer.</param>
    /// <param name="count">Number of samples to write.</param>
    /// <returns>Number of samples written.</returns>
    int Read(float[] buffer, int offset, int count);
}
