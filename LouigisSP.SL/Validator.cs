using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LouigisSP.BO;

namespace LouigisSP.SL
{
    public static class Validator
    {
        public static bool ComparePass(string pass, Person person)
        {
            if (pass == person.Pass)
            {
                return true;
            }
            else return false;
        }


        //checks the password to be strong, password must have at least 8 characters, must contain at least one capital letter
        public static bool VerifyPassword(string password)
        {

            string hasNumber = @"[0-9]+";
            string hasUpperChar = @"[A-Z]+";
            string hasMinimum8Chars = @".{8,}";
            return (Regex.IsMatch(password, hasNumber) && Regex.IsMatch(password, hasUpperChar) && Regex.IsMatch(password, hasMinimum8Chars));

        }

        //checks the name to have more than 3 letters
        public static bool CheckName(string name)
        {
            bool blnCorrect;
            
            if (name.Length <= 3 || name is null) { 
                blnCorrect = false;
            }
            else blnCorrect = true;
            return blnCorrect;
        }

        //checks the email to be well formated 
        public static bool CheckEmail(string email)
        {
            bool isEmailCorrect;
            isEmailCorrect = Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return isEmailCorrect;
        }

        //check for the date to be not null, well formated and for the user not being older than 100 years
        public static bool CheckDateOfBirth(string DOB)
        {
            bool validDateOfBirth = false;
            TimeSpan maxAgeAccepted = new TimeSpan(36500, 0, 0, 0); //Max age accepted is 36500 days
            if (DOB != null)
            {
                DateTime date_dateOfBirth;
                bool dateWellParsed = DateTime.TryParse(DOB, out date_dateOfBirth);

                TimeSpan timeDifference = DateTime.Now.Subtract(date_dateOfBirth);
                if (dateWellParsed && timeDifference <= maxAgeAccepted)
                {
                    validDateOfBirth = true;
                }
            }
            return validDateOfBirth;
        }

        //checks for a phone number to have 10 digits and accepts the following formats
        //2222222222, 222-222-2222, 
        public static bool CheckPhoneNumber(string phone)
        {
            string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (phone != null)
            {
                return Regex.IsMatch(phone, pattern);
            }
            else return false;
        }


        //this method recieves an array with the values to be stored into the Products table, 
        //only returns true if all the elements in the array are valid
        public static bool CheckProductParameters(string[] values)
        {
            if (values is null || values.Length<7)
            {
                return false;
            }
            else {
                float price;
                int stock;
                bool blnValid = true;
                bool[] blnArray = new bool[values.Length];
                blnArray[0] = CheckName(values[0]);
                blnArray[1] = CheckName(values[1]);
                blnArray[2] = CheckName(values[2]);
                blnArray[3] = CheckName(values[3]);
                blnArray[4] = float.TryParse(values[4], out price);
                blnArray[5] = int.TryParse(values[5], out stock);
                blnArray[6] = CheckName(values[6]);
                foreach (var blnFlag in blnArray)
                {
                    if (!blnFlag)
                    {
                        blnValid = false;
                        break;
                    }
                }
                return blnValid;
            }
           
        }


    }
}
