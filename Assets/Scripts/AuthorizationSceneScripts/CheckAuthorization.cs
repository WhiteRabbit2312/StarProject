using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace StarProject
{
    public class CheckAuthorization
    {
        private bool IsPasswordConfirmed(string password, string passwordConfirm)
        {
            return password == passwordConfirm;
        }

        private bool IsLoginValid(string login)
        {
            return !string.IsNullOrEmpty(login) && Regex.IsMatch(login, Constants.LoginPattern);
        }

        private bool IsEnoughSymbols(string password)
        {
            return password.Length >= Constants.MinPasswordLength;
        }
        
        public bool ValidateRegistration(string login, string password, string passwordConfirm)
        {
            var checks = new List<Predicate<object>>
            {
                _ => IsPasswordConfirmed(password, passwordConfirm),
                _ => IsLoginValid(login),
                _ => IsEnoughSymbols(password)
            };
            return checks.All(check => check(null));
        }
    }
}