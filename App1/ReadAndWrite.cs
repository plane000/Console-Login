using System;
using System.IO;
using System.Security;
using System.Text;

namespace App1 {
    class ReadAndWrite {
        public string[] read(string username) {
            var Encrypt = new encryption();

            string LoginData64 = File.ReadAllText(username + ".asd");
            var LoginDataBytes = Convert.FromBase64String(LoginData64);
            string LoginData = Encoding.UTF8.GetString(LoginDataBytes);

            string[] encrypteddata = LoginData.Split(',');

            string email = Encrypt.Decrypt(encrypteddata[1]);
            string age = Encrypt.Decrypt(encrypteddata[2]);
            string password = Encrypt.Decrypt(encrypteddata[3]);
            string recoverycode = Encrypt.Decrypt(encrypteddata[4]);
            string admin = Encrypt.Decrypt(encrypteddata[5]);

            string[] data = new string[] { username, email, age, password, recoverycode, admin };

            return data;
        }

        public string[] readEncrypted(string username) {
            string LoginData64 = File.ReadAllText(username + ".asd");
            var LoginDataBytes = Convert.FromBase64String(LoginData64);
            string LoginData = Encoding.UTF8.GetString(LoginDataBytes);

            string[] encrypteddata = LoginData.Split(',');

            return encrypteddata;
        }

        public string readEncryptedPassword(string username) {
            string LoginData64 = File.ReadAllText(username + ".asd");
            var LoginDataBytes = Convert.FromBase64String(LoginData64);
            string LoginData = Encoding.UTF8.GetString(LoginDataBytes);

            string[] encrypteddata = LoginData.Split(',');

            string encryptedpassword = encrypteddata[3];

            return encryptedpassword;
        }

        public string readEncryptedRecovery(string username) {
            string LoginData64 = File.ReadAllText(username + ".asd");
            var LoginDataBytes = Convert.FromBase64String(LoginData64);
            string LoginData = Encoding.UTF8.GetString(LoginDataBytes);

            string[] encrypteddata = LoginData.Split(',');

            string encryptedrecovery = encrypteddata[4];

            return encryptedrecovery;
        }

        public void write(string username, string email, int age,
            string password, int recoverycode, bool admin) {
            var Encrypt = new encryption();

            string EncryptedEmail = Encrypt.Encrypt(email); // encrypts user data
            string EncryptedAge = Encrypt.Encrypt(Convert.ToString(age));
            string EncryptedPassword = Encrypt.Encrypt(password);
            string EncryptedRecovery = Encrypt.Encrypt(Convert.ToString(recoverycode));
            string EncryptedAdmin = Encrypt.Encrypt(Convert.ToString(admin));

            string WriteFileString = username + "," + EncryptedEmail + "," + EncryptedAge
                + "," + EncryptedPassword + "," + EncryptedRecovery + ","
                + EncryptedAdmin; // makes user data one string

            var WriteFileBytes = Encoding.UTF8.GetBytes(WriteFileString);
            string WriteFile64 = Convert.ToBase64String(WriteFileBytes); // converts to base 64

            File.WriteAllText(username + ".asd", WriteFile64); // writes to file

            return;
        }
    }
}
