using Monogame_Project1.Engine.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class SliderButton : Button
{
    private float _leftSide;
    private float _rightSide;

    public SliderButton(Texture2D pTexture, float pLeftSide, float pRightSide, bool pIsActive = false) : base(pTexture, null, pIsActive) 
    {
        _leftSide = pLeftSide;
        _rightSide = pRightSide;
    }
    protected override void OnHold()
    {
        MouseState mouseState = Mouse.GetState();
        Position = new Vector2(Math.Clamp(mouseState.X, _leftSide, _rightSide), position.Y);
    }
}
