using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : GameObject
{
    #region Fields
    private ShootingSystem _shootingSystem;
    private bool isActive = true;
    #endregion
    #region Constructors
    public Target(Texture2D pTexture, Vector2 pPosition, ShootingSystem pShootingSystem) : base(pTexture)
    {
        position = pPosition;
        _shootingSystem = pShootingSystem;
    }
    #endregion
    #region Public Methods
    public override void Update(GameTime pGameTime)
    {
        if (!isActive) return;

        _shootingSystem.CheckCollision(this);

        base.Update(pGameTime);
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        if (!isActive) return;

        base.Draw(pSpriteBatch);
    }
    public void Destroy()
    {
        isActive = false;
    }
    #endregion
}

