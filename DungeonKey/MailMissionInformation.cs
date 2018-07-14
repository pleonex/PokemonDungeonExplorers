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

    /// <summary>
    /// Information from a mail mission.
    /// </summary>
    public class MailMissionInformation
    {
        /// <summary>
        /// Get or set the type of mail.
        /// </summary>
        /// <value>Type of mail.</value>
        public byte Type { get; set; }

        /// <summary>
        /// Get or set the ID of the mission location.
        /// </summary>
        /// <value>The ID of the mission location.</value>
        public byte LocationId { get; set; }

        /// <summary>
        /// Get or set the floor number.
        /// </summary>
        /// <value>The floor number.</value>
        public byte FloorNumber { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0x08.
        /// </summary>
        /// <value>Unknown field.</value>
        public uint Unknown08 { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0x0C.
        /// </summary>
        /// <value>Unknown field.</value>
        public uint Unknown0C { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0x10.
        /// </summary>
        /// <value>Unknown field.</value>
        public uint Unknown10 { get; set; }

        /// <summary>
        ///Get or set the unique ID.
        /// </summary>
        /// <value></value>
        public ulong UID { get; set; }

        /// <summary>
        /// Get or set the type of client name.
        /// </summary>
        /// <value>Type of client name.</value>
        public byte ClientNameType { get; set; }

        /// <summary>
        /// Get or set the client name.
        /// </summary>
        /// <value>Client name.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0xA0.
        /// </summary>
        /// <value>Unknown field.</value>
        public ushort UnknownA0 { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0xA2.
        /// </summary>
        /// <value>Unknown field.</value>
        public ushort UnknownA2 { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0xA4.
        /// </summary>
        /// <value>Unknown field.</value>
        public ulong UnknownA4 { get; set; }

        /// <summary>
        /// Get or set the remaining mission attempts.
        /// </summary>
        /// <value>Remaining mission attempts.</value>
        public byte RemainingAttempts { get; set; }

        /// <summary>
        /// Get or set an unknown field at position 0xAD.
        /// </summary>
        /// <value>Unknown field.</value>
        public byte UnknownAD { get; set; }

        /// <summary>
        /// Get or set the game type.
        /// </summary>
        /// <value>Game type.</value>
        public GameType GameType { get; set; }

        /// <summary>
        /// Print to the standard output the information from the mail.
        /// </summary>
        public void PrintInformation()
        {
            Console.WriteLine("Mail information:");
            Console.WriteLine($"* Type: {Type}");
            Console.WriteLine($"* LocationId: 0x{LocationId:X2}");
            Console.WriteLine($"* FloorNumber: {FloorNumber}");
            Console.WriteLine($"* Unknown08: 0x{Unknown08:X6}");
            Console.WriteLine($"* Unknown0C: 0x{Unknown0C:X8}");
            Console.WriteLine($"* Unknown10: 0x{Unknown10:X8}");
            Console.WriteLine($"* UID: 0x{UID:X16}");
            Console.WriteLine($"* ClientNameType: 0x{ClientNameType:X2}");
            Console.WriteLine($"* ClientName: {ClientName}");
            Console.WriteLine($"* UnknownA0: 0x{UnknownA0:X4}");
            Console.WriteLine($"* UnknownA2: 0x{UnknownA2:X4}");
            Console.WriteLine($"* UnknownA4: 0x{UnknownA4:X16}");
            Console.WriteLine($"* RemainingAttempts: {RemainingAttempts}");
            Console.WriteLine($"* UnknownAD: 0x{UnknownAD:X2}");
            Console.WriteLine($"* GameType: {GameType}");
        }
    }
}