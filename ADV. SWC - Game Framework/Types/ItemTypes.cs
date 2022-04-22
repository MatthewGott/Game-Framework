using System;
using System.Collections.Generic;

namespace ADV._SWC___Game_Framework.Types
{
    public class Sword : AttackItem
    {
        private static Random rand = new Random();
        private static List<string> SwordNames = new List<string>()
        {
            "Longsword", "Shortsword", "Saber"
        };
        private static List<string> SwordDescriptions = new List<string>()
        {
            "A long blad", "A short blade", "A curved blade"
        };

        public Sword(World world1)
        {
            int name = rand.Next(0, SwordNames.Count);
            Name = SwordNames[name];
            Description = SwordDescriptions[name];
            Damage = rand.Next(2, 4);
            Range = 1;
            position = new Position();
            world = world1;
        }
    }

    public class Bow : AttackItem
    {
        private static Random rand = new Random();
        private static List<string> BowNames = new List<string>()
        {
            "Longbow", "Shortbow", "Crossbow"
        };
        private static List<string> BowDescriptions = new List<string>()
        {
            "A long bow", "A short bow", "An advanced bow"
        };

        public Bow(World world1)
        {
            int name = rand.Next(0, BowNames.Count);
            Name = BowNames[name];
            Description = BowDescriptions[name];
            Damage = rand.Next(2, 4);
            Range = 1;
            position = new Position();
            world = world1;
        }
    }

    public class Helmet : DefenceItem
    {
        private static Random rand = new Random();
        private static List<string> HelmetNames = new List<string>()
        {
            "Bucket", "Sunhat", "Plate Helm"
        };
        private static List<string> HelmetDescriptions = new List<string>()
        {
            "A bucket", "A pretty hat to block out the sun", "A plated helmet"
        };

        public Helmet(World world1)
        {
            int name = rand.Next(0, HelmetNames.Count);
            Name = HelmetNames[name];
            Description = HelmetDescriptions[name];
            Armor = rand.Next(1, 3);
            position = new Position();
            world = world1;
        }
    }

    public class Shield : DefenceItem
    {
        private static Random rand = new Random();
        private static List<string> ShieldNames = new List<string>()
        {
            "Buckler", "Flame Shield", "FrostWall"
        };
        private static List<string> ShieldDescriptions = new List<string>()
        {
            "A small round shield", "A fiery shield", "A very cold shield"
        };

        public Shield(World world1)
        {
            int name = rand.Next(0, ShieldNames.Count);
            Name = ShieldNames[name];
            Description = ShieldDescriptions[name];
            Armor = rand.Next(1, 3);
            position = new Position();
            world = world1;
        }
    }
}