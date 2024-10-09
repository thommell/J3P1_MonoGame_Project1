﻿using Monogame_Project1.Engine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class FakeTarget : BaseTarget
{
    #region Constructors

    private SceneManager _sceneManager;
    public FakeTarget(Texture2D pTexture, SceneManager pSceneManager) : base(pTexture) 
    {
        _sceneManager = pSceneManager;
    }

    #endregion

    #region Public Voids
    public override void OnHit()
    {
         _sceneManager.ChangeScene(_sceneManager.GetScene<MainMenu>());
    }

    #endregion
}
