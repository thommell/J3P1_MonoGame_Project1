using Microsoft.Xna.Framework.Input;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_Project1.Engine.GameObjects;

public class PowerUps : GameObject
{
    private Dictionary<string, Func<BaseTarget>> _powerUps = new();
    private SpawningSystem _spawningSystem;

    public PowerUps()
    {
        _spawningSystem = SceneManager.Instance.CurrentScene.GetObject<SpawningSystem>();   
    }
    public override void LoadContent(ContentManager pContent)
    {
        //Adds a function to create a new BaseTarget
        _powerUps.Add("TimeTarget", () => new TimeTarget(pContent.Load<Texture2D>("ExtraTimeClock")));

        base.LoadContent(pContent);
    }
    public void SpawnRandomPowerUp()
    {
        Random random = new();

        int randomNumber = random.Next(0, _powerUps.Count);

        string key = _powerUps.Keys.ElementAt(randomNumber);

        BaseTarget powerUp = _powerUps[key]();

        _spawningSystem.SpawnPowerUp(powerUp);
    }
    public void SpawnPowerUp(string pPowerUpName)
    {
        BaseTarget powerUp = _powerUps[pPowerUpName]();

        _spawningSystem.SpawnPowerUp(powerUp);
    }
}
