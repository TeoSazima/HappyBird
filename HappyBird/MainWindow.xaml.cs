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
        public bool StillAlive = true;

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
            gameTimer.Tick += Pipe.ChangeHeight;
            
            gameTimer.Tick += Player.Animation;
            gameTimer.Tick += Player.Move;
            
            gameTimer.Tick += CollisionDetect;
        }

        public void CollisionDetect(object sender, EventArgs e)
        {
            bool shouldEndTheGame = false;
            
            foreach (var pipe in Pipe.AllPartsOfPipes)
            {
                Rect pipeHitbox = new Rect(Canvas.GetLeft(pipe), Canvas.GetTop(pipe), pipe.Width, pipe.Height);

                if (pipeHitbox.IntersectsWith(Player.PlayerHitbox))
                {
                    shouldEndTheGame = true;
                    break;
                }
            }

            if (shouldEndTheGame)
            {
                gameTimer.Stop();
                
                Window scoreWindow = new ScoreWindow();
                scoreWindow.Show();

                Close();
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



