// Copyright (C) 2018 Benito Palacios Sánchez
//
// This file is part of PokemonMysteryDungeonExplorers.
//
// PokemonMysteryDungeonExplorers is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// PokemonMysteryDungeonExplorers is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with PokemonMysteryDungeonExplorers.  If not, see <http://www.gnu.org/licenses/>.
//
namespace DungeonKey.Rounds
{
    using System;
    using System.Collections.Generic;
    using IO;
    using Yarhl.IO;

    public static class Substitution
    {
        readonly static Dictionary<char, byte> Map = new Dictionary<char, byte> {
            { '#', 0x0B }, { '%', 0x1B }, { '&', 0x00 }, { '+', 0x0A },
            { '-', 0x16 }, { '0', 0x09 }, { '1', 0x18 }, { '2', 0x19 },
            { '3', 0x1C }, { '4', 0x10 }, { '5', 0x11 }, { '6', 0x01 },
            { '7', 0x02 }, { '8', 0x06 }, { '9', 0x07 }, { '=', 0x1A },
            { '@', 0x1E }, { 'C', 0x13 }, { 'F', 0x08 }, { 'H', 0x14 },
            { 'J', 0x15 }, { 'K', 0x17 }, { 'M', 0x12 }, { 'N', 0x03 },
            { 'P', 0x04 }, { 'Q', 0x1D }, { 'R', 0x05 }, { 'S', 0x0C },
            { 'T', 0x0D }, { 'W', 0x1F }, { 'X', 0x0E }, { 'Y', 0x0F }
        };

        public static byte[] Convert(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            // First convert chars into bytes
            byte[] converted = new byte[data.Length];
            for (int i = 0; i < data.Length; i++) {
                if (!Map.ContainsKey(data[i]))
                    throw new ArgumentException($"Invalid char: '{data[i]:X2}'");

                converted[i] = Map[data[i]];
            }

            // Now take the first 5 bit of each byte
            int reducedSize = (int)Math.Ceiling(converted.Length * 5.0 / 8);
            DataStream stream = new DataStream();
            BitWriter writer = new BitWriter(stream);
            for (int i = 0; i < converted.Length; i++)
                writer.WriteBits(converted[i], 5);

            // Copy
            stream.Position = 0;
            converted = new byte[reducedSize];
            stream.Read(converted, 0, reducedSize);
            return converted;
        }
    }
}