using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermission.API.Domain.Exceptions;
using UserPermission.API.Domain.ValueObjects;
using Xunit;

namespace UserPermission.API.Domain.UnitTests.ValueObjects
{
    public class EmailShould
    {
        [Fact]
        public void Return_Correct_Email()
        {
            var email = "emailTest@gmail.com";

            var emailCreated = new Email(email);

            emailCreated.Value.Should().Be(email);
        }

        [Fact]
        public void Throw_Invalid_Email_Exception()
        {
            var email = "invalidEmail";
            FluentActions.Invoking(() => new Email(email)).Should().Throw<InvalidEmailFormatException>();
        }
    }
}
