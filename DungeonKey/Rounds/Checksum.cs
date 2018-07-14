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
namespace DungeonKey.Rounds
{
    using System;

    /// <summary>
    /// Checksum calculation.
    ///
    /// The checksum algorithm sums the bytes and their indexes.
    /// </summary>
    public static class Checksum
    {
        /// <summary>
        /// Calculate the checksum of the byte buffer.
        /// </summary>
        /// <param name="data">Data to calculate checksum.</param>
        /// <param name="startIdx">Start index of the data.</param>
        /// <param name="size">Size of the data to process.</param>
        /// <returns>The checksum.</returns>
        public static byte Calculate(byte[] data, int startIdx, int size)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            int accumulator = 0;
            for (int i = startIdx; i < size; i++)
                accumulator += data[i] + i;

            return (byte)accumulator;
        }
    }
}
