using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClaus_Factory.Abstractions
{
    public interface IToyFactory
    {
        Toy CreateNew();
    }
}
