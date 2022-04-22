using System;
using ADV._SWC___Game_Framework.Types;
using ADV._SWC___Game_Framework.Interfaces;

namespace ADV._SWC___Game_Framework.Factories
{
    public class ItemFactory : IFactory
    {
        private static int nextID = 0;
        private World world { get; set; }
        public int ID { get; set; }

        /// <summary>
        /// The constructor for the ItemFactory class
        /// </summary>
        /// <param name="world1">The world the factory is attached to</param>
        public ItemFactory (World world1)
        {
            world = world1;
            ID = ++nextID;
        }

        /// <summary>
        /// Function for the to create an Weapon based on WeaponType
        /// </summary>
        /// <returns>AttackItem</returns>
        /// <exception cref="NullReferenceException">Thrown if no WeaponType is given</exception>
        public IWorldObject CreateWeapon(WeaponTypes type)
        {
            if (type == WeaponTypes.Sword) return new Sword(world);
            if (type == WeaponTypes.Bow) return new Bow(world);

            throw new NullReferenceException("no WeaponType defined!");
        }

        /// <summary>
        /// Function for the to create an Armor based on ArmorType
        /// </summary>
        /// <returns>DefenceItem</returns>
        /// <exception cref="NullReferenceException">Thrown if no ArmorType is given</exception>
        public IWorldObject CreateArmor(ArmorTypes type)
        {
            if (type == ArmorTypes.Shield) return new Shield(world);
            if (type == ArmorTypes.Helmet) return new Helmet(world);

            throw new NullReferenceException("no ArmorType defined!");
        }
    }
}