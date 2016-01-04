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
    
    public partial class Player
    {
        public Player()
        {
            this.Army = new HashSet<Army>();
            this.Game_has_Player = new HashSet<Game_has_Player>();
            this.Tile = new HashSet<Tile>();
        }
    
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
    
        public virtual ICollection<Army> Army { get; set; }
        public virtual ICollection<Game_has_Player> Game_has_Player { get; set; }
        public virtual ICollection<Tile> Tile { get; set; }

        public Player(string name)
        {
            PlayerName = name;
        }
    }
}
