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

    /// <summary>
    /// Program logic class.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine(
                "DungeonKey v{0} -- KeyGen for Pokemon Mystery Dungeon ~~ by pleonex",
                Assembly.GetExecutingAssembly().GetName().Version);

            string password;
            if (args.Length == 0) {
                Console.Write("SOS-mail password: ");
                password = Console.ReadLine();
            } else {
                Console.WriteLine("SOS-mail password: " + args[0]);
                password = args[0];
            }

            MissionMail sosMail = null;
            try {
                sosMail = MissionMailConverter.Convert(password);
            } catch {
                Console.WriteLine("Error converting password.");
                Console.WriteLine("Make sure the password is valid.");
                Exit(1);
            }

            sosMail.PrintInformation();
            Console.WriteLine();

            if (MissionMailConverter.Convert(sosMail) == password)
                Console.WriteLine("\x1B[32m✔\x1B[0m EXACT password generated!");
            else
                Console.WriteLine("\x1B[91m✖\x1B[0m FAILED to generate pasword");

            Console.WriteLine("Generating rescue mail");
            AckMail ackMail = new AckMail(sosMail) {
                GameType = GameType.Time,
                ClientLanguage = GameLanguage.English,
                ClientName = "pleonex",
                ObjectID1 = 0,
                ObjectID2 = 0x25
            };
            ackMail.PrintInformation();

            string ackPassword = MissionMailConverter.Convert(ackMail);
            Console.WriteLine("Rescue password: " + ackPassword);

            try {
                MissionMailConverter.Convert(ackPassword);
                Console.WriteLine("\x1B[32m✔\x1B[0m CORRECT password generated!");
            } catch {
                Console.WriteLine("\x1B[91m✖\x1B[0m FAILED to generate pasword");
            }

            Exit(0);
        }

        static void Exit(int returnCode)
        {
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();
            Environment.Exit(returnCode);
        }
    }
}
