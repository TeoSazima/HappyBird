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
    public partial class MainWindow : Window
    {

        // GAME BOOLENANS FOR LOGIC
        public bool paused = true;
        public bool gameStarted = false;
        public bool isMousePressed = false;
        public bool stillalive = true;

        //MAX 10  HIGHEST
        //MIN 385 LOWEST
        //MID 185 CENTER
        int heightOfThePlayer = 185;

        
        // TIMERS FOR TICKS
        DispatcherTimer gameTimer = new DispatcherTimer();

        public MainWindow()
        {
            
            InitializeComponent();
            
            Player.Initialize(img_PlayerCharacter);
            Pipe.Initialize(canvas_GameCanvas);

            gameTimer.Interval = TimeSpan.FromMilliseconds(1.6); // 60FPS
            gameTimer.Tick += Pipe.Move;
            gameTimer.Tick += Pipe.CheckChangeDifficulty;
            gameTimer.Tick += Pipe.CollisionDetect;
            gameTimer.Tick += Pipe.ChangeHeight;
            
            gameTimer.Tick += Player.Animation;
            gameTimer.Tick += Player.Move;
        }
        
        private void txtblk_StartText_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted)
            {
                //ODSTARTUJE HRU
                paused = false;
                gameStarted = true;
                txtblk_StartText.Visibility = Visibility.Hidden;
                
                gameTimer.Start();
            }
        }
        private void btn_Resume_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                btn_Resume.Content = "⬛";
                paused = !paused;
                
                gameTimer.Start();
            }
            else
            {
                btn_Resume.Content = "▶︎";
                paused = !paused;
                
                gameTimer.Stop();
            }
        }
    }
}



