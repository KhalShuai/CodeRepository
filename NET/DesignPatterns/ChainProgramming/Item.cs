using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainProgramming
{
    public class Item
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return string.Format("Title:{0},Price:{1}.", Title, Price);
        }
    }
}
