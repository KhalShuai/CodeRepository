using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainProgramming
{
    public class ItemBuilder :
        ISetTitle,
        ISetPrice,
        IUpdateItem
    {
        private Item item;

        public static ISetTitle WhichItem(Item item)
        {
            return new ItemBuilder(item);
        }

        public ItemBuilder(Item item)
        {
            this.item = item;
        }

        public void UpdateItem()
        {
            Console.WriteLine(item);
        }

        public IUpdateItem SetPrice(decimal price)
        {
            this.item.Price = price;
            return this;
        }

        public ISetPrice SetTitle(string title)
        {
            this.item.Title = title;
            return this;
        }
    }
}
