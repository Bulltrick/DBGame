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
    
    public partial class Game
    {
        public Game()
        {
            this.Game_has_Player = new HashSet<Game_has_Player>();
        }
    
        public int GameID { get; set; }
        public int Map_MapID { get; set; }
        public Nullable<int> GameActivePlayerID { get; set; }
    
        public virtual ICollection<Game_has_Player> Game_has_Player { get; set; }
        public virtual Map Map { get; set; }

        public Game(int mapid)
        {
            Map_MapID = mapid;
        }
    }
}
