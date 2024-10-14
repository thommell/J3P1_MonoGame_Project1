global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using Microsoft.Xna.Framework.Audio;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Design;
global using Microsoft.Xna.Framework.Media;
global using System.Collections.Generic;
using Monogame_Project1.Engine;

namespace Monogame_Project1;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager _sceneManager;

    public static int ScreenWidth = 1920;
    public static int ScreenHeight = 1080;

    private Rectangle bottomBorder;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        //_graphics.IsFullScreen = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _sceneManager = new SceneManager(_graphics, Content, _spriteBatch, this);
        _sceneManager.Awake();

        bottomBorder = new Rectangle(0, 1080 - 167, 1920, 167);
    }

    protected override void Update(GameTime gameTime)
    {
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //    Exit();
        _sceneManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(Content.Load<Texture2D>("Background"), new Vector2(0, 0), Color.White);
        _spriteBatch.Draw(Content.Load<Texture2D>("Pixel"), bottomBorder, Color.Gray * 0f);
        _sceneManager.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
