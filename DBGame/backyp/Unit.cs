//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBGame
{
    using System;
    using System.Collections.Generic;
    
    public partial class Unit
    {
        public Unit()
        {
            this.Construction = new HashSet<Construction>();
            this.UnitType = new HashSet<UnitType>();
            this.Upgrade = new HashSet<Upgrade>();
        }
    
        public int UnitID { get; set; }
        public int ArmyID { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> UnitOffense { get; set; }
        public Nullable<int> UnitDefense { get; set; }
        public Nullable<int> UnitRangedDefense { get; set; }
        public Nullable<bool> UnitRanged { get; set; }
        public Nullable<int> UnitHealth { get; set; }
        public Nullable<int> UnitPosition { get; set; }
    
        public virtual Army Army { get; set; }
        public virtual ICollection<Construction> Construction { get; set; }
        public virtual ICollection<UnitType> UnitType { get; set; }
        public virtual ICollection<Upgrade> Upgrade { get; set; }

        public Unit(string nam, int typ, int health, int off, int def, bool ranged, int defVSranged, int pos)
        {
            UnitName = nam;
            //UnitType = typ; not implemented...
            UnitHealth = health;
            UnitOffense = off;
            UnitDefense = def;
            UnitRanged = ranged;
            UnitRangedDefense = defVSranged;
            UnitPosition = pos;
        }

        public void attack(Unit target)
        {
            int? tHP = target.UnitHealth;
            target.loseHP((int)((this.UnitOffense * (100 / (100 + target.UnitDefense))) * (this.UnitHealth / 100.0)));
            this.loseHP((int)((target.UnitOffense * (100 / (100 + this.UnitDefense))) * (tHP / 100.0)));
        }

        public void Rattack(Unit target)
        {
            target.loseHP((int)((this.UnitOffense * (100 / (100 + target.UnitRangedDefense))) * (this.UnitHealth / 100.0)));
        }

        public bool loseHP(int amount)
        {
            this.UnitHealth -= amount;
            if (UnitHealth > 0) return false;
            return true;
        }

    }
}
