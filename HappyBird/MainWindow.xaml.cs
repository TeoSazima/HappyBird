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
        // CONSTANTS
        private const int pipeWidthInPixels = 30;
        private const int pieceOfPipeLenghtInPixels = 38;

        // LISTS OF PIPES
        List<Rectangle> pipes = new List<Rectangle>();
        List<Rect> pipesHitboxes = new List<Rect>();

        // GAME BOOLENANS FOR LOGIC
        public bool paused = true;
        public bool gameStarted = false;
        public bool isMousePressed = false;
        public bool stillalive = true;
        
        // ANIMATION VARIABLES
        private int birdCurrentFrame = 0; // 0-3 
        private int birdAnimationTick = 0;
        
        // BIRD ANIMATION SET
        List<BitmapImage> BirdAnimationSet = new List<BitmapImage>();

        // PLAYER HITBOX FOR CHECKING COLLISIONS
        Rect playerHitbox = new Rect(185, 20,16,16);
        
        

        //MAX 10  HIGHEST
        //MIN 385 LOWEST
        //MID 185 CENTER
        int heightOfThePlayer = 185;

        
        // TIMERS FOR TICKS
        DispatcherTimer gameTimer = new DispatcherTimer();
        DispatcherTimer newPipeGenerate = new DispatcherTimer();



        public MainWindow()
        {

            InitializeComponent();
            


            gameTimer.Interval = TimeSpan.FromMilliseconds(1.6);
            gameTimer.Tick += DrawScreen;
            gameTimer.Tick  += AreaseObjects;
            gameTimer.Tick += AnimationManager;
            gameTimer.Tick  += CollisionDetect;

            newPipeGenerate.Interval = TimeSpan.FromSeconds(4);
            newPipeGenerate.Tick += NewPipe;


    

        }

        private void CollisionDetect(object sender, EventArgs e)
        {
             
            foreach (var pipesHitbox in pipesHitboxes)
            {
                var HitboxDebug = new Rectangle();
                
                if (pipesHitbox.IntersectsWith(playerHitbox))
                {
                   
                    
                    // KONEC HRY
                    ScoreWindow scoreWindow = new ScoreWindow();
                    scoreWindow.ShowDialog();
                    this.Close();
                    
                }
                
            }
        }
        
        private void NewPipe(object sender, EventArgs e)
        {
            // 1 ELEMENT JE 23px
            // CELKEM 230px = VELIKOST OBRAZOVKY
            char[] hitboxArray = new char[10];

            bool isOnePipe = false;

            Random rand = new Random();
            int x = rand.Next(0, hitboxArray.Length);


            if (x == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    hitboxArray[x + j] = 'X';
                    isOnePipe = true;
                }
            }
            else if (x == 9)
            {
                for (int j = -1; j < 1; j++)
                {
                    hitboxArray[x + j] = 'X';
                    isOnePipe = true;
                }
            }
            else
            {
                for (int j = -1; j < 2; j++)
                {
                    hitboxArray[x + j] = 'X';
                }
            }



            int pipeTopLenght = 0;

            for (int i = 0; i < hitboxArray.Length; i++)
            {

                if (hitboxArray[i] == 'X' && pipeTopLenght != 0) break;

                pipeTopLenght++;
            }

            var pipeTop = new Rectangle();
            pipeTop.Height = pipeTopLenght * pieceOfPipeLenghtInPixels;
            pipeTop.Width = pipeWidthInPixels;
            pipeTop.Fill = Brushes.Chartreuse;
            pipeTop.HorizontalAlignment = HorizontalAlignment.Right;
            pipeTop.Margin = new Thickness(0, 10, 1 - pipeWidthInPixels, 0);
            pipeTop.Stroke = Brushes.Black;
            pipeTop.VerticalAlignment = VerticalAlignment.Top;

            
            var pipeTopWallHolder = new Rectangle();
            pipeTopWallHolder.Height = 5;
            pipeTopWallHolder.Width = pipeWidthInPixels + 5;
            pipeTopWallHolder.Fill = Brushes.Chartreuse;
            pipeTopWallHolder.HorizontalAlignment = HorizontalAlignment.Right;
            pipeTopWallHolder.Margin = new Thickness(0, 10, -2 - pipeWidthInPixels, 0);
            pipeTopWallHolder.Stroke = Brushes.Black;
            pipeTopWallHolder.VerticalAlignment = VerticalAlignment.Top;
            
            var pipeTopEntry = new Rectangle();
            pipeTopEntry.Height = 5;
            pipeTopEntry.Width = pipeWidthInPixels + 5;
            pipeTopEntry.Fill = Brushes.Chartreuse;
            pipeTopEntry.HorizontalAlignment = HorizontalAlignment.Right;
            pipeTopEntry.Margin = new Thickness(0, 10 + pipeTop.Height, -2 -  pipeWidthInPixels, 0);
            pipeTopEntry.Stroke = Brushes.Black;
            pipeTopEntry.VerticalAlignment = VerticalAlignment.Top;
            
            
            
            int pipeBottomLenght = 0;

            if (x == 9 || x == 0 && isOnePipe != true)
            {
                pipeBottomLenght = 8 - pipeTopLenght;
            }
            else
            {
                pipeBottomLenght = 7 - pipeTopLenght; 
            }

            if (pipeBottomLenght != 0)
            {
                
                
                var pipeBottom = new Rectangle();
                pipeBottom.Height = pipeBottomLenght * pieceOfPipeLenghtInPixels;
                pipeBottom.Width = pipeWidthInPixels;
                pipeBottom.Fill = Brushes.Chartreuse;
                pipeBottom.HorizontalAlignment = HorizontalAlignment.Right;
                pipeBottom.Margin = new Thickness(0, 0, 1- pipeWidthInPixels, 47);
                pipeBottom.Stroke = Brushes.Black;
                pipeBottom.VerticalAlignment = VerticalAlignment.Bottom;


                const double xPos = 250 - (1 + pipeWidthInPixels);
                const double yPos = 10;
                var pipeBottomHitbox = new Rect(xPos, yPos, pipeWidthInPixels, pipeBottomLenght * pieceOfPipeLenghtInPixels);
                
                
                var pipeBottomWallHolder = new Rectangle();
                pipeBottomWallHolder.Height = 5;
                pipeBottomWallHolder.Width = pipeWidthInPixels + 5;
                pipeBottomWallHolder.Fill = Brushes.Chartreuse;
                pipeBottomWallHolder.HorizontalAlignment = HorizontalAlignment.Right;
                pipeBottomWallHolder.Margin = new Thickness(0, 0, -2 - pipeWidthInPixels, 47);
                pipeBottomWallHolder.Stroke = Brushes.Black;
                pipeBottomWallHolder.VerticalAlignment = VerticalAlignment.Bottom;
            
                var pipeBottomEntry = new Rectangle();
                pipeBottomEntry.Height = 5;
                pipeBottomEntry.Width = pipeWidthInPixels + 5;
                pipeBottomEntry.Fill = Brushes.Chartreuse;
                pipeBottomEntry.HorizontalAlignment = HorizontalAlignment.Right;
                pipeBottomEntry.Margin = new Thickness(0, 0 , -2 - pipeWidthInPixels, 47+ pipeBottom.Height);
                pipeBottomEntry.Stroke = Brushes.Black;
                pipeBottomEntry.VerticalAlignment = VerticalAlignment.Bottom;
                
                // RECTANGLE PIPE BOTTOM
                grid.Children.Add(pipeBottom);
                grid.Children.Add(pipeBottomEntry);
                grid.Children.Add(pipeBottomWallHolder);
                
                pipes.Add(pipeBottomEntry);
                pipes.Add(pipeBottomWallHolder);
                pipes.Add(pipeBottom);
                
                
                // RECT HITBOX PIPE BOTTOM
                pipesHitboxes.Add(pipeBottomHitbox);
            }

            // RECTANGLE PIPE TOP
            grid.Children.Add(pipeTop);
            grid.Children.Add(pipeTopWallHolder);
            grid.Children.Add(pipeTopEntry);
            
            
            pipes.Add(pipeTop);
            pipes.Add(pipeTopWallHolder);
            pipes.Add(pipeTopEntry);

            
            
            
            
            



        }

        private void AnimationManager(object sender, EventArgs e)
        {
            // BIRD SECTION
            birdAnimationTick++;
            if (birdAnimationTick > 10)
            {
                if (birdCurrentFrame == 3)
                {
                    birdCurrentFrame = 0;
                }
                else
                    birdCurrentFrame++;

                img_PlayerCharacter.Source = new ImageSourceConverter().ConvertFromString("../../source/images/Bird/yellow/tile00" + birdCurrentFrame.ToString() + ".png") as ImageSource;
                birdAnimationTick = 0;

            }


        }
        
        private void AreaseObjects(object sender, EventArgs e)
        {
            #region PIPES CLEANUP
            // CLEANS EVERY PIPE THAT IS OUT OF SCREEN RANGE
            
            foreach (var pipe in pipes.ToList())
            {
                if (pipe.Margin.Right >= 230)
                {
                    
                    grid.Children.Remove(pipe);
                    pipes.Remove(pipe);
                    
                }
            }
            #endregion
        }
        
        private void DrawScreen(object sender, EventArgs e)
        {
            #region PipesRender

            // BEZI KADZYCH 1,6ms ~~ 60 FPS 
            foreach (var pipe in pipes)
            {
                Rectangle marginOld = new Rectangle();
                marginOld.Margin = pipe.Margin;
                
                
                pipe.Margin = new Thickness(pipe.Margin.Left, pipe.Margin.Top, pipe.Margin.Right + 1, pipe.Margin.Bottom);
            }

            #endregion
            #region BirdRender

            // KONTROLA ZDA JE LEVE TLACITKO MYSI ZMACKNUTE
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                isMousePressed = true;
            else
                isMousePressed = false;
            
            if (isMousePressed)
            {
                if(heightOfThePlayer > 10) 
                {
                    heightOfThePlayer -= 2;
                    img_PlayerCharacter.Margin = new Thickness(20, heightOfThePlayer, 0, 0);
                    playerHitbox.Y -= 2;
                }
            }
            else
            {
                if(heightOfThePlayer < 358) 
                {
                    heightOfThePlayer += 2; 
                    img_PlayerCharacter.Margin = new Thickness(20, heightOfThePlayer, 0, 0);
                    playerHitbox.Y += 2;
                    
                }
            }
            #endregion
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
                newPipeGenerate.Start();
            }
        }


        private void btn_Resume_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                btn_Resume.Content = "▶︎";
                paused = !paused;
                
                gameTimer.Start();
                newPipeGenerate.Start();
            }
            else
            {
                btn_Resume.Content = "⬛";
                paused = !paused;
                
                gameTimer.Stop();
                newPipeGenerate.Stop();
            }
        }
    }
}



