using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public abstract class BaseTarget : GameObject
{
    #region Constructors

    public BaseTarget(Texture2D pTexture) : base(pTexture) { }

    #endregion

    #region Public Voids
    public virtual void OnShot() { }

    #endregion
}
