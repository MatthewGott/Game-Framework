using System;
using System.Diagnostics;
using ADV._SWC___Game_Framework.Interfaces;
using ADV._SWC___Game_Framework.Types;

namespace ADV._SWC___Game_Framework
{
    /// <summary>
    /// A Class containing Properties & Functions used when you want a Object that can operate like a Game-Character or similar.
    /// </summary>
    public class Creature : ICreature
    {
        public static int nextID = 0;
        public bool IsAlive = true;

        public int ID { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public Position position { get; set; }
        public DefenceItem DefensiveItem { get; set; }
        public AttackItem OffensiveItem { get; set; }
        public World world { get; set; }
        private int MaxHitPoints { get; set; }

        public Creature() { MaxHitPoints = HitPoints; ID = ++nextID; }

        /// <summary>
        /// Constructor for the Creature class.
        /// </summary>
        /// <param name="name">Name of the Creature</param>
        /// <param name="hitpoints">Amount of 'HitPoints' or 'Life' the Creature will have</param>
        /// <param name="damage">Amount of 'Base-Damage' the Creature will deal (Without Items)</param>
        /// <param name="pos">Current Position of the Creature</param>
        /// <param name="world1">The World that this Creature is a part of</param>
        /// <exception cref="ArgumentNullException">Thrown when 'name' or 'pos' is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the 'name' is greater than 64 characters long, when 'hitpoints' is less than 1, or when 'damage' is a negative value</exception>
        public Creature (string name, int hitpoints, int damage, Position pos, World world1) {
            if (name == null) throw new ArgumentNullException("You must assign a 'Name' to the Creature");
            if (name.Length <= 64) Name = name; else throw new ArgumentOutOfRangeException("'Name' must be less than 64 characters");

            if (hitpoints >= 1) { HitPoints = hitpoints; MaxHitPoints = hitpoints; } 
            else throw new ArgumentOutOfRangeException("HitPoints cannot be less than 1");

            if (damage >= 0) Damage = damage; else throw new ArgumentOutOfRangeException("Damage cannot be a negative value!");

            if (pos == null) throw new ArgumentNullException("'Position' cannot be 'null'"); else position = pos;

            if (world1 == null) throw new ArgumentNullException("'world1' cannot be 'null'"); else world = world1;
            ID = ++nextID;
        }

        /// <summary>
        /// Calculated damage from this Creature & OffensiveItem (if any) and 'sends' this damage to the target Creature.
        /// </summary>
        /// <param name="Target">The Creature who will receive Damage to their HitPoints</param>
        /// <exception cref="ArgumentNullException">Thrown when 'Target' is null</exception>
        public void Hit(Creature Target)
        {
            if (Target == null) throw new ArgumentNullException("Target cannot be 'null'");

            int TotalDamage = Damage;
            if (OffensiveItem != null) TotalDamage += OffensiveItem.Damage;

            Target.ReceiveHit(TotalDamage);
            world.TS.TraceEvent(TraceEventType.Information,0,$"{Name}[{ID}] hit {Target.Name}[{Target.ID}] for {TotalDamage}");
        }

        /// <summary>
        /// Function for when a Creature is receiving 'Damage', and if a DefensiveItem is equipped on Creature reduce this damage.
        /// </summary>
        /// <param name="damage">The Amount of HitPoints the Creature is supposed to be losing (before armor/defensive calculations)</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when 'damage' is less than 0 (if a negative damage or 'healing' is the goal of a certain attack, use the 'Heal()' & 'ReceiveHeal()' functions)</exception>
        public void ReceiveHit(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException("Damage cannot be a negative value");

            int DamageToTake = damage;
            if (DefensiveItem != null) DamageToTake -= DefensiveItem.Armor;

            if (DamageToTake > 0) HitPoints -= DamageToTake;
            if (HitPoints <= 0) { IsAlive = false; Death(); }
        }

        /// <summary>
        /// Checks if the Creature has any items equipped & drops these, afterwards the Creature will be Removed from the 'World' lists.
        /// </summary>
        public void Death() {
            if (OffensiveItem != null) { OffensiveItem.DropItem(this); OffensiveItem = null; }
            if (DefensiveItem != null) { DefensiveItem.DropItem(this); DefensiveItem = null; }
            world.TS.TraceEvent(TraceEventType.Information, 0, $"{Name}[{ID}] has Died!");
            world.DestroyCreature(this);
        }

        /// <summary>
        /// A function to enable a Creature to 'Heal' other Creatures, restoring HitPoints to the target (Can also be used on itself).
        /// </summary>
        /// <param name="Target">The Creature who will receive the HitPoints</param>
        /// <param name="value">Amount of HitPoints the 'target' is supposed to gain</param>
        /// <exception cref="ArgumentNullException">Thrown when 'Target' is null</exception>
        public void Heal(Creature Target, int value)
        {
            if (Target == null) throw new ArgumentNullException("Target cannot be 'null'");

            Target.ReceiveHeal(value);
            world.TS.TraceEvent(TraceEventType.Information, 0, $"{Name}[ID:{ID}] Healed {Target.Name}[{Target.ID}]");
        }

        /// <summary>
        /// Function for when a Creature is receiving 'Healing'.
        /// </summary>
        /// <param name="value">The amount of HitPoints to be gained</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when 'value' is less than 0 (if loss of HitPoints is the goal use the 'Hit()' & 'ReceiveHit()' functions)</exception>
        public void ReceiveHeal(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("Value cannot be negative");

            if (HitPoints + value >= MaxHitPoints) HitPoints = MaxHitPoints;
            else HitPoints += value;
        }

        /// <summary>
        /// A Function to pick up WorldObject from the World, if an WorldObject of the same type as the target WorldObject is already equipped, the 'equipped' WorldObject will be dropped before picking up the new one.
        /// </summary>
        /// <param name="Target">The item to be picked up</param>
        /// <exception cref="ArgumentNullException">Thrown when 'Target' is null</exception>
        /// <exception cref="ArgumentException">Thrown if the 'Target' does not have to 'Lootable' tag (eg. the WorldObject is NOT of types 'AttackItem' or 'DefenceItem')</exception>
        public void Loot(WorldObject Target)
        {
            if (Target == null) throw new ArgumentNullException("No item found");

            if (!Target.Lootable) throw new ArgumentException("Target is not an item"); ;

            if (Target.GetType() == typeof(Bow) || Target.GetType() == typeof(Sword)) {

                if (OffensiveItem != null) { 
                    OffensiveItem.DropItem(this); 
                    OffensiveItem = (AttackItem)Target.PickupItem(this);
                }
                else OffensiveItem = (AttackItem)Target.PickupItem(this);
                world.TS.TraceEvent(TraceEventType.Information, 0, $"{Name}[{ID}] picked up {Target.Name}[{Target.ID}]");
            }
            else if (Target.GetType() == typeof(Helmet) || Target.GetType() == typeof(Shield)) {

                if (DefensiveItem != null) { DefensiveItem.DropItem(this); DefensiveItem = (DefenceItem)Target.PickupItem(this); }
                else DefensiveItem = (DefenceItem)Target.PickupItem(this);
                world.TS.TraceEvent(TraceEventType.Information, 0, $"{Name}[{ID}] picked up {Target.Name}[{Target.ID}]");
            }
        }
    }
}