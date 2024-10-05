using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class ScoringSystem : GameObject
{
    #region Fields
    
    private Scene _currentScene;
    private int _score; 
    
    #endregion
    
    
    #region Properties
    
    public int CurrentScore { get => _score; set => _score = value; }
    
    #endregion
    
    #region Constructors
    
    public ScoringSystem(Scene pScene)
    {
        _currentScene = pScene;
    } 
    #endregion
    
    #region Public Methods
    
    public void AddScore(int pAddedScore)
    {
        _score += pAddedScore;
    }
    public void RemoveScore(int pRemovedScore)
    {
        _score -= pRemovedScore;
    }
    public void ResetScore()
    {
        _score = 0;
    } 
    #endregion
   
    
    
}