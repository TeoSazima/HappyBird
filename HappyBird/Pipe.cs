using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Threading;


namespace HappyBird
{
    public static class Pipe
    {
        // CONSTANTS
        private const int PipeWidthInPixels = 30;
        private const float PieceOfPipeHeightInPixels = 38;
        private const int PieceOfPipeEndHeightInPixels = 5;

        public static List<Rectangle> AllPartsOfPipes = new List<Rectangle>();

        
        // 1 - EASY
        // 1.2 - MEDIUM
        // 1.3 - HARD
        // 1.5 - IMPOSSIBLE
        private static float _difficultyIndex = 1f;
        private static float _moveSpeed = 1f;
        
        
        private static SolidColorBrush _color = Brushes.Chartreuse; // BARVA SE NASTAVUJE PODLE OBTIZNOSTI
        
        
        // PIPE TOP 
        private static Rectangle _pipeTop = new Rectangle();
        private static Rectangle _pipeTopWallHolder = new Rectangle();
        private static Rectangle _pipeTopExit = new Rectangle();
        

        // PIPE BOTTOM
        private static Rectangle _pipeBottom = new Rectangle();
        private static Rectangle _pipeBottomWallHolder = new Rectangle();
        private static Rectangle _pipeBottomExit = new Rectangle();
        

        // INITIALIZACE
        public static void Initialize(Canvas canvas)
        {
            #region PIPE TOP INIT 
            
                _pipeTop.Width = PipeWidthInPixels;
                _pipeTop.Height = 50;
                _pipeTop.Fill = _color;
                _pipeTop.Stroke = Brushes.Black;
                Canvas.SetLeft(_pipeTop, -50);
                
                _pipeTopExit.Width = PipeWidthInPixels + 5;
                _pipeTopExit.Height = PieceOfPipeEndHeightInPixels;
                _pipeTopExit.Fill = _color;
                _pipeTopExit.Stroke = Brushes.Black;
                Canvas.SetLeft(_pipeTopExit, -50);

                
                _pipeTopWallHolder.Width = PipeWidthInPixels + 5;
                _pipeTopWallHolder.Height = PieceOfPipeEndHeightInPixels;
                _pipeTopWallHolder.Fill = _color;
                _pipeTopWallHolder.Stroke = Brushes.Black;
                Canvas.SetLeft(_pipeTopWallHolder, -50);
                Canvas.SetTop(_pipeTopWallHolder, 0);

                
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
                Canvas.SetLeft(_pipeBottom, -50);
                
                _pipeBottomExit.Width = PipeWidthInPixels + 5;
                _pipeBottomExit.Height = PieceOfPipeEndHeightInPixels;
                _pipeBottomExit.Fill = _color;
                _pipeBottomExit.Stroke = Brushes.Black;
                Canvas.SetLeft(_pipeBottomExit, -50);
                
                _pipeBottomWallHolder.Width = PipeWidthInPixels + 5;
                _pipeBottomWallHolder.Height = PieceOfPipeEndHeightInPixels;
                _pipeBottomWallHolder.Fill = _color;
                _pipeBottomWallHolder.Stroke = Brushes.Black;
                Canvas.SetLeft(_pipeBottomWallHolder, -50);
                Canvas.SetTop(_pipeBottomWallHolder,395);
                
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
                    _difficultyIndex = 1.02f;
                    _moveSpeed = 2f;
                    break;
                case 20:
                    _color = Brushes.Orange;
                    _difficultyIndex = 1.03f;
                    _moveSpeed = 3f;
                    break;
                case 30:
                    _color = Brushes.Red;
                    _moveSpeed = 4f;
                    break;
            }
        }
        public static void Move(object sender, EventArgs e)
        {
            foreach (var pipe in  AllPartsOfPipes)
            {
                double currentX = Canvas.GetLeft(pipe);
                
                if (currentX < -100)
                {
                    ResetYPosition(sender, e);
                    return;
                }
                
                Canvas.SetLeft(pipe,  currentX - _moveSpeed);
            }
        }
        public static void ResetYPosition(object sender, EventArgs e)
        {
            // VYRESETUJE VSECHNY PIPES NA PUVODNI POZICI
            
            Canvas.SetLeft(_pipeTop, 252);
            _pipeTop.Fill =  _color;
            Canvas.SetLeft(_pipeTopWallHolder, 250);
            _pipeTopWallHolder.Fill = _color;
            Canvas.SetLeft(_pipeTopExit, 250);
            _pipeTopExit.Fill = _color;

            Canvas.SetLeft(_pipeBottom, 252);
            _pipeBottom.Fill = _color;
            Canvas.SetLeft(_pipeBottomExit, 250);
            _pipeBottomExit.Fill = _color;
            Canvas.SetLeft(_pipeBottomWallHolder, 250);
            _pipeBottomWallHolder.Fill = _color;
        }
        
        public static void ChangeHeight(object sender, EventArgs e)
        {
            foreach (var pipe in AllPartsOfPipes)
            {
                double currentX = Canvas.GetLeft(pipe);
                if(currentX < -50)
                {
                    Random rand = new Random();
                   
                    int pipeTopHeight = rand.Next(1, 7);
                    int pipeBottomHeight = 7 - pipeTopHeight;

                    // NASTAVENI POTREBNYCH VELIKOST 
                    // DIFFICULTY INDEX DELA TRUBKY MIRNE VETSI PODLE DOSAZENEHO SCORE
                    
                    _pipeTop.Height =  pipeTopHeight * PieceOfPipeHeightInPixels * _difficultyIndex; 
                    Canvas.SetTop(_pipeTop, PieceOfPipeEndHeightInPixels);
                    Canvas.SetTop(_pipeTopExit, _pipeTop.Height + PieceOfPipeEndHeightInPixels);
                    
                    
                    _pipeBottom.Height =  pipeBottomHeight * PieceOfPipeHeightInPixels * _difficultyIndex;
                    Canvas.SetTop(_pipeBottom, 400 - _pipeBottom.Height - PieceOfPipeEndHeightInPixels );
                    Canvas.SetTop(_pipeBottomExit,400 - _pipeBottom.Height - 2 * PieceOfPipeEndHeightInPixels);
                    //Canvas.SetBottom(_pipeBottomExit, 2*PieceOfPipeEndHeightInPiexels + _pipeBottomWallHolder.Height);
                    
                    
                    
                    Pipe.ResetYPosition(sender, e);


                }
            }
            
            
        }
    }
}