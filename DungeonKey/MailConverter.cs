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
namespace DungeonKey
{
    using System;
    using IO;
    using Rounds;
    using Yarhl.IO;

    public static class MailConverter
    {
        public static MailInformation Convert(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            // Sanitaze and check password
            password = password.Replace(" ", "");
            password = password.Replace(Environment.NewLine, "");
            password = password.ToUpper();
            if (password.Length != 0x36)
                throw new ArgumentException("Invalid password length");

            // Do rounds
            password = Permutation.Convert(password);
            byte[] binary = Substitution.Convert(password);
            Scramble.Convert(binary[0], binary, 1, 0x20);

            // Validate checksum
            byte checksum = binary[0]; // checksum is the key
            byte newChecksum = Checksum.Calculate(binary, 1, 0x21);
            if (checksum != newChecksum)
                throw new FormatException("Invalid checksum");

            // Convert the binary password into the structure
            DataStream stream = new DataStream();
            stream.Write(binary, 1, 0x21);
            BitReader reader = new BitReader(stream);

            MailInformation info = new MailInformation();
            info.Type = reader.ReadByte(4);
            info.LocationId = reader.ReadByte(7);
            info.FloorNumber = reader.ReadByte(7);
            info.Unknown08 = (info.Type == 1) ? reader.ReadUInt32(24) : 0x00;
            info.Unknown0C = 0x00;
            info.Unknown10 = 0x00;
            info.UID = reader.ReadUInt64(64);
            info.ClientNameType = reader.ReadByte(4);
            info.ClientName = reader.ReadString(80, "iso-8859-1");
            info.UnknownA0 = (info.Type == 1) ? (ushort)0x00 : reader.ReadUInt16(10);
            info.UnknownA2 = (info.Type == 1) ? (ushort)0x00 : reader.ReadUInt16(10);
            info.UnknownA4 = reader.ReadUInt64(64);
            info.RemainingAttempts = 0x00; // TODO
            info.UnknownAD = 0x01;
            info.GameType = (GameType)reader.ReadByte(2);

            return info;
        }

        public static string Convert(MailInformation info)
        {
            throw new NotImplementedException();
        }
    }
}