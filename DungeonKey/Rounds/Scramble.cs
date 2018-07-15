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

    /// <summary>
    /// Scramble / Ofuscate bytes by adding or substracting from a table.
    ///
    /// This algorithms uses a byte key to determine the start position in the
    /// table. This key also defines the range of elements in the table to
    /// iterate. In each iteration the next byte from the buffer is added or
    /// substracted from the next element of the table.
    /// </summary>
    public static class Scramble
    {
        readonly static byte[] Table = {
            0x2E, 0x75, 0x3F, 0x99, 0x09, 0x6C, 0xBC, 0x61, 0x7C, 0x2A, 0x96, 0x4A,
            0xF4, 0x6D, 0x29, 0xFA, 0x90, 0x14, 0x9D, 0x33, 0x6F, 0xCB, 0x49, 0x3C,
            0x48, 0x80, 0x7B, 0x46, 0x67, 0x01, 0x17, 0x59, 0xB8, 0xFA, 0x70, 0xC0,
            0x44, 0x78, 0x48, 0xFB, 0x26, 0x80, 0x81, 0xFC, 0xFD, 0x61, 0x70, 0xC7,
            0xFE, 0xA8, 0x70, 0x28, 0x6C, 0x9C, 0x07, 0xA4, 0xCB, 0x3F, 0x70, 0xA3,
            0x8C, 0xD6, 0xFF, 0xB0, 0x7A, 0x3A, 0x35, 0x54, 0xE9, 0x9A, 0x3B, 0x61,
            0x16, 0x41, 0xE9, 0xA3, 0x90, 0xA3, 0xE9, 0xEE, 0x0E, 0xFA, 0xDC, 0x9B,
            0xD6, 0xFB, 0x24, 0xB5, 0x41, 0x9A, 0x20, 0xBA, 0xB3, 0x51, 0x7A, 0x36,
            0x3E, 0x60, 0x0E, 0x3D, 0x02, 0xB0, 0x34, 0x57, 0x69, 0x81, 0xEB, 0x67,
            0xF3, 0xEB, 0x8C, 0x47, 0x93, 0xCE, 0x2A, 0xAF, 0x35, 0xF4, 0x74, 0x87,
            0x50, 0x2C, 0x39, 0x68, 0xBB, 0x47, 0x1A, 0x02, 0xA3, 0x93, 0x64, 0x2E,
            0x8C, 0xAD, 0xB1, 0xC4, 0x61, 0x04, 0x5F, 0xBD, 0x59, 0x21, 0x1C, 0xE7,
            0x0E, 0x29, 0x26, 0x97, 0x70, 0xA9, 0xCD, 0x18, 0xA3, 0x7B, 0x74, 0x70,
            0x96, 0xDE, 0xA6, 0x72, 0xDD, 0x13, 0x93, 0xAA, 0x90, 0x6C, 0xA7, 0xB5,
            0x76, 0x2F, 0xA8, 0x7A, 0xC8, 0x81, 0x06, 0xBB, 0x85, 0x75, 0x11, 0x0C,
            0xD2, 0xD1, 0xC9, 0xF8, 0x81, 0x70, 0xEE, 0xC8, 0x71, 0x53, 0x3D, 0xAF,
            0x76, 0xCB, 0x0D, 0xC1, 0x56, 0x28, 0xE8, 0x3C, 0x61, 0x64, 0x4B, 0xB8,
            0xEF, 0x3B, 0x41, 0x09, 0x72, 0x07, 0x50, 0xAD, 0xF3, 0x2E, 0x5C, 0x43,
            0xFF, 0xC3, 0xB3, 0x32, 0x7A, 0x3E, 0x9C, 0xA3, 0xC2, 0xAB, 0x10, 0x60,
            0x99, 0xFB, 0x08, 0x8A, 0x90, 0x57, 0x8A, 0x7F, 0x61, 0x90, 0x21, 0x88,
            0x55, 0xE8, 0xFC, 0x4B, 0x0D, 0x4A, 0x7A, 0x48, 0xC9, 0xB0, 0xC7, 0xA6,
            0xD0, 0x04, 0x7E, 0x05
        };

        /// <summary>
        /// Encrypt data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">Data to modify in the encryption process.</param>
        /// <param name="index">Index to start encrypting.</param>
        /// <param name="size">Size of the data to encrypt.</param>
        public static void Encrypt(byte key, byte[] data, int index, int size)
        {
            Convert(true, key, data, index, size);
        }

        /// <summary>
        /// Decrypt data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">Data to modify in the decryption process.</param>
        /// <param name="index">Index to start decrypting.</param>
        /// <param name="size">Size of the data to decrypt.</param>
        public static void Decrypt(byte key, byte[] data, int index, int size)
        {
            Convert(false, key, data, index, size);
        }

        private static void Convert(bool encrypt, byte key, byte[] data, int startIdx, int size)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (startIdx < 0 || startIdx >= data.Length || startIdx + size > data.Length)
                throw new ArgumentOutOfRangeException(nameof(data));

            byte tableBlockSize = (byte)((key & 0x0F) + (key >> 4) + 8);
            int direction = (key & 0x01) == 1 ? 1 : -1;
            int opSign = encrypt ? 1 : -1;

            for (int i = 0; i < size; i++) {
                int idx = ((i % tableBlockSize) * direction) + key;
                idx &= 0xFF;
                data[startIdx + i] = (byte)(data[startIdx + i] + (opSign * Table[idx]));
            }
        }
    }
}
