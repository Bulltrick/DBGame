using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBGame
{
    /// <summary>
    /// Interaction logic for MapEditor.xaml
    /// </summary>
    public partial class MapEditor : Window
    {
        List<Tile> map;
        Tile activeTile;
        Tile lastActiveTile;
        int mapID;
        string oldMapName;

        public MapEditor()
        {
            InitializeComponent();
            fillTemplate();
            map = new List<Tile>();
            mapID = -1;

            using (G2776_2Entities1 db = new G2776_2Entities1())
            {
                
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    map.Add(new Tile(i, j, -3, new Terrain(1, "Plains", "LightYellow"), null, null, null));
                }

            }
            drawMap();

        }

        public MapEditor(int mappi)
        {
            InitializeComponent();
            fillTemplate();
            map = new List<Tile>();
            mapID = mappi;
            using (G2776_2Entities1 db = new G2776_2Entities1())
            {
                var m = from b in db.Map.Include("Tile")
                        where b.MapID == mappi
                        select b;
                foreach (Map c in m)
                {
                    oldMapName = c.MapName;
                    txtMapname.Text = c.MapName;
                }

            }
            try
            {
                using (G2776_2Entities1 db = new G2776_2Entities1())
                {

                    var tiles = from a in db.Tile
                                where a.MapID == mappi
                                select a;

                    foreach (Tile t in tiles)
                    {
                        if (t.PlayerID != null)
                        {
                            using (G2776_2Entities1 player = new G2776_2Entities1())
                            {
                                var pla = (from pl in player.Player
                                           where pl.PlayerID == t.PlayerID
                                           select pl).First();
                                t.Player = pla;
                            }
                        }
                        if (t.TerrainID != null)
                        {
                            using (G2776_2Entities1 terrain = new G2776_2Entities1())
                            {
                                var ter = (from te in terrain.Terrain
                                           where te.TerrainID == t.TerrainID
                                           select te).First();
                                t.Terrain = ter;
                            }
                        }
                        if (t.ConstructionID != null)
                        {
                            using (G2776_2Entities1 construction = new G2776_2Entities1())
                            {
                                var con = (from co in construction.Construction
                                           where co.ConstructionID == t.ConstructionID
                                           select co).First();
                                t.Construction = con;
                            }
                        }
                        if (t.CityID != null)
                        {
                            using (G2776_2Entities1 city = new G2776_2Entities1())
                            {
                                var cit = (from ci in city.City
                                           where ci.CityID == t.CityID
                                           select ci).First();
                                t.City = cit;
                            }
                        }
                        if (t.ArmyID != null)
                        {
                            using (G2776_2Entities1 army = new G2776_2Entities1())
                            {
                                var arm = (from ar in army.Army.Include("Unit")
                                           where ar.ArmyID == t.ArmyID
                                           select ar).First();
                                t.Army = arm;
                                foreach (Unit u in arm.Unit)
                                {
                                    arm.AddUnit(u);
                                }
                            }
                            
                            /*
                            using (G2776_2Entities1 units = new G2776_2Entities1())
                            {
                                var uni = from un in units.Unit
                                          where un.ArmyID == t.ArmyID
                                          select un;
                                foreach (Unit unit in uni)
                                {
                                    t.Army.Unit.Add(new Unit(unit.UnitName,unit.UnitHealth,(int)unit.UnitOffense,(int)unit.UnitDefense,unit.UnitRanged,(int)unit.UnitRangedDefense,(int)unit.UnitPosition));
                                    //t.Army.Unit.Add(unit);
                                }
                            }*/
                        }
                        /*
                        Tile temp = new Tile(t.TileX,t.TileY);
                        if (t.PlayerID != null)
                        {
                            temp.PlayerID = t.PlayerID;
                        }
                    
                        if (t.Terrain != null)
                        {
                            temp.Terrain = t.Terrain;
                            temp.TerrainID = t.TerrainID;
                        }
                        if (t.Construction != null)
                        {
                            temp.Construction = t.Construction;
                            temp.ConstructionID = t.ConstructionID;
                        }
                        if (t.City != null)
                        {
                            temp.City = t.City;
                            temp.CityID = t.CityID;
                        }
                        if (t.Army != null)
                        {
                            temp.Army = t.Army;
                            temp.ArmyID = t.ArmyID;
                        }
                        map.Add(temp);
                         * */
                        map.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
            drawMap();
        }

        private void drawMap()
        {


            foreach (Tile t in map)
            {
                Button btn = new Button();
                string content = "";

                if (t.Army != null) content += "A ";
                if (t.City != null) content += "C ";
                if (t.Construction != null) content += "B ";

                content += "                 £x" + t.TileX + " £y" + t.TileY + " ";

                if (t.Terrain != null)
                {
                    btn.Background = new SolidColorBrush(t.Terrain.getColor());  // string -> Color
                }


                btn.Content = content;
                btn.Height = 45;
                btn.Width = 45;
                Grid.SetColumn(btn, t.TileY);
                Grid.SetRow(btn, t.TileX);
                gridMap.Children.Add(btn);
                btn.Click += btn_Click;
                switch (t.PlayerID)
                {
                    case -3: btn.Foreground = new SolidColorBrush(Colors.Gray);
                        break;
                    case -1: btn.Foreground = new SolidColorBrush(Colors.Green);
                        break;
                    case -2: btn.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    default: btn.Foreground = new SolidColorBrush(Colors.Pink);
                        break;
                }
                
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            lastActiveTile = activeTile;

            lblInfo1.Content = "";
            lblInfo2.Content = "";
            lblInfo3.Content = "";
            lblInfo4.Content = "";
            lblInfo5.Content = "";


            Button btn = (Button)e.OriginalSource;
            string apu = btn.Content.ToString();
            int apux = 0;
            int apuy = 0;
            int.TryParse(apu.Substring(apu.IndexOf("£x") + 2, 2), out apux);
            int.TryParse(apu.Substring((apu.IndexOf("£y") + 2), 2), out apuy);

            foreach (Tile t in map)
            {
                if (t.TileX == apux && t.TileY == apuy) activeTile = t;
            }
            refreshInfo();
        }

        private void fillTemplate() {
            cmbOwner.Items.Add("Player 1");
            cmbOwner.Items.Add("Player 2");
            cmbOwner.Items.Add("Neutral");

            cmbTerrainType.Items.Add("Mountain");
            cmbTerrainType.Items.Add("Plains");
            cmbTerrainType.Items.Add("Forest");
            cmbTerrainType.Items.Add("Lake");
            cmbTerrainType.Items.Add("Hills");
            

            cmbConstructionType.Items.Add("Barracks");
            cmbConstructionType.Items.Add("Archery Range");
            cmbConstructionType.Items.Add("Stables");

            cmbUnit1.Items.Add("Swordman");
            cmbUnit1.Items.Add("Archer");
            cmbUnit1.Items.Add("Horseman");

            cmbUnit2.Items.Add("Swordman");
            cmbUnit2.Items.Add("Archer");
            cmbUnit2.Items.Add("Horseman");

            cmbUnit3.Items.Add("Swordman");
            cmbUnit3.Items.Add("Archer");
            cmbUnit3.Items.Add("Horseman");

            cmbUnit4.Items.Add("Swordman");
            cmbUnit4.Items.Add("Archer");
            cmbUnit4.Items.Add("Horseman");

            cmbUnit5.Items.Add("Swordman");
            cmbUnit5.Items.Add("Archer");
            cmbUnit5.Items.Add("Horseman");
        }
        
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {

            switch (cmbOwner.SelectedIndex)
            {
                case 0: activeTile.PlayerID = -1;
                    break;
                case 1: activeTile.PlayerID = -2;
                    break;
                case 2: activeTile.PlayerID = -3;
                    break;
                default: activeTile.PlayerID = -3;
                    break;
            }
            switch (cmbTerrainType.SelectedIndex)
            {
                case 1: activeTile.Terrain = new Terrain(1, "Plains", "LightYellow");
                    break;
                case 2: activeTile.Terrain = new Terrain(2, "Forest", "LawnGreen");
                    break;
                case 3: activeTile.Terrain = new Terrain(3, "Lake", "Blue");
                    break;
                case 4: activeTile.Terrain = new Terrain(4, "Hills", "Gray");
                    break;
                case 0: activeTile.Terrain = new Terrain(0, "Mountain", "WhiteSmoke");
                    break;
                default: MessageBox.Show("Select Terrain type from the combobox");
                    break;
            }

            double efficiency;
            if (double.TryParse(txtConstEfficiency.Text, out efficiency))
            {
                switch (cmbConstructionType.SelectedIndex)
                {
                    case 0: activeTile.Construction = new Construction("Barracks", efficiency, new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Construction = new Construction("Archery Range", efficiency, new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Construction = new Construction("Stables", efficiency, new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default: 
                        break;
                }
              
            }

            if (checkArmy.IsChecked == true)
            {
                activeTile.Army = new Army(txtArmy.Text, new List<Unit>(), (int)activeTile.PlayerID);

                switch (cmbUnit1.SelectedIndex)
                {
                    case 0: activeTile.Army.AddUnit(new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Army.AddUnit(new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Army.AddUnit(new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default:
                        break;
                }
                switch (cmbUnit2.SelectedIndex)
                {
                    case 0: activeTile.Army.AddUnit(new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Army.AddUnit(new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Army.AddUnit(new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default:
                        break;
                }
                switch (cmbUnit3.SelectedIndex)
                {
                    case 0: activeTile.Army.AddUnit(new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Army.AddUnit(new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Army.AddUnit(new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default:
                        break;
                }
                switch (cmbUnit4.SelectedIndex)
                {
                    case 0: activeTile.Army.AddUnit(new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Army.AddUnit(new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Army.AddUnit(new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default:
                        break;
                }
                switch (cmbUnit5.SelectedIndex)
                {
                    case 0: activeTile.Army.AddUnit(new Unit("Swordman", 100, 75, 75, false, 75, 1));
                        break;
                    case 1: activeTile.Army.AddUnit(new Unit("Archer", 100, 80, 25, true, 50, 2));
                        break;
                    case 2: activeTile.Army.AddUnit(new Unit("Horseman", 100, 90, 50, false, 50, 1));
                        break;
                    default:
                        break;
                }
                //if (activeTile.Army.Unit.Count <= 0) activeTile.Army = null;
            }
            

            drawMap();
        }
    
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbConstructionType.SelectedIndex = -1;
            cmbTerrainType.SelectedIndex = -1;
            cmbUnit1.SelectedIndex = -1;
            cmbUnit2.SelectedIndex = -1;
            cmbUnit3.SelectedIndex = -1;
            cmbUnit4.SelectedIndex = -1;
            cmbUnit5.SelectedIndex = -1;
        }

        private void refreshInfo()
        {
            lblInfo1.Content = "Tile Coordinates: " + activeTile.TileX + "." + activeTile.TileY;
            if (activeTile.Terrain != null) lblInfo2.Content = "Terrain: " + activeTile.Terrain.TerrainName;
            if (activeTile.Construction != null)
            {
                lblInfo3.Content = "Construtction: " + activeTile.Construction.ConstructionName + " Efficiency: " + activeTile.Construction.ConstructionEfficiency;
            }

            if (activeTile.Army != null)
            {
                lblInfo4.Content = "Army: " + activeTile.Army.ArmyName;
                txtBoxArmy.Text += "Player " + activeTile.PlayerID + "\n Name: " + activeTile.Army.ArmyName + "\n\n";
                foreach (Unit u in activeTile.Army.Unit)
                {
                    txtBoxArmy.Text += "Unit: " + u.UnitName + "\n HP: " + u.UnitHealth + "\n Attack: " + u.UnitOffense + " Defense: " + u.UnitDefense + "\n\n";
                }
            }
        }

        private void btnSaveAndExit_Click(object sender, RoutedEventArgs e)
        {
            //int mapID = -1;
            try
            {
                using (G2776_2Entities1 db = new G2776_2Entities1())
                {
                    if (mapID < 0 || txtMapname.Text != oldMapName)
                    {
                        if (txtMapname.Text != null && txtMapname.Text != "")
                        {
                            db.Map.Add(new Map(txtMapname.Text));
                        }
                        else
                        {
                            MessageBox.Show("Give a name for the map");
                            return;
                        }
                        db.SaveChanges();

                        var mappi = from m in db.Map
                                    orderby m.MapID descending
                                    select m;
                        foreach (var a in mappi)
                        {
                            mapID = a.MapID;
                            break;
                        }
                    }
                    

                    foreach (Tile t in map)
                    {
                        t.MapID = mapID;
                        /*
                        if (t.Army != null)
                        {
                            db.Army.Add(t.Army);
                            foreach (Unit u in t.Army.Unit)
                            {
                                db.Unit.Add(u);
                            }
                        }
                        if (t.City != null)
                        {
                            db.City.Add(t.City);
                        }
                        if (t.Construction != null)
                        {
                            db.Construction.Add(t.Construction);
                        }
                        if (t.Terrain != null)
                        {
                            db.Terrain.Add(t.Terrain);
                        }*/
                        var player = (from p in db.Player
                                     where p.PlayerID == t.PlayerID
                                     select p).First();
                        t.Player = player;

                        var terrain = (from ter in db.Terrain
                                       where ter.TerrainType == t.Terrain.TerrainType
                                       select ter).First();
                        t.Terrain = terrain;
                        db.Tile.Add(t);
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("Map saved successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbOwner_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
