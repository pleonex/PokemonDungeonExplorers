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

    public class MailInformation
    {
        public byte Type { get; set; }

        public byte LocationId { get; set; }

        public byte FloorNumber { get; set; }

        public uint Unknown08 { get; set; }

        public uint Unknown0C { get; set; }

        public uint Unknown10 { get; set; }

        public ulong UID { get; set; }

        public byte ClientNameType { get; set; }

        public string ClientName { get; set; }

        public ushort UnknownA0 { get; set; }

        public ushort UnknownA2 { get; set; }

        public ulong UnknownA4 { get; set; }

        public byte RemainingAttempts { get; set; }

        public byte UnknownAD { get; set; }

        public GameType GameType { get; set; }

        public void PrintInformation()
        {
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