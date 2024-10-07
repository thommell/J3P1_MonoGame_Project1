using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class Target : GameObject
{
    #region Fields
    public int ScoreAmount { get; private set; }
    
    #endregion
    
    #region Constructors
    
    public Target(Texture2D pTexture, int pScoreAmount) : base(pTexture)
    {
        ScoreAmount = pScoreAmount;
    }
    
    #endregion
}

