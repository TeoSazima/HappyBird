using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace HappyBird
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        public ScoreWindow()
        {

            
            InitializeComponent();

            SaveCurrentScore();
             
        }

        private void SaveCurrentScore()
        {

            MessageBoxResult result = MessageBox.Show("Přejete si uložit dosavadní score?", "GAME OVER", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                
            }           
            
        }

        
    }
}
