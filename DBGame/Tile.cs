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
        public Nullable<int> ArmyID { get; set; }
        public int TerrainID { get; set; }
        public Nullable<int> ConstructionID { get; set; }
        public Nullable<int> CityID { get; set; }
        public int MapID { get; set; }
        public Nullable<int> PlayerID { get; set; }
        public int TileX { get; set; }
        public int TileY { get; set; }
    
        public virtual Army Army { get; set; }
        public virtual City City { get; set; }
        public virtual Construction Construction { get; set; }
        public virtual Map Map { get; set; }
        public virtual Player Player { get; set; }
        public virtual Terrain Terrain { get; set; }

        public Tile()
        {

        }

        public Tile(int x, int y)
        {
            this.TileX = x;
            this.TileY = y;
        }

        public Tile(int x, int y, int owner, Terrain ter, Construction con, City city, Army arm)
        {
            //MapID = map;
            this.TileX = x;
            this.TileY = y;
            Terrain = ter;
            //foliage = fol;
            Construction = con;
            this.City = city;
            Army = arm;
            PlayerID = owner;
        }
        public Tile(int map, int x, int y, int owner, Terrain ter, Construction con, City city, Army arm)
        {
            MapID = map;
            this.TileX = x;
            this.TileY = y;
            Terrain = ter;
            //foliage = fol;
            Construction = con;
            this.City = city;
            Army = arm;
            PlayerID = owner;
        }
    }
}
