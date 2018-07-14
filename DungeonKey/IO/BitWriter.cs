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
    /// Bit stream writer.
    /// </summary>
    public class BitWriter
    {
        readonly DataStream stream;

        /// <summary>
        /// Initializes a instance of the BitWriter class.
        /// </summary>
        /// <param name="stream">Stream to write.</param>
        public BitWriter(DataStream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// Get or set the bit position in the stream.
        /// </summary>
        /// <value>Bit position.</value>
        public long Position { get; set; }

        /// <summary>
        /// Write a bit from a byte.
        /// </summary>
        /// <param name="bit">Bit to write.</param>
        public void WriteBit(byte bit)
        {
            int bitIdx = (int)(Position % 8);
            long byteIdx = Position / 8;

            // We need to initialize the next byte to 0 to expand the stream.
            if (stream.Length == byteIdx) {
                stream.Seek(0, SeekMode.End);
                stream.WriteByte(0x00);
            }

            Position++;

            stream.Position = byteIdx;
            byte current = stream.ReadByte();

            current |= (byte)(bit << bitIdx);

            stream.Position = byteIdx;
            stream.WriteByte(current);
        }

        /// <summary>
        /// Write bits from an array of bytes.
        /// </summary>
        /// <param name="bits">Bits to write.</param>
        /// <param name="length">Number of bits to write.</param>
        public void WriteBits(byte[] bits, int length)
        {
            for (int i = 0; i < length; i++) {
                byte current = bits[i / 8];
                int bitIdx = i % 8;
                byte bit = (byte)((current >> bitIdx) & 1);
                WriteBit(bit);
            }
        }

        /// <summary>
        /// Write a byte of the given bit size.
        /// </summary>
        /// <param name="value">Bits to write.</param>
        /// <param name="length">Number of bits to write.</param>
        public void WriteByte(byte value, int length)
        {
            WriteBits(new[] { value }, length);
        }

        /// <summary>
        /// Write an ushort of the given bit size.
        /// </summary>
        /// <param name="value">Bits to write.</param>
        /// <param name="length">Number of bits to write.</param>
        public void WriteUInt16(ushort value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        /// <summary>
        /// Write an uint of the given bit size.
        /// </summary>
        /// <param name="value">Bits to write.</param>
        /// <param name="length">Number of bits to write.</param>
        public void WriteUInt32(uint value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        /// <summary>
        /// Write an ulong of the given bit size.
        /// </summary>
        /// <param name="value">Bits to write.</param>
        /// <param name="length">Number of bits to write.</param>
        public void WriteUInt64(ulong value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        /// <summary>
        /// Write a string of the given bit size.
        /// </summary>
        /// <param name="value">String to write.</param>
        /// <param name="length">Number of bits to write.</param>
        /// <param name="encoding">Encoding name for encoding chars.</param>
        public void WriteString(string value, int length, string encoding)
        {
            byte[] data = Encoding.GetEncoding(encoding).GetBytes(value);

            // Fill with zeroes to have at least null bytes to write bits.
            if (length > data.Length * 8)
                Array.Resize(ref data, (length + 7) / 8);

            WriteBits(data, length);
        }
    }
}
