using System;

namespace App1 {
    class Program {
        static void Main(string[] args) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            var i = new LogIn(); //calls login file LogIn.cs
            i.start();
        }
    }
}
