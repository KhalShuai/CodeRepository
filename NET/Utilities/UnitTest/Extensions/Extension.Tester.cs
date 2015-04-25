using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Utilities.Extensions;

namespace Utilities.UnitTest.Extensions
{
    [TestFixture]
    public class ExtensionTester
    {
        [Test]
        public void UnitTest_Paging()
        {
            foreach (var list in new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Paging(5))
            {
                Console.WriteLine("******");
                foreach (var i in list)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
