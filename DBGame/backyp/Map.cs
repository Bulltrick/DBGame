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
    
    public partial class Map
    {
        public Map()
        {
            this.Game = new HashSet<Game>();
            this.Tile = new HashSet<Tile>();
        }
    
        public int MapID { get; set; }
        public string MapName { get; set; }
        public string MapDesc { get; set; }
    
        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Tile> Tile { get; set; }
    }
}
