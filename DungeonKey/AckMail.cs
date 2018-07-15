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
    /// <summary>
    /// Acknowledgment mail.
    /// </summary>
    public class AckMail : MissionMail
    {
        /// <summary>
        /// Initiaze a new instance of the AckMail class from a mission mail.
        /// </summary>
        /// <param name="mail">Mission mail to initialize parameters.</param>
        public AckMail(MissionMail mail)
        {
            Type = MissionState.Acknowledgment;

            // Copy info from SOS mail
            LocationId = mail.LocationId;
            FloorNumber = mail.FloorNumber;
            Random = mail.Random;
            UID = mail.UID;

            var randomGenerator = new System.Random();
            RescuerUID = (ulong)randomGenerator.Next();
            RescuerUID |= (ulong)randomGenerator.Next() << 32;
        }
    }
}
