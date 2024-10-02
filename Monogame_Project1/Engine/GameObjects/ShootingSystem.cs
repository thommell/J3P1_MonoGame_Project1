using System;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;
public class ShootingSystem : GameObject
{
    private int _ammo;
    private bool _hasShot;
    private Scene _scene;
    public ShootingSystem(Scene pScene, int pAmmo) 
    {
        _scene = pScene;
        _ammo = pAmmo;
    }
    public override void Update(GameTime gameTime) 
    {
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && !_hasShot && _ammo > 0) 
        {
            _ammo--;
            _hasShot = true;
            Console.WriteLine("Shot");
        } 
        else if (mouseState.LeftButton == ButtonState.Released && _hasShot) 
        {
            _hasShot = false;
        }        
    }
    public void CheckCollision(Target pTarget) 
    {
        MouseState mouseState = Mouse.GetState(); 
        Point mousePoint = new Point(mouseState.X, mouseState.Y); 
        if (pTarget.Rectangle.Contains(mousePoint) && _hasShot)
        {
            pTarget.Destroy();            
        } 
    }
}
