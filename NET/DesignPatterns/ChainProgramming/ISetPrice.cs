using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainProgramming
{
    public interface ISetPrice
    {
        IUpdateItem SetPrice(decimal price);
    }
}
