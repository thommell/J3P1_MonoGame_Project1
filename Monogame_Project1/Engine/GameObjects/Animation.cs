using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class Animation : GameObject
{
    #region Fields
    private float _time;
    private readonly float _animationSpeed = 0.1f;

    private readonly int _lengthX = 3;
    private readonly int _lengthY = 3;

    private int _x = 0;
    private int _y = 0;
    #endregion

    #region Constructors
    public Animation(Texture2D pTexture, Vector2 pPosition, int pLengthX, int pLengthY, float pAnimationSpeed = 0.1f)
    {
        texture = pTexture;
        position = pPosition;
        _lengthX = pLengthX;
        _lengthY = pLengthY;
        _animationSpeed = pAnimationSpeed;
    }
    #endregion

    #region Methods
    public override void Update(GameTime pGameTime)
    {
        _time += (float)pGameTime.ElapsedGameTime.TotalSeconds;

        if (_time >= _animationSpeed && _x < _lengthX && _y < _lengthY)
        {
            _time = 0;
            NextFrame();
        }
        else if (_time >= _animationSpeed && _y >= _lengthY) DeactivateObject(this);
    }
    private void NextFrame()
    {
        //selects next frame
        if (_x >= _lengthX - 1)
        {
            _x = 0;
            _y++;
        }
        else
        {
            _x++;
        }
    }
    public override void Draw(SpriteBatch pSpriteBatch)
    {
        int width = texture.Width / _lengthX;
        int heigth = texture.Height / _lengthY;

        Rectangle spriteRec = new Rectangle(_x * width, _y * heigth, width, heigth);

        Rectangle positionRec = new Rectangle((int)position.X - width / 2, (int)position.Y - heigth / 2, width, heigth);

        pSpriteBatch.Draw(texture, positionRec, spriteRec, Color.White);
    }
    #endregion
}
