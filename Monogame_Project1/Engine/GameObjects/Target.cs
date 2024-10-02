using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : GameObject
{
    private ShootingSystem _shootingSystem;
    public Target(Texture2D pTexture, Vector2 pPosition, ShootingSystem pShootingSystem) : base(pTexture)
    {
        position = pPosition;
        _shootingSystem = pShootingSystem;
    }
    public void CheckCollision()
    {

    }
}

