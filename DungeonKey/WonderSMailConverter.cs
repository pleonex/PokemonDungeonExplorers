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

    /// <summary>
    /// Conversion of Mail information into password.
    /// </summary>
    public static class WonderSMailConverter
    {
        const int PasswordLength = 0x22;

        /// <summary>
        /// Converts a password into mail information.
        /// </summary>
        /// <param name="password">The password to convert.</param>
        /// <returns>Mail information from the password.</returns>
        public static WonderSMail Convert(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            // Sanitaze and check password
            password = password.Replace(" ", "");
            password = password.Replace("\t", "");
            password = password.Replace(Environment.NewLine, "");
            password = password.ToUpper();
            if (password.Length != PasswordLength)
                throw new ArgumentException("Invalid password length");

            // Do decryption rounds
            // The last byte for "scramble" is ignored. It should be the null
            // terminator 0x00.
            password = Permutation.Decrypt(password, false);
            byte[] binary = Substitution.Decrypt(password);
            Scramble.Decrypt(binary[0], binary, 4, binary.Length - 5);

            // Validate checksum
            uint crc32 = BitConverter.ToUInt32(binary, 0);
            uint newCrc32 = Crc32.Calculate(binary, 4, binary.Length - 5);
            if (crc32 != newCrc32)
               throw new FormatException("Invalid crc32");

            // Convert the binary password into the structure.
            // Write the array into a stream to use the BitReader.
            DataStream stream = new DataStream();
            stream.Write(binary, 4, binary.Length - 4);
            BitReader reader = new BitReader(stream);

            WonderSMail info = new WonderSMail();
            info.MailType = reader.ReadByte(4);
            info.MissionType = reader.ReadByte(4);
            info.MissionSubType = reader.ReadByte(4);
            info.SourceClientId = reader.ReadUInt16(11);
            info.TargetClientId = reader.ReadUInt16(11);
            info.TargetClientFemale = reader.ReadUInt16(11);
            info.RewardObjectId = reader.ReadUInt16(10);
            info.RewardType = reader.ReadByte(4);
            info.RewardId = reader.ReadUInt16(11);
            info.RestrictionType = reader.ReadByte(1);
            info.RestrictionParam = reader.ReadUInt16(11);
            info.Random = reader.ReadUInt32(24);
            info.LocationId = reader.ReadByte(8);
            info.FloorNumber = reader.ReadByte(8);
            info.Requirement = reader.ReadByte(8);

            return info;
        }

        /// <summary>
        /// Converts a missiong information into a password.
        /// </summary>
        /// <param name="info">Mission to convert.</param>
        /// <returns>The password.</returns>
        public static string Convert(WonderSMail info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            // Serialize the structure into a bit stream
            DataStream stream = new DataStream();
            BitWriter writer = new BitWriter(stream);

            writer.Write(info.MailType, 4);
            writer.Write(info.MissionType, 4);
            writer.Write(info.MissionSubType, 4);
            writer.Write(info.SourceClientId, 11);
            writer.Write(info.TargetClientId, 11);
            writer.Write(info.TargetClientFemale, 11);
            writer.Write(info.RewardObjectId, 10);
            writer.Write(info.RewardType, 4);
            writer.Write(info.RewardId, 11);
            writer.Write(info.RestrictionType, 1);
            writer.Write(info.RestrictionParam, 11);
            writer.Write(info.Random, 24);
            writer.Write(info.LocationId, 8);
            writer.Write(info.FloorNumber, 8);
            writer.Write(info.Requirement, 8);

            // Write the stream into an array for the rounds.
            // We allocate an extra space for the checksum (first uint)
            // and the null terminator (last byte).
            byte[] binary = new byte[stream.Length + 5];
            stream.Position = 0;
            stream.Read(binary, 4, (int)stream.Length);

            // Create checksum
            uint crc32 = Crc32.Calculate(binary, 4, binary.Length - 5);
            byte[] crc32Bytes = BitConverter.GetBytes(crc32);
            binary[0] = crc32Bytes[0];
            binary[1] = crc32Bytes[1];
            binary[2] = crc32Bytes[2];
            binary[3] = crc32Bytes[3];

            // Do encryption rounds
            // The key is the checksum, we don't encrypt the null terminator.
            Scramble.Encrypt(crc32Bytes[0], binary, 4, binary.Length - 5);
            string password = Substitution.Encrypt(binary, PasswordLength);
            password = Permutation.Encrypt(password, false);

            return password;
        }

        /// <summary>
        /// Validate a password by checking the checksum.
        /// </summary>
        /// <param name="password">Password to validate.</param>
        /// <returns><c>true</c> if the password is valid, <c>false</c> otherwise.</returns>
        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            // Sanitaze and check password
            password = password.Replace(" ", "");
            password = password.Replace(Environment.NewLine, "");
            password = password.ToUpper();
            if (password.Length != PasswordLength)
                throw new ArgumentException("Invalid password length");

            // Do decryption rounds
            byte[] binary;
            try {
                password = Permutation.Decrypt(password, false);
                binary = Substitution.Decrypt(password);
                Scramble.Decrypt(binary[0], binary, 4, binary.Length - 5);
            } catch {
                return false;
            }

            // Validate checksum
            uint crc32 = BitConverter.ToUInt32(binary, 0);
            uint newCrc32 = Crc32.Calculate(binary, 4, binary.Length - 5);
            return crc32 == newCrc32;
        }
    }
}
