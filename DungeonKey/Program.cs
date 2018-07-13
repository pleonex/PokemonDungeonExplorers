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
    using System.Reflection;

    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "DungeonKey v{0} -- KeyGen for Pokemon Mystery Dungeon ~~ by pleonex",
                Assembly.GetExecutingAssembly().GetName().Version);


            Console.Write("SOS-mail password: ");
            string password = Console.ReadLine();

            MailInformation mail = MailConverter.Convert(password);

            Console.WriteLine("Mission information:");
            mail.PrintInformation();
        }

        static void FailExit()
        {
            Console.WriteLine("Press a key to quit...");
            Console.ReadLine();
            Environment.Exit(-1);
        }
    }
}
