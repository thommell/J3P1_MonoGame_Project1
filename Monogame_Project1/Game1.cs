global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Input;
global using Microsoft.Xna.Framework.Audio;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Design;
global using Microsoft.Xna.Framework.Media;
global using System.Collections.Generic;
using Monogame_Project1.Engine;
using Monogame_Project1.Engine.GameObjects;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1;
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    // private SceneManager _sceneManager;

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
         AudioManager.Instance.LoadContent(Content);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // _sceneManager = new SceneManager(_graphics, Content, _spriteBatch, this);
        // _sceneManager.Awake();
        SceneManager.Instance.Game = this;
        SceneManager.Instance.Awake();
    }
    protected override void Update(GameTime gameTime)
    {
        // _sceneManager.Update(gameTime);
        SceneManager.Instance.Update(gameTime);
        ResultHandler.Instance.Update(gameTime);
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        // _sceneManager.Draw(_spriteBatch);
        SceneManager.Instance.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
