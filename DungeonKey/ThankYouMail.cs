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
    public class ThankYouMail : MissionMail
    {
        public ThankYouMail(MissionMail mail)
        {
            Type = MissionState.ThankYou;

            // Copy info from rescue mail
            LocationId = mail.LocationId;
            FloorNumber = mail.FloorNumber;
            Random = mail.Random;
            UID = mail.UID;
            RescuerUID = mail.RescuerUID;
        }
    }
}