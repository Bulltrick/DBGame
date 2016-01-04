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
    
    public partial class Tile
    {
        public int TileID { get; set; }
        public int ArmyID { get; set; }
        public int TerrainID { get; set; }
        public int ConstructionID { get; set; }
        public int CityID { get; set; }
        public int MapID { get; set; }
        public Nullable<int> TileX { get; set; }
        public Nullable<int> TileY { get; set; }
    
        public virtual Army Army { get; set; }
        public virtual City City { get; set; }
        public virtual Construction Construction { get; set; }
        public virtual Map Map { get; set; }
        public virtual Terrain Terrain { get; set; }
    }
}
