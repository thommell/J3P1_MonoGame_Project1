using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class PlayButton : Button
{
    public PlayButton(Game1 pGame, SceneManager pManager, Texture2D pTexture, string text) : base(pGame, pManager, pTexture, text) { }

    protected override void OnClick()
    {
        // manager.ChangeScene(manager.GetScene<>());
    }
}