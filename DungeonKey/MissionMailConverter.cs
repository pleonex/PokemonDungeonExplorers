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
    public static class MissionMailConverter
    {
        const int PasswordLength = 0x36;
        const string EncodingName = "iso-8859-1";

        /// <summary>
        /// Converts a password into mail information.
        /// </summary>
        /// <param name="password">The password to convert.</param>
        /// <returns>Mail information from the password.</returns>
        public static MissionMail Convert(string password)
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
            // The last byte for "scramble" is ignored. It should be the null
            // terminator 0x00.
            password = Permutation.Decrypt(password);
            byte[] binary = Substitution.Decrypt(password);
            Scramble.Decrypt(binary[0], binary, 1, binary.Length - 2);

            // Validate checksum
            byte checksum = binary[0]; // The scramble key is the checksum too.
            byte newChecksum = Checksum.Calculate(binary, 1, binary.Length - 1);
            if (checksum != newChecksum)
                throw new FormatException("Invalid checksum");

            // Convert the binary password into the structure.
            // Write the array into a stream to use the BitReader.
            DataStream stream = new DataStream();
            stream.Write(binary, 1, binary.Length - 1);
            BitReader reader = new BitReader(stream);

            MissionMail info = new MissionMail();
            info.Type = (MissionState)reader.ReadByte(4);
            info.LocationId = reader.ReadByte(7);
            info.FloorNumber = reader.ReadByte(7);
            info.Random = (info.Type == MissionState.Sos) ? reader.ReadUInt32(24) : 0x00;
            info.UID = reader.ReadUInt64(64);
            info.ClientLanguage = (GameLanguage)reader.ReadByte(4);
            info.ClientName = reader.ReadString(80, EncodingName);
            info.ObjectID1 = (info.Type == MissionState.Sos) ? (ushort)0x00 : reader.ReadUInt16(10);
            info.ObjectID2 = (info.Type == MissionState.Sos) ? (ushort)0x00 : reader.ReadUInt16(10);
            info.RescuerUID = reader.ReadUInt64(64);
            info.GameType = (GameType)reader.ReadByte(2);

            return info;
        }

        /// <summary>
        /// Converts a missiong information into a password.
        /// </summary>
        /// <param name="info">Mission to convert.</param>
        /// <returns>The password.</returns>
        public static string Convert(MissionMail info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            // Serialize the structure into a bit stream
            DataStream stream = new DataStream();
            BitWriter writer = new BitWriter(stream);

            writer.WriteByte((byte)info.Type, 4);
            writer.WriteByte(info.LocationId, 7);
            writer.WriteByte(info.FloorNumber, 7);
            if (info.Type == MissionState.Sos)
                writer.WriteUInt32(info.Random, 24);
            writer.WriteUInt64(info.UID, 64);
            writer.WriteByte((byte)info.ClientLanguage, 4);
            writer.WriteString(info.ClientName, 80, EncodingName);
            if (info.Type != MissionState.Sos) {
                writer.WriteUInt16(info.ObjectID1, 10);
                writer.WriteUInt16(info.ObjectID2, 10);
            }

            writer.WriteUInt64(info.RescuerUID, 64);
            writer.WriteByte((byte)info.GameType, 2);

            // Write the stream into an array for the rounds.
            // We allocate an extra space for the checksum (first byte)
            // and the null terminator (last byte).
            byte[] binary = new byte[stream.Length + 2];
            stream.Position = 0;
            stream.Read(binary, 1, binary.Length - 2);

            // Create checksum
            byte checksum = Checksum.Calculate(binary, 1, binary.Length - 1);
            binary[0] = checksum;

            // Do encryption rounds
            // The key is the checksum, we don't encrypt the null terminator.
            Scramble.Encrypt(checksum, binary, 1, binary.Length - 2);
            string password = Substitution.Encrypt(binary);
            password = Permutation.Encrypt(password);

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
                password = Permutation.Decrypt(password);
                binary = Substitution.Decrypt(password);
                Scramble.Decrypt(binary[0], binary, 1, binary.Length - 2);
            } catch {
                return false;
            }

            // Validate checksum
            byte checksum = binary[0];
            byte newChecksum = Checksum.Calculate(binary, 1, binary.Length - 1);

            return checksum == newChecksum;
        }
    }
}
