using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public bool paused = true;
        public bool gameStarted = false;

        //BIRD PROPERITIES
        public bool stillalive = true;
        //MAX 10  BIGGEST
        //MIN 358 LOWEST
        //MID 185
        float height = 185f; 


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void txtblk_StartText_KeyDown(object sender, KeyEventArgs e)
        {
            if(!gameStarted)
            {
                paused = false;
                gameStarted = true;
                txtblk_StartText.Visibility = Visibility.Hidden;
            }
        }


        private void btn_Resume_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                btn_Resume.Content = "▶︎";
                paused = false; 
                
            }
            else
            {
                btn_Resume.Content = "⬛";
                paused = true;
            }
        }

        //private void playscreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(gameStarted && !paused && stillalive && height > 10)
        //    {
        //        height -= 1f;
        //        img_PlayerCharacter.Margin = new Thickness(20, height, 0, 0);
        //    }
        //}

        //private void playscreen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if(gameStarted && !paused && stillalive && height < 358)
        //    {
        //        height += 1f;
        //        img_PlayerCharacter.Margin = new Thickness(20, height, 0, 0);
        //    }

            

            

        
    }
}



