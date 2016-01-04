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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Tile activeTile;
        private Tile lastActiveTile;
        private List<Tile> map;
        private bool movingArmy;
        private int activePlayer;

        public MainWindow()
        {
            InitializeComponent();

            /*
            List<Unit> uniitit = new List<Unit>();
            uniitit.Add(new Unit("Unit1", 1, 100, 100, 50,false,5,1));

            List<Unit> uniitit2 = new List<Unit>();
            uniitit2.Add(new Unit("Unit2", 1, 100, 60, 80, false, 5, 1));

            List<Unit> uniitit3 = new List<Unit>();
            uniitit3.Add(new Unit("Unit4", 1, 100, 10, 105, false, 5, 1));
            */

            map = new List<Tile>();

            /*
             * var queryMap = 
             * til in tiles
             * where til.MapID == 1 
             * select til;
             * 
             * foreach (var til in queryMap) 
             * {
             *  map.Add(new Tile(til.x,til.y,til.owner,new Terrain(
             * }
             * 
             * 
             */
            // SELECT * FROM Tile WHERE MapID = 1;
/*
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    map.Add(new Tile(i,j,0,new Terrain(1,"Plains","LightYellow"),null,null,null));
                }
                
            }
            map.Add(new Tile(0, 14,1, new Terrain(1, "Plains", "LightYellow"), null, null, new Army("Army1", uniitit, 1)));
            map.Add(new Tile(1, 14, 1, new Terrain(1, "Plains", "LightYellow"), null, null, new Army("Army2", uniitit2, 1)));
            map.Add(new Tile(2, 14, 2, new Terrain(1, "Plains", "LightYellow"), null, null, new Army("Army3", uniitit3, 2)));

            map.Add(new Tile(3, 14,1, new Terrain(2, "Forest", "LawnGreen"), new Construction("Barracks", 1, 1.0, new Unit("Miekkamies", 1, 100, 50, 50, false, 75, 1), 1), null, null));
            map.Add(new Tile(4, 14,1, new Terrain(2, "Forest", "LawnGreen"), new Construction("Archery Range", 1, 1.0, new Unit("Jousimies", 1, 100, 75, 10, true, 30, 1), 1), null, null));
            map.Add(new Tile(5, 14,2, new Terrain(2, "Forest", "LawnGreen"), new Construction("Stables", 1, 1.0, new Unit("Ratsumies", 1, 100, 85, 50, false, 25, 1), 2), null, null));

            map.Add(new Tile(6, 14, 2, new Terrain(3, "Lake", "Blue"), new Construction("Stables", 1, 1.0, new Unit("Ratsumies", 1, 100, 85, 50, false, 25, 1), 2), null, null));
            map.Add(new Tile(7, 14, 2, new Terrain(4, "Hill", "Gray"), new Construction("Stables", 1, 1.0, new Unit("Ratsumies", 1, 100, 85, 50, false, 25, 1), 2), null, null));
            map.Add(new Tile(8, 14, 2, new Terrain(0, "Mountain", "WhiteSmoke"), new Construction("Stables", 1, 1.0, new Unit("Ratsumies", 1, 100, 85, 50, false, 25, 1), 2), null, null));

            activePlayer = 1;
            */
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
                    switch (t.Terrain.TerrainType)
                    {
                        case 1: btn.Background = new SolidColorBrush(Colors.LightYellow);
                            break;
                        case 2: btn.Background = new SolidColorBrush(Colors.LawnGreen);
                            break;
                        case 3: btn.Background = new SolidColorBrush(Colors.Blue);
                            break;
                        case 4: btn.Background = new SolidColorBrush(Colors.Gray);
                            break;
                        case 0: btn.Background = new SolidColorBrush(Colors.WhiteSmoke);
                            break;
                        default:
                            break;
                    }
                }


                btn.Content = content;
                btn.Height = 45;
                btn.Width = 45;
                Grid.SetColumn(btn, (int)t.TileX);
                Grid.SetRow(btn, (int)t.TileY);
                gridMap.Children.Add(btn);
                btn.Click += btn_Click;
                if (t.PlayerID == activePlayer) btn.Foreground = new SolidColorBrush(Colors.Green);
                else if (t.PlayerID == null) btn.Foreground = new SolidColorBrush(Colors.Gray);
                else btn.Foreground = new SolidColorBrush(Colors.Red);
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
            txtBoxArmy.Text = "";

            Button btn = (Button)e.OriginalSource;
            string apu = btn.Content.ToString();
            int apux = 0;
            int apuy = 0;
            int.TryParse(apu.Substring(apu.IndexOf("£x")+2, 2), out apux);
            int.TryParse(apu.Substring((apu.IndexOf("£y") + 2),2 ), out apuy);

            foreach (Tile t in map)
            {
                if (t.TileX == apux && t.TileY == apuy) activeTile = t;
            }

            

            /*
            if (movingArmy)
            {
                if (activeTile.Terrain.TerrainType > 0 && activeTile != lastActiveTile && lastActiveTile.TileX +1 >= activeTile.TileX && activeTile.TileX >= lastActiveTile.TileX - 1 && lastActiveTile.TileY + 1 >= activeTile.TileY && activeTile.TileY >= lastActiveTile.TileY - 1)
                {
                    if (activeTile.Army != null && activeTile.Army.PlayerID == activePlayer) //armeijoiden yhdistämienn
                    {
                        foreach (Unit u in lastActiveTile.Army.Unit)
                        {
                            activeTile.Army.AddUnit(u);
                        }
                        lastActiveTile.Army = null;
                        activeTile.Army.ArmyMovement--;
                    }
                    else if (activeTile.Army != null) //armeijalla hyökkääminen
                    {
                        lastActiveTile.Army.Attack(activeTile.Army);
                        lastActiveTile.Army.ArmyMovement--;
                        bool attackerDead = lastActiveTile.Army.CheckHP();
                        if (attackerDead)
                        {
                            lastActiveTile.Army = null;
                        }
                        if (activeTile.Army.CheckHP())
                        {
                            activeTile.Army = null;
                            if (!attackerDead)
                            {
                                activeTile.Army = lastActiveTile.Army;
                                lastActiveTile.Army = null;
                            }
                        }
                    }
                    else //armeijalla liikkuminen
                    {
                        activeTile.Army = lastActiveTile.Army;
                        lastActiveTile.Army = null;
                        activeTile.PlayerID = activePlayer;
                        activeTile.Army.ArmyMovement--;
                        if (activeTile.Construction != null) activeTile.PlayerID = activePlayer;
                        if (activeTile.City != null) activeTile.PlayerID = activePlayer;
                    }
                    
                    drawMap();
                }
                else MessageBox.Show("Illegal movement!");
             
                movingArmy = false;
            }*/



            refreshInfo();
        }

        private void refreshInfo()
        {
            lblInfo1.Content = "Tile Coordinates: " + activeTile.TileX + "." + activeTile.TileY;
            if (activeTile.Terrain != null) lblInfo2.Content = "Terrain: " + activeTile.Terrain.TerrainName;
            if (activeTile.Construction != null)
            {
                lblInfo3.Content = "Construtction: " + activeTile.Construction.ConstructionName + " Efficiency: " + activeTile.Construction.ConstructionEfficiency;
                if (activeTile.PlayerID == activePlayer)
                {
                    lblInfo3.Content += " Progress: " + activeTile.Construction.ConstructionProgress;
                }
            }

            if (activeTile.Army != null)
            {
                lblInfo4.Content = "Army: " + activeTile.Army.ArmyName;
                txtBoxArmy.Text += "Player " + activeTile.Army.PlayerID + "\n Name: " + activeTile.Army.ArmyName + "\n\n";
                foreach (Unit u in activeTile.Army.Unit)
                {
                    txtBoxArmy.Text += "Unit: " + u.UnitName + "\n HP: " + u.UnitHealth + "\n Attack: " + u.UnitOffense + " Defense: " + u.UnitDefense + "\n\n";
                }
            }
        }

        /*
         * TODO: Highlightaa annetun alueen tilet
         * 
         */
        private void highlightTiles(int minx, int maxx, int miny, int maxy)
        {

        }

        private void btnMoveArmy_Click(object sender, RoutedEventArgs e)
        {
            if (activeTile.Army != null && activeTile.Army.PlayerID == activePlayer && activeTile.Army.ArmyMovement >= 1) movingArmy = true;
            else MessageBox.Show("Cannot move enemy army or no army in selected tile");
        }

        private void btnEndTurn_Click(object sender, RoutedEventArgs e)
        {
            // LÄHETÄ TIEDOT KANTAAN!
            if (activePlayer == 1) activePlayer = 2;
            else activePlayer = 1;
            MessageBox.Show("Next player");

            foreach (Tile t in map)
            {
                if (t.PlayerID == activePlayer)
                {
                    if (t.Army != null) t.Army.ArmyMovement = t.Army.ArmyMaxMovement;
                    if (t.Construction != null)
                    {
                        if (t.Construction.ConstructionDisabled) t.Construction.ConstructionDisabled = false;
                        else
                        {
                            t.Construction.ConstructionProgress += t.Construction.ConstructionEfficiency;
                            if (t.Construction.ConstructionProgress >= 1)
                            {
                                if (t.Construction.Unit != null)
                                {
    //                                if (t.Army != null) t.Army.AddUnit(t.Construction.Unit);
     //                               else
                                    {
   //                                     t.Army = new Army("New Army", new List<Unit>(), activePlayer);
    //                                    t.Army.AddUnit(t.Construction.Unit);
                                    }
                                }
                                else
                                {
                                    // TÄHÄN UPGRADEJEN LISÄYS YKSIKÖILLE JNE
                                }
                            }
                        }
                    }
                }
            }

            drawMap();
            
        }

        private void btnMapEditor_Click(object sender, RoutedEventArgs e)
        {
            MapEditor me = new MapEditor();
            me.Show();
        }

    }
}
