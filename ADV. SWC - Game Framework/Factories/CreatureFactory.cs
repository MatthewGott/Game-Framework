using System;
using ADV._SWC___Game_Framework.Interfaces;
using ADV._SWC___Game_Framework.Types;

namespace ADV._SWC___Game_Framework.Factories
{
    public class CreatureFactory : IFactory
    {
        private static int nextID = 0;
        private World world { get; set; }
        public int ID { get; set; }

        /// <summary>
        /// The constructor for the CreatureFactory class
        /// </summary>
        /// <param name="world1">The world the factory is attached to</param>
        public CreatureFactory(World world1) 
        {
            world = world1;
            ID = ++nextID;
        }

        /// <summary>
        /// Primary function for the factory to create a Creature
        /// </summary>
        /// <returns>Creature</returns>
        /// <exception cref="NullReferenceException">Thrown if no CreatureType is given</exception>
        public ICreature CreateCreature(CreatureTypes type)
        {
            if (type == CreatureTypes.Slime) return new Slime(world);
            if (type == CreatureTypes.Skeleton) return new Skeleton(world);

            throw new NullReferenceException("No CreatureType defined!");
        }
    }
}