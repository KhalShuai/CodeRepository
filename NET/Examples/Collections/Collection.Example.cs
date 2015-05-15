using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Utilities.Serialization;

namespace Examples.Collections
{
    [TestFixture]
    public class CollectionExample
    {
        [Test]
        public void Example_01()
        {
            var itemCollection = new ItemCollection();
            itemCollection.Add(new Item { ItemNumber = "001" });
            itemCollection.Add(new Item { ItemNumber = "002" });
            Console.WriteLine(Serialization.XmlSerialize<ItemCollection>(itemCollection));
        }
    }

    #region Collection Type
    public class ItemCollection : Collection<Item> { }

    public class OrderCollection : Collection<Order>
    {
        public IList<Order> OrderList
        {
            get { return this.Items == null ? new List<Order>() : this.Items; }
        }

        public OrderCollection(IList<Order> orderList)
        {
            if (orderList == null) return;

            foreach (var order in orderList)
                this.Items.Add(order);
        }
    }

    #endregion

    #region Model
    public class Item
    {
        public string ItemNumber { get; set; }
    }

    public class Order
    {
        public string OrderNumber { get; set; }
    }
    #endregion
}
