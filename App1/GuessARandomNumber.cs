using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1 {
    class GuessRandom {
        public void start() {
            Console.Clear();

            Random rnd = new Random();
            int RandomNumber = rnd.Next(1, 100);

            Console.WriteLine("A random number has been generated, you have to guess it. " +
                "Each guess you will be told wheather your guess was higher or lower");

            int guess;
            bool correct = false;
            while (correct == false) {
                Console.Write("Your guess:");

                string stringguess = Console.ReadLine();
                try {
                    guess = int.Parse(stringguess);
                    Console.WriteLine();

                    if (guess == RandomNumber) {
                        break;
                    } else if (guess < RandomNumber) {
                        Console.WriteLine("The number is greater than your guess");
                        Console.WriteLine();
                    } else if (guess > RandomNumber) {
                        Console.WriteLine("The number is less than your guess");
                        Console.WriteLine();
                    }
                } catch (FormatException) {
                    Console.WriteLine("{0} is not a number", stringguess);
                }
            }

            Console.WriteLine("Correct, the number was {0}", RandomNumber);
            Console.WriteLine("Press enter to return to your account");
            Console.ReadKey();
            return;
        }
    }
}
