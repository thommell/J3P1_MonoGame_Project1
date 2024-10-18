namespace Monogame_Project1.Engine.JSON;

public struct GameInfo
{
    private int _levelCount;
    private int _score;
    private int _highScore;
    public GameInfo(int pLevelCount, int pScore, int pHighScore)
    {
        _levelCount = pLevelCount;
        _score = pScore;
        _highScore = pHighScore;
    }

    public GameInfo()
    {
        _levelCount = 1;
        _score = 0;
        _highScore = 0;
    }
    public int HighScore => _highScore;
    public int Score { get => _score; set => _score = value; }
    public int LevelCount { get => _levelCount; set => _levelCount = value; }
}