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
        public byte Unknown00 { get; set; }

        public byte Unknown01 { get; set; }

        public byte Unknown02 { get; set; }

        public byte Unknown04 { get; set; }

        public byte Unknown05 { get; set; }

        public uint Unknown08 { get; set; }

        public byte Unknown0C { get; set; }

        public ushort Unknown0E { get; set; }

        public ushort Unknown10 { get; set; }

        public ushort Unknown12 { get; set; }

        public ushort Unknown14 { get; set; }

        public byte Unknown16 { get; set; }

        public ushort Unknown18 { get; set; }

        public byte Unknown1A { get; set; }

        public ushort Unknown1C { get; set; }

        /// <summary>
        /// Print the information of this mail to the standard output.
        /// </summary>
        public void PrintInformation()
        {
            Console.WriteLine("Wonder S Mail information:");
            Console.WriteLine($"* Unknown00: {Unknown00:X2}");
            Console.WriteLine($"* Unknown01: {Unknown01:X2}");
            Console.WriteLine($"* Unknown02: {Unknown02:X2}");
            Console.WriteLine($"* Unknown04: {Unknown04:X2}");
            Console.WriteLine($"* Unknown05: {Unknown05:X2}");
            Console.WriteLine($"* Unknown08: {Unknown08:X8}");
            Console.WriteLine($"* Unknown0C: {Unknown0C:X2}");
            Console.WriteLine($"* Unknown0E: {Unknown0E:X4}");
            Console.WriteLine($"* Unknown10: {Unknown10:X4}");
            Console.WriteLine($"* Unknown12: {Unknown12:X4}");
            Console.WriteLine($"* Unknown14: {Unknown14:X4}");
            Console.WriteLine($"* Unknown16: {Unknown16:X2}");
            Console.WriteLine($"* Unknown18: {Unknown18:X4}");
            Console.WriteLine($"* Unknown1A: {Unknown1A:X2}");
            Console.WriteLine($"* Unknown1C: {Unknown1C:X4}");
        }
    }
}