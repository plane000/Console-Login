using System;
using System.IO;
using System.Security;
using System.Text;

namespace App1 {
    class LoggedIn {
        public void loggedIn(string username) {
            var Encrypt = new encryption();
            var Login = new LogIn();
            var TextAdventure = new Game();
            var GuessRandom = new GuessRandom();
            var rw = new ReadAndWrite();

            string[] encrypteddata = rw.readEncrypted(username);
            string[] data = rw.read(username);
            string email = data[1];
            int age = int.Parse(data[2]);
            string password = data[3];
            bool admin = Convert.ToBoolean(data[5]);

            Console.Clear();

            string isadmin = null;
            if (admin) {
                isadmin = "you are signed in as an administrator";
            }

            Console.WriteLine("Welcome {0}, {1}", username, isadmin);
            Console.WriteLine();

            Console.WriteLine("You have access to:");
            Console.WriteLine();

            if (admin) {
                Console.WriteLine("(0) Admin tools");
            }

            Console.WriteLine("(1) Adventure Game");
            Console.WriteLine("(2) Guess a Random Number Game");
            Console.WriteLine("(3)");
            Console.WriteLine("(4)");
            Console.WriteLine("(5) Reveiw and change your details");
            Console.WriteLine();
            Console.WriteLine("Press enter to logout");

            var response = Console.ReadKey();
            switch (response.KeyChar) {
                case '0':
                    adminTools(admin, username, encrypteddata);
                    break;
                case '1':
                    TextAdventure.start();
                    break;
                case '2':
                    GuessRandom.start();
                    break;
                case '3':
                    string[] thing = rw.read(username);
                    break;
                case '4':
                    break;
                case '5':
                    userDetails(username);
                    break;
            }
            switch (response.Key) {
                case ConsoleKey.Enter:
                    Login.start();
                    return;
            }
            loggedIn(username);
        }

        private void adminTools(bool admin, string username, string[] encrypteddata) {
            Console.Clear();

            if (admin) {

                Console.WriteLine("Welcome {0}, you are in the admin control panel", username);



                Console.ReadKey();

            } else {
                Console.WriteLine("You are not signed in as an administrator");
                Console.WriteLine("Press enter to return");
                Console.ReadKey();
                loggedIn(username);
            }
        }

        private void userDetails(string username) {
            var get = new LogIn();
            var Encrypt = new encryption();
            var rw = new ReadAndWrite();

            string[] encrypteddata = rw.readEncrypted(username);

            string[] data = rw.read(username);
            string email = data[1];
            int age = int.Parse(data[2]);
            string password = data[3];
            int recoverycode = int.Parse(data[4]);
            bool admin = Convert.ToBoolean(data[5]);

            Console.Clear();

            Console.WriteLine(username + ", here are your details:");
            Console.WriteLine();

            Console.WriteLine("Email: " + email);
            Console.WriteLine("Age: " + age);
            Console.WriteLine();

            Console.WriteLine("(1) Update email");
            Console.WriteLine("(2) Update age");
            Console.WriteLine("(3) Change password");
            Console.WriteLine();

            Console.WriteLine("Press enter to save and return");

            var response = Console.ReadKey();
            switch (response.KeyChar) {
                case '1':
                    email = updateEmail(email);
                    break;
                case '2':
                    age = updateAge(age);
                    break;
                case '3':
                    password = changePassword(password);
                    break;
            }

            rw.write(username, email, age, password, recoverycode, admin);

            loggedIn(username);

            switch (response.Key) {
                case ConsoleKey.Enter:
                    loggedIn(username);
                    break;
            }

            userDetails(username);

            Console.ReadKey();
        }

        private string updateEmail(string email) {
            var get = new LogIn();

            Console.Clear();

            email = get.enterEmail();
            Console.WriteLine("Your new email is: " + email);
            Console.WriteLine("Press enter to return");
            Console.ReadKey();

            return email;
        }

        private int updateAge(int age) {
            var get = new LogIn();

            Console.Clear();
            age = get.enterAge();
            Console.WriteLine("Your new age is: " + age);
            Console.WriteLine("Press enter to return");
            Console.ReadKey();

            return age;
        }

        private string changePassword(string password) {
            var get = new LogIn();

            Console.Clear();
            Console.WriteLine("Your password is: {0}", password);
            Console.WriteLine("Press enter to return or space to continue");

            var response = Console.ReadKey();
            switch (response.Key) {
                case ConsoleKey.Enter:
                    return password;
                case ConsoleKey.Spacebar:
                    break;
            }

            Console.Write("Password: ");
            password = get.getPassword();

            return password;
        }
    }
}