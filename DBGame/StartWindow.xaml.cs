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
using MySql.Data.MySqlClient;

namespace DBGame
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        G2776_2Entities1 db;
        List<int> pID;
        List<int> ghpIDs;
        List<int> mapIDs;
        public StartWindow()
        {
            InitializeComponent();
            pID = new List<int>();
            ghpIDs = new List<int>();
            mapIDs = new List<int>();
            db = new G2776_2Entities1();
            refreshComboboxes();
            /*
            try
            {

                
                con = new MySqlConnection(str);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT PlayerID FROM Player", con);

                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                pelaajaIDt = new List<int>();
                int apu = -1;
                while (reader.Read())
                {
                    int.TryParse(reader.GetString(0), out apu);
                    pelaajaIDt.Add(apu);
                    cmbPlayer.Items.Add(apu);
                    apu = -1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());

            }
            finally
            {
                con.Close();
            }
             * */
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMapEditor_Click(object sender, RoutedEventArgs e)
        {
            if (mapIDs[cmbMapForEditor.SelectedIndex] < 0)
            {
                MapEditor me = new MapEditor();
                me.Show();
            }
            else
            {
                MapEditor me = new MapEditor(mapIDs[cmbMapForEditor.SelectedIndex]);
                me.Show();
            }
        }

        private void cmbPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbGame.Items.Clear();
            ghpIDs.Clear();
            
            lblOpponent.Content = "Opponent: ";
            lblMap.Content = "Map: ";
            try
            {

                using (db = new G2776_2Entities1())
                {
                    int pnro = pID[cmbPlayer.SelectedIndex];
                    var game = from g in db.Game
                               join ghp in db.Game_has_Player
                               on g.GameID equals ghp.GameID
                               where ghp.PlayerID.Equals(pnro)
                               select new 
                               {
                                   g,
                                   ghp
                               };

                    foreach (var a in game)
                    {
                        if (cmbGame.Items.Contains(a.g.GameID)) continue;
                        cmbGame.Items.Add(a.g.GameID);
                        ghpIDs.Add(a.ghp.Game_has_PlayerID);
                    }
                }
                //MySqlCommand cmd = new MySqlCommand("SELECT Game_has_PlayerID FROM Game_has_Player WHERE Game_has_Player.PlayerID ='" + cmbPlayer.Items[cmbPlayer.SelectedIndex] + "';", con);

                /*
                 * 
                 * var game = from g in db.Game_has_Player
                               where g.PlayerID == pID[cmbPlayer.SelectedIndex]
                               select g;
                 * 
                 * 
                 * 
                int pid;
                MySqlCommand cmd1 = new MySqlCommand("SELECT GameID FROM Game_has_Player WHERE PlayerID = '" + cmbPlayer.Items[cmbPlayer.SelectedIndex] + "';",con);
                MySqlDataReader reader;
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse(reader.GetString(0), out pid);
                    MySqlCommand cmd2 = new MySqlCommand("SELECT PlayerName FROM Player WHERE PlayerID ='" + pid + "';", con);

                }
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void cmbGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblOpponent.Content = "Opponent: ";
            lblMap.Content = "Map: ";
            if (cmbGame.SelectedIndex < 0) return;
            try
            {
                using (db = new G2776_2Entities1())
                {
                    int gID = (int)cmbGame.Items[cmbGame.SelectedIndex];
                    int ghpID = ghpIDs[cmbGame.SelectedIndex];
                    //string asd = cmbPlayer.Items[cmbGame.SelectedIndex].ToString();
                    //int.TryParse(asd, out gID);
                    var info = from a in db.Player
                               join ghp in db.Game_has_Player
                               on a.PlayerID equals ghp.PlayerID
                               where ghp.GameID == gID
                               //where ghp.Game_has_PlayerID != ghpID
                               select a;
                    foreach (var q in info)
                    {
                        lblOpponent.Content = "Opponent: " + q.PlayerName;
                    }
                }
            //    MySqlCommand cmd = new MySqlCommand("SELECT Game_has_Player.PlayerID FROM Game_has_Player WHERE Game_has_Player.Game_has_PlayerID = '" + cmbGame.Items[cmbGame.SelectedIndex] + "';", con); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            Game g = new Game(mapIDs[cmbMap.SelectedIndex]);
            Game_has_Player ghp1;
            Game_has_Player ghp2;
            using (db = new G2776_2Entities1())
            {
                db.Game.Add(g);
                db.SaveChanges();
                
                var game = from gam in db.Game
                           orderby gam.GameID descending
                           select gam;
                foreach (var a in game) // purkkaa, foreachin sijaan pitäis vaan ensimmäistä alkiota käyttää...
                {
                    ghp1 = new Game_has_Player(a.GameID, pID[cmbPlayer.SelectedIndex]);
                    ghp2 = new Game_has_Player(a.GameID, pID[cmbOpponent.SelectedIndex]);
                    db.Game_has_Player.Add(ghp1);
                    db.Game_has_Player.Add(ghp2);
                    break;
                }
                db.SaveChanges();
                
            }
            // start game with given map and players
        }

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            bool validName = true;
            if (txtName.Text != null && txtName.Text != "")
            {
                using (db = new G2776_2Entities1())
                {
                    var pelaajat = from p in db.Player
                                   select p;
                    foreach (var p in pelaajat)
                    {
                        if (p.PlayerName == txtName.Text)
                        {
                            validName = false;
                            MessageBox.Show("Selected Player name already in use! Please select another");
                        }
                    }

                    if (validName)
                    {
                        db.Player.Add(new Player(txtName.Text));
                        db.SaveChanges();
                        MessageBox.Show("New Player created successfully");
                        refreshComboboxes();
                    }
                }
            }
        }

        private void refreshComboboxes()
        {


            cmbMap.Items.Clear();
            cmbMapForEditor.Items.Clear();
            cmbOpponent.Items.Clear();
            cmbPlayer.Items.Clear();
            pID.Clear();
            mapIDs.Clear();
            using (db)
            {
                var pelaajat = from p in db.Player
                               select p;

                foreach (var p in pelaajat)
                {
                    cmbPlayer.Items.Add(p.PlayerName);
                    pID.Add(p.PlayerID);
                    cmbOpponent.Items.Add(p.PlayerName);
                }
                var maps = from m in db.Map
                           select m;
                foreach (var m in maps)
                {
                    cmbMapForEditor.Items.Add(m.MapName);
                    mapIDs.Add(m.MapID);
                    cmbMap.Items.Add(m.MapName);
                }
                cmbMapForEditor.Items.Add("New Map");
                mapIDs.Add(-1);
            }
        }

    }
}
