using System;
using ADV._SWC___Game_Framework.Interfaces;

namespace ADV._SWC___Game_Framework
{
    /// <summary>
    /// A Class containing Properties & Functions used when you want a Object that can operate like a 'Barrel', 'Crate' or Similar object. Has 2 Derived classes that operate as Items (can be picked up/equipped).
    /// </summary>
    public class WorldObject : IWorldObject
    {
        public static int nextID = 0;
        public bool Lootable = false;

        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public Position position { get; set; }
        public bool Removeable { get; set; }
        public World world { get; set; }

        public WorldObject () { }

        /// <summary>
        /// A function for when a Creature wants to pick up a WorldObject.
        /// </summary>
        /// <param name="creature">The Creature trying to pick up the WorldObject</param>
        /// <exception cref="ArgumentException">Thrown when the WorldObject does not have the 'Lootable' tag</exception>
        /// <returns>the WorldObject to be picked up</returns>
        public WorldObject PickupItem(Creature creature)
        {
            if (!Lootable) throw new ArgumentException("Object is not an Item");

            position = creature.position;
            return this;
        }

        /// <summary>
        /// A function for when a Creature wants to drop a WorldObject.
        /// </summary>
        /// <param name="creature">The Creature trying to drop the WorldObject</param>
        /// <exception cref="ArgumentException">Thrown when the WorldObject does not have the 'Lootable' tag</exception>
        /// <returns>the WorldObject to be dropped</returns>
        public WorldObject DropItem(Creature creature)
        {
            if (!Lootable) throw new ArgumentException("Object is not an Item");

            position = creature.position;
            return this;
        }
    }

    /// <summary>
    /// A derived class from the WorldObject class with additional properties related to an Offensive Item (eg. 'Damage' & 'Range')
    /// </summary>
    public class AttackItem : WorldObject
    {
        public int Damage { get; set; }
        public int Range { get; set; }

        public AttackItem() { Lootable = true; ID = ++nextID; }

        /// <summary>
        /// Constructor of the AttackItem : WorldObject.
        /// </summary>
        /// <param name="name">Name of the Item</param>
        /// <param name="description">Description of the Item</param>
        /// <param name="damage">The Amount of Damage the Item increases by</param>
        /// <param name="range">The Range of the Item</param>
        /// <param name="pos">The Position of the Item</param>
        /// <param name="world1">The World this AttackItem is a part of</param>
        /// <exception cref="ArgumentNullException">Thrown when 'name', 'pos' or 'world1' is 'null'</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when 'damage' or 'range' is a negative value</exception>
        public AttackItem(string name, string description, int damage, int range, Position pos, World world1)
        {
            if (name != null) Name = name; else throw new ArgumentNullException("'name' cannot be 'null'");
            if (description != null) Description = description; else Description = "";

            if (damage < 0) throw new ArgumentOutOfRangeException("'damage' cannot be negative value"); else Damage = damage;

            if (range < 0) throw new ArgumentOutOfRangeException("'range' cannot be negative value"); else Range = range;

            if (pos != null) position = pos; else throw new ArgumentNullException("'pos' cannot be 'null'");

            if (world1 != null) world = world1; else throw new ArgumentNullException("'world1' cannot be 'null'");

            ID = ++nextID;
            Lootable = true;
        }
    }

    /// <summary>
    /// A derived class from the WorldObject class with additional properties related to an Defensive Item (eg. 'Armor')
    /// </summary>
    public class DefenceItem : WorldObject
    {
        public int Armor { get; set; }

        public DefenceItem() { Lootable = true; ID = ++nextID; }

        //Constructor
        /// <summary>
        /// Constructor of the DefenceItem : WorldObject.
        /// </summary>
        /// <param name="name">The Name of the Item</param>
        /// <param name="description">The Description of the Item</param>
        /// <param name="armor">The Amount of reduction to Damage the Item provides</param>
        /// <param name="pos">The Position of the Item</param>
        /// <param name="world1">The World this DefenceItem is a part of</param>
        /// <exception cref="ArgumentNullException">Thrown when 'name', 'pos' or 'world1' is 'null'</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when 'armor' is a negative value</exception>
        public DefenceItem(string name, string description, int armor, Position pos, World world1)
        {
            if (name != null) Name = name; else throw new ArgumentNullException("'name' cannot be 'null'");
            if (description != null) Description = description; else Description = "";

            if (armor < 0) throw new ArgumentOutOfRangeException("'armor' cannot be negative value"); else Armor = armor;

            if (pos != null) position = pos; else throw new ArgumentNullException("'pos' cannot be 'null'");

            if (world1 != null) world = world1; else throw new ArgumentNullException("'world1' cannot be 'null'");

            ID = ++nextID;
            Lootable = true;
        }
    }
}