using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;
public class ShootingSystem
{
    public void Update(GameTime pGameTime)
    {

    }
    public void CheckCollision(Target target)
    {
        MouseState mouseState = Mouse.GetState(); //pakt alle gegevens van de muis
        Point mousePoint = new Point(mouseState.X, mouseState.Y); //zet de positie van de muis in een soort Vector2
    }
}
