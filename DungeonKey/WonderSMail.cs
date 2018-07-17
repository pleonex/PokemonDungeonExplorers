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
    /// Wonder Mail S information.
    /// </summary>
    public class WonderSMail
    {
        public byte MailType { get; set; }

        public byte MissionType { get; set; }

        public byte MissionSubType { get; set; }

        public byte LocationId { get; set; }

        public byte FloorNumber { get; set; }

        public uint Random { get; set; }

        public byte Requirement { get; set; }

        public ushort SourceClientId { get; set; }

        public ushort TargetClientId { get; set; }

        public ushort TargetClientFemale { get; set; }

        public ushort RewardObjectId { get; set; }

        public byte RewardType { get; set; }

        public ushort RewardId { get; set; }

        public byte RestrictionType { get; set; }

        public ushort RestrictionParam { get; set; }

        /// <summary>
        /// Print the information of this mail to the standard output.
        /// </summary>
        public void PrintInformation()
        {
            Console.WriteLine("Wonder S Mail information:");
            Console.WriteLine($"* MailType: 0x{MailType:X2}");
            Console.WriteLine($"* MissionType: 0x{MissionType:X2}");
            Console.WriteLine($"* MissionSubType: 0x{MissionSubType:X2}");
            Console.WriteLine($"* LocationId: 0x{LocationId:X2}");
            Console.WriteLine($"* FloorNumber: {FloorNumber}");
            Console.WriteLine($"* Random: 0x{Random:X6}");
            Console.WriteLine($"* Requirement: 0x{Requirement:X2}");
            Console.WriteLine($"* SourceClientId: 0x{SourceClientId:X4}");
            Console.WriteLine($"* TargetClientId: 0x{TargetClientId:X4}");
            Console.WriteLine($"* TargetClientFemale: 0x{TargetClientFemale:X4}");
            Console.WriteLine($"* RewardObjectId: 0x{RewardObjectId:X4}");
            Console.WriteLine($"* RewardType: 0x{RewardType:X2}");
            Console.WriteLine($"* RewardId: 0x{RewardId:X4}");
            Console.WriteLine($"* RestrictionType: 0x{RestrictionType:X2}");
            Console.WriteLine($"* RestrictionParam: 0x{RestrictionParam:X4}");
        }
    }
}