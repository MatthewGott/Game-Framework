using System;
using ADV._SWC___Game_Framework;

namespace FrameWorkTestApp
{
    class Program
    {
        static World world;
        static void Main(string[] args)
        {
            world = new World(16, 16);
            Console.Write("\n--- FactoryDemo Start ---\n\n");
            FactoryDemo();
            Console.Write("\n--- FactoryDemo End ---\n");

            Console.ReadKey();

            Console.Write("\n--- PickupDemo Start ---\n\n");
            PickupDemo();
            Console.Write("\n--- PickupDemo End ---\n");

            Console.ReadKey();

            Console.Write("\n--- CombatDemo Start ---\n\n");
            CombatDemo();
            Console.Write("\n--- CombatDemo End ---\n");

            Console.ReadKey();
        }

        static void FactoryDemo()
        {
            world.CreateCreatureFactory();
            world.CreateItemFactory();
            world.SpawnCreature(1, CreatureTypes.Slime);
            world.SpawnCreature(1, CreatureTypes.Skeleton);
            world.SpawnItem(1, WeaponTypes.Sword);
            world.SpawnItem(1, WeaponTypes.Bow);
            world.SpawnItem(1, ArmorTypes.Helmet);
            world.SpawnItem(1, ArmorTypes.Shield);
        }

        static void PickupDemo()
        {
            Creature creature1 = world.WorldCreatures[0];
            Creature creature2 = world.WorldCreatures[1];

            creature1.Loot(world.WorldObjects[0]);
            creature1.Loot(world.WorldObjects[2]);
            creature2.Loot(world.WorldObjects[1]);
            creature2.Loot(world.WorldObjects[3]);
        }

        static void CombatDemo()
        {
            Creature creature1 = world.WorldCreatures[0];
            Creature creature2 = world.WorldCreatures[1];
            while (true)
            {
                if (!creature1.IsAlive || !creature2.IsAlive) break;
                creature1.Hit(creature2);
                creature2.Hit(creature1);
                Console.ReadKey();
            }
        }
    }
}