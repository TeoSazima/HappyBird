using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System;
using System.Threading;


namespace HappyBird
{
    public static class Pipe
    {
        // CONSTANTS
        private const int PipeWidthInPixels = 30;
        private const int PieceOfPipeHeightInPixels = 36;
        private const int PieceOfPipeEndHeightInPiexels = 5;

        private static List<Rectangle> AllPartsOfPipes = new List<Rectangle>();

        
        // 1 - EASY
        // 1.2 - MEDIUM
        // 1.3 - HARD
        // 1.5 - IMPOSSIBLE
        private static float DifficultyIndex = 1f; 
        
        
        private static SolidColorBrush _color = Brushes.Chartreuse; // BARVA SE NASTAVUJE PODLE OBTIZNOSTI
        
        
        // PIPE TOP 
        private static Rectangle _pipeTop = new Rectangle();
        private static Rectangle _pipeTopWallHolder = new Rectangle();
        private static Rectangle _pipeTopExit = new Rectangle();
        
        private static Rectangle TEST = new Rectangle();

        // PIPE BOTTOM
        private static Rectangle _pipeBottom = new Rectangle();
        private static Rectangle _pipeBottomWallHolder = new Rectangle();
        private static Rectangle _pipeBottomExit = new Rectangle();
        

        // INITIALIZACE
        public static void Initialize(Canvas canvas)
        {
            TEST.Width = PipeWidthInPixels;
            TEST.Height = 380;
            TEST.Fill = Brushes.Red;
            TEST.Stroke = Brushes.Black;
            TEST.Margin = new Thickness(0, 0, 0, 0);
            
            canvas.Children.Add(TEST);
            

            #region PIPE TOP INIT 
            
                _pipeTop.Width = PipeWidthInPixels;
                _pipeTop.Height = 50;
                _pipeTop.Fill = _color;
                _pipeTop.Stroke = Brushes.Black;
                _pipeTop.Margin = new Thickness(-50, PieceOfPipeEndHeightInPiexels, 0, 0);
                
                _pipeTopExit.Width = PipeWidthInPixels + 5;
                _pipeTopExit.Height = PieceOfPipeEndHeightInPiexels;
                _pipeTopExit.Fill = _color;
                _pipeTopExit.Stroke = Brushes.Black;
                _pipeTopExit.Margin = new Thickness(-50,PieceOfPipeEndHeightInPiexels + _pipeTop.Height, 0, 0);
                
                _pipeTopWallHolder.Width = PipeWidthInPixels + 5;
                _pipeTopWallHolder.Height = PieceOfPipeEndHeightInPiexels;
                _pipeTopWallHolder.Fill = _color;
                _pipeTopWallHolder.Stroke = Brushes.Black;
                _pipeTopWallHolder.Margin = new Thickness(-50, 0, 0, 0);
                
                canvas.Children.Add(_pipeTop);
                canvas.Children.Add(_pipeTopWallHolder);
                canvas.Children.Add(_pipeTopExit);
                
                AllPartsOfPipes.Add(_pipeTop);
                AllPartsOfPipes.Add(_pipeTopWallHolder);
                AllPartsOfPipes.Add(_pipeTopExit);
            
            #endregion
            #region PIPE BOTTOM INIT
            
                _pipeBottom.Width = PipeWidthInPixels;
                _pipeBottom.Height = 50;
                _pipeBottom.Fill = _color;
                _pipeBottom.Stroke = Brushes.Black;
                _pipeBottom.Margin = new Thickness(-50, 250, 0, PieceOfPipeEndHeightInPiexels +  _pipeBottom.Height);
                
                _pipeBottomExit.Width = PipeWidthInPixels + 5;
                _pipeBottomExit.Height = PieceOfPipeEndHeightInPiexels;
                _pipeBottomExit.Fill = _color;
                _pipeBottomExit.Stroke = Brushes.Black;
                _pipeBottomExit.Margin = new Thickness(-50,250, 0, 2*PieceOfPipeEndHeightInPiexels +  _pipeBottom.Height);
                
                _pipeBottomWallHolder.Width = PipeWidthInPixels + 5;
                _pipeBottomWallHolder.Height = PieceOfPipeEndHeightInPiexels;
                _pipeBottomWallHolder.Fill = _color;
                _pipeBottomWallHolder.Stroke = Brushes.Black;
                _pipeBottomWallHolder.Margin = new Thickness(-50, 250, 0, 10 * PieceOfPipeEndHeightInPiexels);
                
                canvas.Children.Add(_pipeBottom);
                canvas.Children.Add(_pipeBottomWallHolder);
                canvas.Children.Add(_pipeBottomExit);
                
                AllPartsOfPipes.Add(_pipeBottom);
                AllPartsOfPipes.Add(_pipeBottomWallHolder);
                AllPartsOfPipes.Add(_pipeBottomExit);
            
            #endregion
        }
        
        
        // ZMENNA BAREV TRUBEK PODLE DOSAZENEHO SCORE
        public static void CheckChangeDifficulty(object sender, EventArgs e)
        {
            switch (Player.Score)
            {
                case 10:
                    _color = Brushes.Yellow;
                    DifficultyIndex = 1.02f;
                    break;
                case 20:
                    _color = Brushes.Orange;
                    DifficultyIndex = 1.03f;
                    break;
                case 30:
                    _color = Brushes.Red;
                    DifficultyIndex = 1.5f;
                    break;
            }
        }
        public static void Move(object sender, EventArgs e)
        {
            foreach (var pipe in  AllPartsOfPipes)
            {
                if (pipe.Margin.Left < -30)
                {
                    ResetPosition(sender, e);
                }
                pipe.Margin = new Thickness(pipe.Margin.Left - 1, pipe.Margin.Top, pipe.Margin.Right, pipe.Margin.Bottom);
            }
        }
        public static void ResetPosition(object sender, EventArgs e)
        {
            // VYRESETUJE VSECHNY PIPES NA PUVODNI POZICI
            
            _pipeTop.Margin = new Thickness(252, PieceOfPipeEndHeightInPiexels, 0, 0);
            _pipeTop.Fill =  _color;
            _pipeTopWallHolder.Margin = new Thickness(250, 0, 0, 0);
            _pipeTopWallHolder.Fill = _color;
            _pipeTopExit.Margin = new Thickness(250,PieceOfPipeEndHeightInPiexels + _pipeTop.Height, 0, 0);
            _pipeTopExit.Fill = _color;

            _pipeBottom.Margin = new Thickness(252, 250, 0, PieceOfPipeEndHeightInPiexels +  _pipeBottom.Height);
            _pipeBottom.Fill = _color;
            _pipeBottomExit.Margin = new Thickness(250,250, 0, 2*PieceOfPipeEndHeightInPiexels +  _pipeBottom.Height);
            _pipeBottomExit.Fill = _color;
            _pipeBottomWallHolder.Margin = new Thickness(250, 250, 0, PieceOfPipeEndHeightInPiexels);
            _pipeBottomWallHolder.Fill = _color;
        }
        public static void CollisionDetect(object sender, EventArgs e)
        {
            
        }
        public static void ChangeHeight(object sender, EventArgs e)
        {
            foreach (var pipe in AllPartsOfPipes)
            {
                if (pipe.Margin.Left < -30)
                {
                    Random rand = new Random();
                   
                    int pipeTopHeight = rand.Next(1, 8);
                    int pipeBottomHeight = 7 - pipeTopHeight;

                    //NASTAVENI POTREBNYCH VELIKOST 
                    _pipeTop.Height =  pipeTopHeight * PieceOfPipeHeightInPixels;
                    _pipeBottom.Height =  pipeBottomHeight * PieceOfPipeHeightInPixels;
                    Pipe.ResetPosition(sender, e);


                }
            }
            
            
        }
    }
}