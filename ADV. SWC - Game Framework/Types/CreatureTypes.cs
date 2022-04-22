using System;
using System.Collections.Generic;

namespace ADV._SWC___Game_Framework.Types
{
    public class Skeleton : Creature
    {
        private static Random rand = new Random();
        private static List<string> SkeletonNames = new List<string>()
        {
            "Ragged Skeleton", "Sturdy Skeleton", "Ethereal Skeleton"
        };

        public Skeleton(World world1)
        {
            Name = SkeletonNames[rand.Next(0, SkeletonNames.Count)];
            HitPoints = rand.Next(25, 35);
            Damage = rand.Next(5,8);
            position = new Position();
            world = world1;
        }
    }

    public class Slime : Creature
    {
        private static Random rand = new Random();
        static List<string> SlimeNames = new List<string>()
        {
            "Green Slime", "Blue Slime", "Red Slime"
        };

        public Slime(World world1)
        {
            Name = SlimeNames[rand.Next(0, SlimeNames.Count)];
            HitPoints = rand.Next(18,26);
            Damage = rand.Next(3, 6);
            position = new Position();
            world = world1;
        }
    }
}