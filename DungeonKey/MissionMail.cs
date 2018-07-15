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
    public class MissionMail
    {
        /// <summary>
        /// Get or set the type of mail.
        /// </summary>
        /// <value>Type of mail.</value>
        public MissionState Type { get; set; }

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
        /// Get or set the random field.
        /// </summary>
        /// <value>Random field.</value>
        public uint Random { get; set; }

        /// <summary>
        ///Get or set the unique ID.
        /// </summary>
        /// <value></value>
        public ulong UID { get; set; }

        /// <summary>
        /// Get or set the client language.
        /// </summary>
        /// <value>Client language.</value>
        public GameLanguage ClientLanguage { get; set; }

        /// <summary>
        /// Get or set the client name.
        /// </summary>
        /// <value>Client name.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Get or set the first ID of the gift object..
        /// </summary>
        /// <value>First ID of the object.</value>
        public ushort ObjectID1 { get; set; }

        /// <summary>
        /// Get or set the second ID of the gift object.
        /// </summary>
        /// <value>Second ID of the object.</value>
        public ushort ObjectID2 { get; set; }

        /// <summary>
        /// Get or set an the rescuer unique ID.
        /// </summary>
        /// <value>Rescuer UID.</value>
        public ulong RescuerUID { get; set; }

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
            Console.WriteLine($"* Random: 0x{Random:X6}");
            Console.WriteLine($"* UID: 0x{UID:X16}");
            Console.WriteLine($"* ClientLanguage: {ClientLanguage}");
            Console.WriteLine($"* ClientName: {ClientName}");
            Console.WriteLine($"* ObjectID1: 0x{ObjectID1:X4}");
            Console.WriteLine($"* ObjectID2: 0x{ObjectID2:X4}");
            Console.WriteLine($"* RescuerUID: 0x{RescuerUID:X16}");
            Console.WriteLine($"* GameType: {GameType}");
        }
    }
}