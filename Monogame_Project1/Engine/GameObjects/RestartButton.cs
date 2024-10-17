using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class RestartButton : Button
{
    public RestartButton(Texture2D pTexture, string text, bool pIsActive = true) : base(pTexture, text, pIsActive) {}

    protected override void OnClick()
    {
        SceneManager.Instance.RestartLevel(SceneManager.Instance.pastLevelScene);
    }
}