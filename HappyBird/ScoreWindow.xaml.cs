using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Text.Json;
using System.Windows.Input;


namespace HappyBird
{

    public class PlayerRecord
    {
        public string Nickname { get; set; }
        public int Score { get; set; }
        //public DateOnly DateOnly { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        public override string ToString()
        {
            return $"NAME: {Nickname} | SCORE: {Score}";
            //return $"NAME: {Nickname} | SCORE: {Score} | DATE: {DateOnly}";
            
            
        }
    }
    
    public partial class ScoreWindow : Window
    {
        public List<PlayerRecord> PlayerRecords;
        
        
        public ScoreWindow()
        {
            InitializeComponent();
            ShouldSaveCurrentScore();

        }

        public void ShouldSaveCurrentScore()
        {

            MessageBoxResult result = MessageBox.Show("Přejete si uložit dosavadní score?", "GAME OVER", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                btn_Submit.Visibility = Visibility.Hidden;
                txtbox_Nickname.Visibility = Visibility.Hidden;
            }           
            
            if (!File.Exists("Leaderboards.json"))
            {
                File.Create("Leaderboards.json").Close();
                File.WriteAllText("Leaderboards.json", "[]");
            }
            
            string jsonText = File.ReadAllText("Leaderboards.json");
            
            PlayerRecords = JsonSerializer.Deserialize<List<PlayerRecord>>(jsonText);
            UpdateLeaderboard();
            
            
            
        }


        private void Btn_Submit(object sender, RoutedEventArgs e)
        {
            PlayerRecord PR = new PlayerRecord();
            PR.Nickname = txtbox_Nickname.Text;
            PR.Score = Player.Score;
            
            PlayerRecords.Add(PR);
            UpdateLeaderboard();
            
            btn_Submit.Visibility = Visibility.Hidden;
            txtbox_Nickname.Visibility = Visibility.Hidden;

        }

        private void ScoreWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            string JsonSerialized = JsonSerializer.Serialize(PlayerRecords);
            
            File.WriteAllText("Leaderboards.json", JsonSerialized);
        }

        private void UpdateLeaderboard()
        {
            PlayerRecords = PlayerRecords.OrderByDescending(o => o.Score).ToList();

            lstbox_Leaderboard.ItemsSource = null;
            lstbox_Leaderboard.ItemsSource = PlayerRecords; 
        }

        private void Txtbox_Nickname_OnGotFocus(object sender, RoutedEventArgs e)
        {
            txtbox_Nickname.Text = string.Empty;
        }

        private void ScoreWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Delete) && lstbox_Leaderboard.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Opravdu chcete tento záznam smazat", "DELETE RECORD", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int index = lstbox_Leaderboard.SelectedIndex;
                    PlayerRecords.RemoveAt(index);
                    UpdateLeaderboard();
                }     
            }
        }
    }
}
