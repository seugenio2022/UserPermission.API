using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserPermission.API.Domain.Exceptions;

namespace UserPermission.API.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private const string EmailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public string Value { get; private set; }

        private Email() { }
        public Email(string value)
        {
            if (!IsValidEmailFormat(value))
            {
                throw new InvalidEmailFormatException(value);
            }

            Value = value;
        }

        private static bool IsValidEmailFormat(string email)
        {
            return Regex.IsMatch(email, EmailRegexPattern);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
