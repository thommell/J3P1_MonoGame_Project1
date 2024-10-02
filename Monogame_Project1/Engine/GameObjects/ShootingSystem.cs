using Microsoft.Xna.Framework.Input;
using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;
public class ShootingSystem
{
    private int _ammo;

    private bool _shot = false;
    public ShootingSystem(int pAmmo) {
        
         _ammo = pAmmo;
    }
    public void Update(GameTime gameTime) {

        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed && !_shot && _ammo > 0) 
        {
            _ammo--;
            _shot = true;
            Console.WriteLine("Shot");
        } 
        else if (mouseState.LeftButton == ButtonState.Released && _shot) 
        {
            _shot = false;
        }        
    }
    public void CheckCollision(Target target) {
        
        MouseState mouseState = Mouse.GetState(); 
        Point mousePoint = new Point(mouseState.X, mouseState.Y); 

        if (target.Rectangle.Contains(mousePoint) && _shot)
        {
            target.Destroy();            
        } 
    }
}
