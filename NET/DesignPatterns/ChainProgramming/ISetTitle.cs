using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.ChainProgramming
{
    public interface ISetTitle
    {
        ISetPrice SetTitle(string title);
    }
}
