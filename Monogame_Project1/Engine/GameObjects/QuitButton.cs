using System;
using Monogame_Project1.Engine.BaseClasses;

namespace Monogame_Project1.Engine.GameObjects;

public class QuitButton : Button
{
    public QuitButton(Texture2D pTexture, string text) : base(pTexture, text) { }
    protected override void OnClick()
    {
        Environment.Exit(0);
    }
}