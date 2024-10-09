using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class RestartButton : Button
{
    private SceneManager _manager;
    public RestartButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text, bool pIsActive = true) :
        base(pGame, pManager, pTexture, text, pIsActive)
    {
        _manager = pManager;
    }

    protected override void OnClick()
    {
        _manager.RestartLevel();
    }
}