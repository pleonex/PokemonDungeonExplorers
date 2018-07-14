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
namespace DungeonKey.IO
{
    using System;
    using System.Text;
    using Yarhl.IO;

    /// <summary>
    /// Bit stream reader.
    /// </summary>
    public class BitReader
    {
        readonly DataStream stream;

        /// <summary>
        /// Initializes a new BitReader instance.
        /// </summary>
        /// <param name="stream">Stream to read.</param>
        public BitReader(DataStream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// Get or set the position in bits in the stream.
        /// </summary>
        /// <value>Bit position.</value>
        public long Position { get; set; }

        /// <summary>
        /// Get the length in bits of the stream.
        /// </summary>
        public long Length => stream.Length * 8;

        /// <summary>
        /// Read a bit from the stream.
        /// </summary>
        /// <returns>The bit inside a byte.</returns>
        public byte ReadBit()
        {
            int bitIdx = (int)(Position % 8);
            stream.Position = Position / 8;
            Position++;

            return (byte)((stream.ReadByte() >> bitIdx) & 1);
        }

        /// <summary>
        /// Read an array of bits into a buffer.
        /// </summary>
        /// <param name="buffer">Buffer to write the read bytes.</param>
        /// <param name="length">Number of bits to read.</param>
        public void ReadBits(byte[] buffer, int length)
        {
            int numBytes = (int)Math.Ceiling(length / 8.0);
            if (buffer.Length < numBytes)
                throw new ArgumentOutOfRangeException(nameof(buffer));

            for (int i = 0; i < length; i++) {
                buffer[i / 8] |= (byte)(ReadBit() << (i % 8));
            }
        }

        /// <summary>
        /// Read an array of bits and returns their conversion to bytes.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <returns>Bytes with the bits.</returns>
        public byte[] ReadBits(int length)
        {
            int numBytes = (int)Math.Ceiling(length / 8.0);
            byte[] buffer = new byte[numBytes];
            ReadBits(buffer, length);
            return buffer;
        }

        /// <summary>
        /// Read a byte of a given bit size.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <returns>The byte from bits.</returns>
        public byte ReadByte(int length)
        {
            return ReadBits(length)[0];
        }

        /// <summary>
        /// Read an unsigned short of a given bit size.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <returns>The ushort from bits.</returns>
        public ushort ReadUInt16(int length)
        {
            byte[] buffer = new byte[2];
            ReadBits(buffer, length);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// Read an unsigned int of a given bit size.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <returns>The uint from bits.</returns>
        public uint ReadUInt32(int length)
        {
            byte[] buffer = new byte[4];
            ReadBits(buffer, length);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// Read an unsigned long of a given bit size.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <returns>The ulong from bits.</returns>
        public ulong ReadUInt64(int length)
        {
            byte[] buffer = new byte[8];
            ReadBits(buffer, length);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>
        /// Read a string of a given bit size.
        /// </summary>
        /// <param name="length">Number of bits to read.</param>
        /// <param name="enconding">Name of the encoding for decoding chars.</param>
        /// <returns>The string from bits.</returns>
        public string ReadString(int length, string encoding)
        {
            byte[] data = ReadBits(length);

            string result = Encoding.GetEncoding(encoding).GetString(data);
            result = result.Replace("\0", "");

            return result;
        }
    }
}