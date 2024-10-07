using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.GameObjects;

namespace Monogame_Project1.Engine.UIObjects;

public class AmmoUI : UIObject
{
    private ShootingSystem _shootingSystem;
    public AmmoUI(Texture2D pTexture, bool pActive = true) : base(pTexture, pActive) {}

    public override void LateLoad()
    {
        _shootingSystem =   
        base.LateLoad();
    }
}