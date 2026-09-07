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
using System.Windows.Threading;

namespace HappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public bool paused = true;
        public bool gameStarted = false;
        public bool isMousePressed = false;
        

        //BIRD PROPERITIES
        public bool stillalive = true;

        //MAX 10  BIGGEST
        //MIN 358 LOWEST
        //MID 185
        int height = 185;
        
        DispatcherTimer timer = new DispatcherTimer();
        


        public MainWindow()
        {
            
            InitializeComponent();
            
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += MainLogic;
            timer.Start();

        }

        public void MainLogic(object sender, EventArgs e)
        {
                // KONTROLA ZDA JE LEVE TLACITKO MYSI ZMACKNUTE
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    isMousePressed = true;
                else
                    isMousePressed = false;
            
                if (isMousePressed)
                {
                    if(height > 10) 
                    {
                        height -= 1;
                        img_PlayerCharacter.Margin = new Thickness(20, height, 0, 0);
                    }
                }
                else
                {
                    if(height < 358) 
                    {
                        height += 1; 
                        img_PlayerCharacter.Margin = new Thickness(20, height, 0, 0);
                    }
                }
                
                
            
            
        }

        private void txtblk_StartText_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted)
            {
                //ODSTARTUJE HRU
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
                paused = !paused;
                
                //SPUSTI HLAVNI LOGIKU
                //MainLogic();
                   
                

            }
            else
            {
                btn_Resume.Content = "⬛";
                paused = !paused;
            }
        }


        
    }
}



