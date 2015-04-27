using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DesignPatterns.ChainProgramming;

namespace DesignPatterns.UnitTest.ChainProgramming
{
    [TestFixture]
    public class ItemBuilderTester
    {
        [Test]
        public void UnitTest_UpdateItem()
        {
            ItemBuilder.WhichItem(new Item { Title = "", Price = 0 }).SetTitle("MacBookPro").SetPrice(9999).UpdateItem();
        }
    }
}
