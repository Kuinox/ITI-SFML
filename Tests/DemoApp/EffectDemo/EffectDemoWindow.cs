using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.EffectDemo
{
    class EffectDemoWindow : RenderWindow
    {
        readonly Font _font;
        readonly Effect[] _effects;
        readonly Text _description;
        readonly Texture _textBackgroundTexture;
        readonly Sprite _textBackground;
        readonly Text _instructions;
        int _currentEffect;

        public EffectDemoWindow()
            : base( new VideoMode( 800, 600 ), "SFML.Net Shader" )
        {
            SetVerticalSyncEnabled( true );
            // Load the application font.
            _font = new Font( "resources/sansation.ttf" );
            // Create the effects
            _effects = new Effect[]
            {
                new PixelateEffect(_font),
                new WaveBlurEffect(_font),
                new StormBlinkEffect(_font),
                new EdgeEffect(_font)
            };
            // Create the messages background
            _textBackgroundTexture = new Texture( "resources/text-background.png" );
            _textBackground = new Sprite( _textBackgroundTexture );
            _textBackground.Position = new Vector2f( 0, 520 );
            _textBackground.Color = new Color( 255, 255, 255, 200 );

            // Create the description text (_currentEffect is 0).
            _description = new Text( "Current effect: " + _effects[0].Name, _font, 20 );
            _description.Position = new Vector2f( 10, 530 );
            _description.FillColor = new Color( 80, 80, 80 );

            // Create the instructions text
            _instructions = new Text( "Press left and right arrows to change the current shader", _font, 20 );
            _instructions.Position = new Vector2f( 280, 555 );
            _instructions.FillColor = new Color( 80, 80, 80 );
        }

        public void Update( Clock clock )
        {
            Vector2i mouse = Mouse.GetPosition( this );
            var x = (float)mouse.X / Size.X;
            var y = (float)mouse.Y / Size.Y;
            _effects[_currentEffect].Update( clock.ElapsedTime.AsSeconds(), x, y );
        }

        public void Draw()
        {
            // Clear the window.
            Clear( new Color( 255, 128, 0 ) );

            // Draw the current example.
            Draw( _effects[_currentEffect] );

            // Draw the text
            Draw( _textBackground );
            Draw( _instructions );
            Draw( _description );

            // Finally, display the rendered frame on screen.
            Display();
        }

        protected override void OnClosed()
        {
            Close();
            base.OnClosed();
        }

        protected override void OnKeyPressed( KeyEventArgs e )
        {
            // Escape key : exit
            if( e.Code == Keyboard.Key.Escape )
            {
                OnClosed();
            }
            // Left arrow key: previous shader
            if( e.Code == Keyboard.Key.Left )
            {
                if( _currentEffect == 0 )
                    _currentEffect = _effects.Length - 1;
                else
                    _currentEffect--;
                _description.DisplayedString = "Current effect: " + _effects[_currentEffect].Name;
            }

            // Right arrow key: next shader
            if( e.Code == Keyboard.Key.Right )
            {
                if( _currentEffect == _effects.Length - 1 )
                    _currentEffect = 0;
                else _currentEffect++;
                _description.DisplayedString = "Current effect: " + _effects[_currentEffect].Name;
            }
            base.OnKeyPressed( e );
        }
    }


}
