using System;
using System.Collections.Generic;
using System.Diagnostics;
using ADV._SWC___Game_Framework.Factories;

namespace ADV._SWC___Game_Framework
{
    /// <summary>
    /// A class that encompasses and contains lists of all the Active Creature's & WorldObject's in the Game. Also Contains functions for 'Spawning'(creating) these Objects.
    /// </summary>
    public class World
    {
        public TraceSource TS { get; set; }
        public ConsoleTraceListener Listener { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<Creature> WorldCreatures { get; set; }
        public List<WorldObject> WorldObjects { get; set; }
        public List<CreatureFactory> CreatureFactories { get; set; }
        public List<ItemFactory> ItemFactories { get; set; }

        /// <summary>
        /// Primary Constructor for the World object type.
        /// </summary>
        /// <param name="maxX">Maximum world size on the X-axis</param>
        /// <param name="maxY">Maximum world size on the Y-axis</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if 'maxX' or 'maxY' is lower than 8</exception>
        public World(int maxX, int maxY)
        {
            if (maxX < 8 || maxY < 8) throw new ArgumentOutOfRangeException("'maxX' & 'maxY' must be at least '8' or higher");
            MaxX = maxX;
            MaxY = maxY;

            WorldCreatures = new List<Creature>();
            WorldObjects = new List<WorldObject>();
            CreatureFactories = new List<CreatureFactory>();
            ItemFactories = new List<ItemFactory>();

            TS = new TraceSource("TS");
            Listener = new ConsoleTraceListener();
            TS.Listeners.Add(Listener);
            TS.Switch = new SourceSwitch("Info", "All");
            TS.TraceEvent(TraceEventType.Information, 0, "Created World");
        }

        /// <summary>
        /// Expanded Constructor for the World object type that can be used to 'Load' lists of Creatures & WorldObjects.
        /// </summary>
        /// <param name="maxX">Maximum world size on the X-axis</param>
        /// <param name="maxY">Maximum world size on the Y-axis</param>
        /// <param name="creatureList">List of Creatures to assign to the World</param>
        /// <param name="objectList">List of WorldObjects to assign to the World</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if 'maxX' or 'maxY' is lower than 8</exception>
        /// <exception cref="ArgumentNullException">Thrown if 'creatureList' or 'objectList' is 'null'</exception>
        public World(int maxX, int maxY, List<Creature> creatureList, List<WorldObject> objectList)
        {
            if (maxX < 8 || maxY < 8) throw new ArgumentOutOfRangeException("'maxX' & 'maxY' must be at least '8' or higher");
            MaxX = maxX;
            MaxY = maxY;
            if (creatureList == null || objectList == null) throw new ArgumentNullException("'creatureList' & 'objectList' cannot be 'null' (use the simpler constructor instead)");
            WorldCreatures = creatureList;
            WorldObjects = objectList;
            TS = new TraceSource("TS");
            Listener = new ConsoleTraceListener();
            TS.Listeners.Add(Listener);
            TS.Switch = new SourceSwitch("Info", "All");
            TS.TraceEvent(TraceEventType.Information, 0, "Created World with existing Creature & WorldObject Lists");
        }

        /// <summary>
        /// Function for creating a new CreatureFactory
        /// </summary>
        public void CreateCreatureFactory()
        {
            CreatureFactory newFactory = new CreatureFactory(this);
            CreatureFactories.Add(newFactory);
            TS.TraceEvent(TraceEventType.Information, 0, $"Created CreatureFactory[{newFactory.ID}]");
        }

        /// <summary>
        /// Function for creating a new ItemFactory
        /// </summary>
        public void CreateItemFactory()
        {
            ItemFactory newFactory = new ItemFactory(this);
            ItemFactories.Add(newFactory);
            TS.TraceEvent(TraceEventType.Information, 0, $"Created ItemFactory[{newFactory.ID}]");
        }

        /// <summary>
        /// Function for Spawning a Creature
        /// </summary>
        /// <param name="factory">The ID of the Factory</param>
        public void SpawnCreature(int id, CreatureTypes type)
        {
            CreatureFactory factory = null;
            foreach (CreatureFactory fact in CreatureFactories) if (fact.ID == id) factory = fact;
            Creature creature = (Creature)factory.CreateCreature(type);
            WorldCreatures.Add(creature);
            TS.TraceEvent(TraceEventType.Information, 0, $"CreatureFactory[{factory.ID}] Created new Creature: {creature.Name}[{creature.ID}]");
        }

        /// <summary>
        /// Function for Spawning an AttackItem
        /// </summary>
        /// <param name="id">The ID of the Factory</param>
        /// <param name="type">The type of Weapon to spawn</param>
        public void SpawnItem(int id, WeaponTypes type)
        {
            ItemFactory factory = null;
            foreach (ItemFactory fact in ItemFactories) if (fact.ID == id) factory = fact;
            AttackItem item = (AttackItem)factory.CreateWeapon(type);
            WorldObjects.Add(item);
            TS.TraceEvent(TraceEventType.Information, 0, $"ItemFactory[{factory.ID}] Created new Weapon: {item.Name}[{item.ID}]");
        }

        /// <summary>
        /// Function for Spawning an DefenceItem
        /// </summary>
        /// <param name="id">The ID of the Factory</param>
        /// <param name="type">The type of Weapon to spawn</param>
        public void SpawnItem(int id, ArmorTypes type)
        {
            ItemFactory factory = null;
            foreach (ItemFactory fact in ItemFactories) if (fact.ID == id) factory = fact;
            DefenceItem item = (DefenceItem)factory.CreateArmor(type);
            WorldObjects.Add(item);
            TS.TraceEvent(TraceEventType.Information, 0, $"ItemFactory[{factory.ID}] Created new Armor: {item.Name}[{item.ID}]");
        }

        /// <summary>
        /// Function for Destroying a Creature & removing it from the 'WorldCreature' list.
        /// </summary>
        /// <param name="creature">The Creature to be Removed</param>
        public void DestroyCreature(Creature creature)
        {
            TS.TraceEvent(TraceEventType.Information, 0, $"Removed {creature.Name}[{creature.ID}]");
            WorldCreatures.Remove(creature);
        }

        /// <summary>
        /// Function for Destroying a WorldObject & removing it from the 'WorldObjects' list.
        /// </summary>
        /// <param name="obj">The WorldObject to be Removed</param>
        public void DestroyWorldObject(WorldObject obj)
        {
            TS.TraceEvent(TraceEventType.Information, 0, $"Removed {obj.Name}[{obj.ID}]");
            WorldObjects.Remove(obj);
        }
    }
}