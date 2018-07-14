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
    using System.Text;
    using IO;
    using Yarhl.IO;

    public static class Substitution
    {
        const string Map = "&67NPR89F0+#STXY45MCHJ-K12=%3Q@W";

        public static byte[] Convert(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            // First convert chars into bytes
            byte[] converted = new byte[data.Length];
            for (int i = 0; i < data.Length; i++) {
                int idx = Map.IndexOf(data[i]);
                if (idx == -1)
                    throw new ArgumentException($"Invalid char: '{data[i]:X2}'");

                converted[i] = (byte)idx;
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

        public static string Convert(byte[] data)
        {
            // Create an stream for easy reading
            DataStream stream = new DataStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;

            // Each 5 bits it's an entry
            StringBuilder builder = new StringBuilder();
            BitReader reader = new BitReader(stream);

            while (reader.Position + 5 < reader.Length) {
                int idx = reader.ReadByte(5);
                if (idx >= Map.Length)
                    throw new FormatException("Invalid password byte");

                builder.Append(Map[idx]);
            }

            stream.Dispose();
            return builder.ToString();
        }
    }
}
