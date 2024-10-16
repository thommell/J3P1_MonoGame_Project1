using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.BaseClasses;
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
        ResultHandlerSingleton.Instance.HandleResult((LevelScene)SceneManagerSingleton.Instance.CurrentScene, Result.Lose);
    }

    #endregion
}
