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
        // CONSTANTS
        private const int pipeWidthInPixels = 30;
        private const int opieceOfPipeLenghtInPixels = 38;
        

        public bool paused = true;
        public bool gameStarted = false;
        public bool isMousePressed = false;
        

        //BIRD PROPERITIES
        public bool stillalive = true;

        //MAX 10  BIGGEST
        //MIN 385 LOWEST
        //MID 185
        int height = 185;
        
        DispatcherTimer renderTimer = new DispatcherTimer();
        DispatcherTimer newPipeGenerate = new DispatcherTimer();



        public MainWindow()
        {

            InitializeComponent();

            renderTimer.Interval = TimeSpan.FromMilliseconds(10);
            renderTimer.Tick += MainLogic;

            newPipeGenerate.Interval = TimeSpan.FromSeconds(5);
            newPipeGenerate.Tick += NewPipe;
            



        }

        private void NewPipe(object sender, EventArgs e)
        {
            // 1 ELEMENT JE 23px
            // CELKEM 230px = VELIKOST OBRAZOVKY
            char[] hitboxArray = new char[10];
            
            Random rand = new Random();
            int x = rand.Next(0, hitboxArray.Length);
            
            
                if (x == 0)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        hitboxArray[x+j] = 'X';
                    }
                }
                else if (x == 9)
                {
                    for (int j = -1; j < 1; j++)
                    {
                        hitboxArray[x+j] = 'X';
                    }
                }
                else
                {
                    for (int j = -1; j < 2; j++)
                    {
                        hitboxArray[x+j] = 'X';
                    }
                }
            
            
            
            int FirstPipeLenght = 0;
            
            for (int i = 0; i < hitboxArray.Length; i++)
            {

                if (hitboxArray[i] == 'X' && FirstPipeLenght != 0) break;

                FirstPipeLenght++;
            }
            
            var pipeTop =  new Rectangle();
            pipeTop.Height = FirstPipeLenght * opieceOfPipeLenghtInPixels;
            pipeTop.Width = pipeWidthInPixels;
            pipeTop.Fill = Brushes.Chartreuse;
            pipeTop.HorizontalAlignment = HorizontalAlignment.Center;
            pipeTop.Margin = new Thickness(0,10,0,0);
            pipeTop.Stroke  = Brushes.Black;
            pipeTop.VerticalAlignment = VerticalAlignment.Top;
            
            grid.Children.Add(pipeTop);
            
            
            
            
            
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
                
                renderTimer.Start();
                newPipeGenerate.Start();
            }
        }


        private void btn_Resume_Click(object sender, RoutedEventArgs e)
        {
            if (paused)
            {
                btn_Resume.Content = "▶︎";
                paused = !paused;
                
                renderTimer.Start();
                newPipeGenerate.Start();
            }
            else
            {
                btn_Resume.Content = "⬛";
                paused = !paused;
                
                renderTimer.Stop();
                newPipeGenerate.Stop();
            }
        }
    }
}



