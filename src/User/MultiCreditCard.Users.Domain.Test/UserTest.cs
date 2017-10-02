using MultiCreditCard.Users.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiCreditCard.Domain.Test
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void Test()
        {
            var a = new User("", 0, null, null);
        }
    }
}
;