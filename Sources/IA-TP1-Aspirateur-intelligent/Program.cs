using System;
using System.Threading;

namespace IA_TP1_Aspirateur_intelligent
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Tapez N pour l'exploration non informée ou I pour l'exploration informée : ");
            string input = "x";

            while ( (input != "N" && input != "n" && input != "I" && input != "i") )
            {
                input = Console.ReadLine();
                Console.WriteLine("Mauvais choix");
                
            }

            Console.Clear();

            if (input == "N" || input == "n")
            {
                input = "n";
            }

            if (input == "I" || input == "i")
            {
                input = "i";
            }


            Manor manor = Manor.getInstance();
            manor.setAlive(input);
        }
    }
}
