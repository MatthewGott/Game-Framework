using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV._SWC___Game_Framework.Interfaces
{
    public interface ICreature
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public interface IWorldObject
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public interface IFactory
    {
        public int ID { get; set; }
    }
}