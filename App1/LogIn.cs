using System;
using System.IO;
using System.Security;
using System.Text;

namespace App1 {
    class LogIn {
        public void start() {
            Console.Clear();
            Console.WriteLine("Would you like to (L)ogin or (S)ign up?"); //login / signup chooser
            var response = Console.ReadKey();
            switch (response.KeyChar) {
                case 'L':
                case 'l':
                    login();
                    break;

                case 'S':
                case 's':
                    signup();
                    break;
            }
            start();
        }

        public void signup() {
            var Encrypt = new encryption();
            var rw = new ReadAndWrite();

            Console.Clear();

            string username = enterUsername(); //calls enterusernmae - username is returned

            string email = enterEmail(); //calls enteremail - email is returned 

            int age = enterAge(); //calls enterage - age is returned

            Console.Write("Password: ");
            string password = getPassword();

            Random rnd = new Random();
            int recoverycode = rnd.Next(100000000, 999999999); // picks recovery code 
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Your recovery code is {0}, if you have " +
                "forgoten your password you can use this to retreive your " +
                "password.", recoverycode);

            bool admin = false;
            if (username == "Plane000_BEN") // sets admin for specified username
            {
                admin = true;
            }

            rw.write(username, email, age, password, recoverycode, admin);

            Console.WriteLine();
            Console.WriteLine("SignUp Succesfull, enter to continue");
            Console.ReadKey();

            start();
        }

        public void login() {
            var Encrypt = new encryption();
            var rw = new ReadAndWrite();

            Console.Clear();

            string username = null;
            bool findfile = false;
            while (findfile == false) {
                Console.Write("Username:");
                username = Console.ReadLine();

                if (File.Exists(username + ".asd")) {
                    break;
                } else {
                    Console.WriteLine("The user you have entered does not exist");
                    Console.WriteLine("Press enter to try again and S to signup");
                    var response = Console.ReadKey();
                    switch (response.KeyChar) {
                        case 'S':
                        case 's':
                            signup();
                            break;
                    }
                }
            }

            string encryptedpassword = rw.readEncryptedPassword(username);

            bool passwordcorrect = false;
            while (!passwordcorrect) {
                Console.Write("Password:");
                string enteredpassword = Encrypt.Encrypt(getPassword());

                if (enteredpassword == encryptedpassword) {
                    LoginSuccess(username);
                    return;
                } else {
                    Console.WriteLine();
                    Console.WriteLine("The password you have entered is incorrect " +
                        "press enter to try again and L to log in with a " +
                        "different user");
                    Console.WriteLine("If you have forgotten your password press " +
                        "R to recover your account");
                    var response = Console.ReadKey();
                    switch (response.KeyChar) {
                        case 'L':
                        case 'l':
                            login();
                            break;
                        case 'R':
                        case 'r':
                            AccountRecovery(username);
                            break;
                    }
                }
            }
            return;
        }

        public void LoginSuccess(string username) {
            var i = new LoggedIn();
            i.loggedIn(username);
        }

        private void AccountRecovery(string username) {
            var Encrypt = new encryption();
            var rw = new ReadAndWrite();

            Console.Clear();

            string encryptedpassword = rw.readEncryptedPassword(username);
            string encryptedrecovery = rw.readEncryptedRecovery(username);

            bool recoverycorrect = false;
            while (recoverycorrect == false) {
                Console.Write("Please enter the recovery code given to you at account " +
                    "creation: ");

                string recovery = Encrypt.Encrypt(Console.ReadLine());

                if (recovery == encryptedrecovery) {

                    Console.WriteLine("Your password is {0}", Encrypt.Decrypt(encryptedpassword));
                    Console.WriteLine("Press enter to return");
                    Console.ReadKey();
                    return;
                } else {
                    Console.WriteLine("Your recovery code was incorrect");
                    Console.WriteLine("Press enter to try again or S to signup");
                    var response = Console.ReadKey();
                    switch (response.KeyChar) {
                        case 'S':
                        case 's':
                            signup();
                            break;
                    }
                }
            }
            return;
        }





        private string enterUsername() {
            bool usernamecorrect = false;
            string username = null;
            while (!usernamecorrect) {
                Console.Write("Username: ");
                username = Console.ReadLine();

                if (username.Contains(" ")) {
                    Console.WriteLine("That username is not valid");
                } else if (username == "") {
                    Console.WriteLine("That username is not valid");
                } else if (File.Exists(username + ".asd")) {
                    Console.WriteLine("That account is taken");
                } else {
                    usernamecorrect = true;
                    break;
                }
            }
            return username;
        }

        public int enterAge() {
            bool agepassed = false;
            int age = 0;
            while (!agepassed) {
                Console.Write("Age: ");
                string ReadLine = Console.ReadLine();
                try {
                    age = int.Parse(ReadLine);
                    break;
                } catch (FormatException) {
                    Console.WriteLine("{0} is not an number", ReadLine);
                } catch (OverflowException) {
                    Console.WriteLine("Thats not your age");
                }
            }
            return age;
        }

        public string enterEmail() {
            bool checkemail = false;
            string email = null;
            while (checkemail == false) {
                Console.Write("Email: ");
                email = Console.ReadLine();
                checkemail = checkEmail(email);

                if (checkemail) {
                    break;
                } else {
                    Console.WriteLine("The email you entered is not valid");
                }
            }
            return email;
        }

        public bool checkEmail(string email) {
            bool passed = false;

            if (email.Contains("@") && email.Contains(".")) {
                passed = true;
            } else {
                passed = false;
            }

            return passed;
        }

        public string getPassword() {
            var pwd = new SecureString();
            while (true) {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter) {
                    break;
                } else if (i.Key == ConsoleKey.Backspace) {
                    if (pwd.Length > 0) {
                        pwd.RemoveAt(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                } else {
                    pwd.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            string password = new System.Net.NetworkCredential(string.Empty, pwd).Password;
            return Convert.ToString(password);
        }
    }
}