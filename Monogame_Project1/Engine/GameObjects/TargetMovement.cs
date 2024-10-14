using System;
using System.Data;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class TargetMovement 
{
    private Vector2 _direction;
    private Scene _currentScene;
    private BaseTarget _owner;
    private Random _random = new();
    private float _timer;
    private double _elapsedTime; 
    private const double ChangeInterval = 1.0;
    private float _speed;
    private Game1 _game;
    public TargetMovement(BaseTarget pOwner, double elapsedTime, float pSpeed, Game1 pGame)
    {
        _owner = pOwner;
        _elapsedTime = elapsedTime;
        _speed = pSpeed;
        _direction = GetRandomDirection();
        _game = pGame;
    }
    
    private static readonly Vector2[] Directions =
    {
        new(0, -1),   // Up
        new(1, -1),   // Up-Right
        new(1, 0),    // Right
        new(1, 1),    // Down-Right
        new(0, 1),    // Down
        new(-1, 1),   // Down-Left
        new(-1, 0),   // Left
        new(-1, -1)   // Up-Left
    };

    public Vector2 GetRandomDirection()
    {
        int index = _random.Next(0, Directions.Length);
        return Vector2.Normalize(Directions[index]);
    }

    public void Update(GameTime pGameTime)
    {
        SetDirection(pGameTime);
        ClampTarget();
    }

    private void SetDirection(GameTime pGameTime)
    {
        _elapsedTime += pGameTime.ElapsedGameTime.TotalSeconds;
        if (_elapsedTime >= ChangeInterval)
        {
            _direction = GetRandomDirection();
            _elapsedTime = 0; 
        }
        _owner.Position += _direction * (float)pGameTime.ElapsedGameTime.TotalSeconds * _speed; // 100 is movement speed 
    }
    private void ClampTarget()
    {
        // If the target goes out of bounds, invert direction
        if (_owner.Position.X < 0 + _owner.Origin.X || _owner.Position.X > _game.GraphicsDevice.Viewport.Width - _owner.Origin.X)
        {
            _direction.X = -_direction.X; // Invert X direction
            _elapsedTime = 0.0;
        }
        
        if (_owner.Position.Y < 0 + _owner.Origin.Y || _owner.Position.Y > _game.GraphicsDevice.Viewport.Height - _owner.Origin.Y)
        {
            _direction.Y = -_direction.Y; // Invert Y direction
            _elapsedTime = 0.0;
        }
    }

}