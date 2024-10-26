using System;
using System.Net.Mime;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects;

public class QuitButton : Button 
{
    public QuitButton(Texture2D pTexture, string text) : base(pTexture, text) { }
    protected override void OnClick()
    {
        SceneManager.Instance.Exit();
    }
}