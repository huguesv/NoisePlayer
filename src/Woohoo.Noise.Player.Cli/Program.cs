// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Noise.Player.Cli;

using Woohoo.Noise.Core;
using Woohoo.Noise.Playback;

internal class Program
{
    private static readonly Dictionary<char, (string Name, NoiseType Type)> Noises = new()
    {
        { 'W', ("White", NoiseType.White) },
        { 'B', ("Brown", NoiseType.Brown) },
        { 'P', ("Pink", NoiseType.Pink) },
        { 'L', ("Blue", NoiseType.Blue) },
        { 'V', ("Violet", NoiseType.Violet) },
    };

    private static void Main(string[] args)
    {
        Console.WriteLine("Woohoo.Noise.Player.Cli. Copyright (c) 2025 Hugues Valois.");
        Console.WriteLine();

        Console.WriteLine("Select noise or [Q]uit:");
        Console.WriteLine("[W]hite, [B]rown, [P]ink, b[L]ue, [V]iolet");
        Console.WriteLine();

        try
        {
            Console.CursorVisible = false;

            var player = new SdlNoisePlayer();

            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                char c = char.ToUpperInvariant(key.KeyChar);
                if (Noises.TryGetValue(c, out var noise))
                {
                    player.Noise = noise.Type;
                    player.Play();
                    Console.WriteLine($"Playing: {noise.Name} noise");
                }
                else if (c == 'Q')
                {
                    player.Pause();
                    break;
                }
            }
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }
}
