using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HappyBird
{
    public static class Player
    {
        public static int Score = 30;
        public static Rect PlayerHitbox;
        
        private static int _birdCurrentFrame = 0; // 0-3 
        private static int _birdAnimationTick = 0;
        

        public static Image PlayerCharacter = new Image();

        
        public static void Initialize(Image birdCharacter)
        {
            PlayerCharacter =  birdCharacter;
            PlayerHitbox = new Rect();
        } 
        public static void Animation(object sender, EventArgs e)
        {
            _birdAnimationTick++;
            if (_birdAnimationTick > 10)
            {
                if (_birdCurrentFrame == 3)
                {
                    _birdCurrentFrame = 0;
                }
                else
                    _birdCurrentFrame++;

                PlayerCharacter.Source = new ImageSourceConverter().ConvertFromString("../../source/images/Bird/yellow/tile00" + _birdCurrentFrame.ToString() + ".png") as ImageSource;
                _birdAnimationTick = 0;
            }
        }

        public static void Move(object sender, EventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if(PlayerCharacter.Margin.Top > 5)
                    PlayerCharacter.Margin = new Thickness(20, PlayerCharacter.Margin.Top - 2, 0,0);
                

            }
            else
            {
                if(PlayerCharacter.Margin.Top < 375)
                    PlayerCharacter.Margin = new Thickness(20, PlayerCharacter.Margin.Top + 2, 0,0);
            }
            
            PlayerHitbox = new Rect(PlayerCharacter.Margin.Left, PlayerCharacter.Margin.Top, PlayerCharacter.Width - 30, PlayerCharacter.Height - 2);
            

        }

    }
}