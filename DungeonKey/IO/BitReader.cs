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

    public class BitReader
    {
        readonly DataStream stream;

        public BitReader(DataStream stream)
        {
            this.stream = stream;
        }

        public long Position { get; set; }

        public byte ReadBit()
        {
            int bitIdx = (int)(Position % 8);
            stream.Position = Position / 8;
            Position++;

            return (byte)((stream.ReadByte() >> bitIdx) & 1);
        }

        public byte[] ReadBits(int length)
        {
            int numBytes = (int)Math.Ceiling(length / 8.0);
            byte[] buffer = new byte[numBytes];

            for (int i = 0; i < length; i++) {
                buffer[i / 8] |= (byte)(ReadBit() << (i % 8));
            }

            return buffer;
        }

        public byte ReadByte(int length)
        {
            return ReadBits(length)[0];
        }

        public ushort ReadUInt16(int length)
        {
            byte[] data = ReadBits(length);
            ushort result = 0;
            for (int i = 0; i < data.Length; i++)
                result |= (ushort)(data[i] << (i * 8));

            return result;
        }

        public uint ReadUInt32(int length)
        {
            byte[] data = ReadBits(length);
            uint result = 0;
            for (int i = 0; i < data.Length; i++)
                result |= (uint)data[i] << (i * 8);

            return result;
        }

        public ulong ReadUInt64(int length)
        {
            byte[] data = ReadBits(length);
            ulong result = 0;
            for (int i = 0; i < data.Length; i++)
                result |= (ulong)data[i] << (i * 8);

            return result;
        }

        public string ReadString(int length, string encoding)
        {
            byte[] data = ReadBits(length);

            string result = Encoding.GetEncoding(encoding).GetString(data);
            result = result.Replace("\0", "");

            return result;
        }
    }
}