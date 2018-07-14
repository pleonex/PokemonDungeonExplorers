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

    public class BitWriter
    {
        readonly DataStream stream;

        public BitWriter(DataStream stream)
        {
            this.stream = stream;
        }

        public long Position { get; set; }

        public void WriteBit(byte bit)
        {
            int bitIdx = (int)(Position % 8);
            long byteIdx = Position / 8;

            // We need to initialize the next byte to 0
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

        public void WriteBits(byte bits, int length)
        {
            if (length < 0 || length >= 8)
                throw new ArgumentOutOfRangeException(nameof(length));

            for (int i = 0; i < length; i++) {
                int bitIdx = i % 8;
                byte bit = (byte)((bits >> bitIdx) & 1);
                WriteBit(bit);
            }
        }

        public void WriteBits(byte[] bits, int length)
        {
            for (int i = 0; i < length; i++) {
                byte current = bits[i / 8];
                int bitIdx = i % 8;
                byte bit = (byte)((current >> bitIdx) & 1);
                WriteBit(bit);
            }
        }

        public void WriteUInt16(ushort value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        public void WriteUInt32(uint value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        public void WriteUInt64(ulong value, int length)
        {
            WriteBits(BitConverter.GetBytes(value), length);
        }

        public void WriteString(string value, int length, string encoding)
        {
            byte[] data = Encoding.GetEncoding(encoding).GetBytes(value);
            if (length > data.Length * 8)
                Array.Resize(ref data, (length + 7) / 8);

            WriteBits(data, length);
        }
    }
}
