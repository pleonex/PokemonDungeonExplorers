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

    /// <summary>
    /// Permute / Change the position of the data.
    ///
    /// This algorithms uses a table with the permutation indexes.
    /// </summary>
    public static class Permutation
    {
        readonly static byte[] Table = {
            0x0D, 0x07, 0x19, 0x0F, 0x04, 0x1D, 0x2A, 0x31,
            0x08, 0x13, 0x2D, 0x18, 0x0E, 0x1A, 0x1B, 0x29,
            0x01, 0x20, 0x21, 0x22, 0x11, 0x33, 0x26, 0x00,
            0x35, 0x0A, 0x2B, 0x1F, 0x12, 0x23, 0x2C, 0x17,
            0x27, 0x10, 0x1C, 0x30, 0x0B, 0x02, 0x24, 0x09,
            0x32, 0x05, 0x28, 0x34, 0x2E, 0x03, 0x1E, 0x0C,
            0x25, 0x14, 0x2F, 0x16, 0x06, 0x15
        };

        /// <summary>
        /// Encrypt a string by permutating their characters.
        /// </summary>
        /// <param name="data">Data to encrypt.</param>
        /// <returns>Encrypted data.</returns>
        public static string Encrypt(string data)
        {
            return Convert(data, true);
        }

        /// <summary>
        /// Decrypt a string by permutating their characters.
        /// </summary>
        /// <param name="data">Data to decrypt.</param>
        /// <returns>Decrypted data.</returns>
        public static string Decrypt(string data)
        {
            return Convert(data, false);
        }

        static string Convert(string source, bool encrypt)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));

            if (source.Length > Table.Length)
                throw new ArgumentOutOfRangeException(nameof(source));

            // Create a filled string so we can substitute characters at any
            // position.
            StringBuilder destination = new StringBuilder();
            destination.Append(new string('0', source.Length));

            for (int i = 0; i < source.Length; i++) {
                int permIdx = Table[i];

                if (encrypt)
                    destination[permIdx] = source[i];
                else
                    destination[i] = source[permIdx];
            }

            return destination.ToString();
        }
    }
}
