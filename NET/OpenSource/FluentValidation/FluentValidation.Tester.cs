using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using OpenSource.Model;

namespace OpenSource.FluentValidation
{
    [TestFixture]
    public class FluentValidationTester
    {
        [Test]
        public void UnitTest_CallValidate()
        {
            var item = new Item { ItemNumber = "SI001", Price = 0, Title = "12" };
            IValidator<Item> validator = new ItemValidator();
            var results = validator.Validate(item);

            if (!results.IsValid)
                Console.WriteLine(string.Join(",", results.Errors.Select(error => error.ErrorMessage)));

            Assert.IsTrue(results.IsValid);
        }
    }
}
