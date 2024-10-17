using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Enums;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class FakeTarget : BaseTarget
{
    #region Constructors

    private SceneManager _sceneManager;
    public FakeTarget(Texture2D pTexture) : base(pTexture) {}

    #endregion

    #region Public Voids
    public override void OnHit()
    {
        ResultHandler.Instance.HandleResult((LevelScene)SceneManager.Instance.CurrentScene, Results.Lose);
    }

    #endregion
}
