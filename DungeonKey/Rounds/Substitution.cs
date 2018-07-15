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

    /// <summary>
    /// Substitute data elements converting 5-bits elements into a string.
    ///
    /// This algorithms uses map string to substitute characteres by the index
    /// (5 bits) in the map string.
    /// </summary>
    public static class Substitution
    {
        const string Map = "&67NPR89F0+#STXY45MCHJ-K12=%3Q@W";
        const int ElementSize = 5;

        /// <summary>
        /// Decrypt data by substituting characters into bits.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public static byte[] Decrypt(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            int decSize = (int)Math.Ceiling(data.Length * ElementSize / 8.0);
            byte[] decrypted = new byte[decSize];

            using (DataStream stream = new DataStream()) {
                // Convert chars into bits
                BitWriter writer = new BitWriter(stream);
                for (int i = 0; i < data.Length; i++) {
                    int idx = Map.IndexOf(data[i]);
                    if (idx == -1)
                        throw new ArgumentException($"Invalid char: '{data[i]}'");

                    writer.Write((byte)idx, ElementSize);
                }

                // Copy
                stream.Position = 0;
                stream.Read(decrypted, 0, decrypted.Length);
            }

            return decrypted;
        }

        /// <summary>
        /// Encrypt data by substituting bits into characters.
        /// </summary>
        /// <param name="data">Data to encrypt.</param>
        /// <returns>Encrypted data.</returns>
        public static string Encrypt(byte[] data, int length)
        {
            if (data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            string result;
            using (DataStream stream = new DataStream()) {
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                result = Encrypt(stream, length);
            }

            return result;
        }

        static string Encrypt(DataStream stream, int length)
        {
            // Each 5 bits it's an element to substitute.
            StringBuilder builder = new StringBuilder();
            BitReader reader = new BitReader(stream);

            for (int i = 0; i < length; i++) {
                int idx = reader.ReadByte(ElementSize);
                if (idx >= Map.Length)
                    throw new FormatException("Invalid password element");

                builder.Append(Map[idx]);
            }

            return builder.ToString();
        }
    }
}
