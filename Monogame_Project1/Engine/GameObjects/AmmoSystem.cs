﻿using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Scenes;
using Monogame_Project1.Engine.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class AmmoSystem : GameObject
{
    private int _ammo;
    private int _startAmmo;
    public int Ammo {  get { return _ammo; } set { _ammo = value; } }

    public int StartAmmo { get { return _startAmmo; } set { _startAmmo = value; } }


    public AmmoSystem(int pAmmo)
    {
        _ammo = pAmmo;
        _startAmmo = pAmmo;
    }

    public override void Update(GameTime pGameTime)
    {
        if (_ammo == 0)
            SceneManager.Instance.SwapScene(SceneManager.Instance.GetScene<LoseScene>());
    }

    public void AddAmmo(int pAmmo)
    {
        _ammo += pAmmo;
    }
    public void SubtractAmmo(int pAmmo) 
    {
        _ammo -= pAmmo;
    }
    public void ResetAmmo()
    {
        _ammo = _startAmmo;
    }
}