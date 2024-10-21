using System;
using Monogame_Project1.Engine.BaseClasses;
using Monogame_Project1.Engine.Singletons;

namespace Monogame_Project1.Engine.GameObjects
{
    public class SpawningSystem : GameObject
    {
        private readonly Scene _scene;
        private ShootingSystem _shootingSystem;
        private bool _hasSpawned;
        public bool HasSpawned { get { return _hasSpawned; }set { _hasSpawned = value; } }
        public List<GameObject> currentTargets = new();

        public SpawningSystem(Scene pScene)
        {
            string texture = _random.Next(100) < 5 ? "Potoo" : "Target";
            Target newTarget = new Target(SceneManager.Instance.Game.Content.Load<Texture2D>(texture), _scene, 2)
            {
                Position = GetPosition()
            };
            // Temp Fix
            newTarget.MovementSystem = CreateMovement(newTarget);
            
            _scene.Objects.Add(newTarget); 
            currentTargets.Add(newTarget);
        }

        public override void LateLoad()
        {
            string texture = _random.Next(2) == 0 ? "Bomb" : "TNT";
            FakeTarget newTarget = new FakeTarget(SceneManager.Instance.Game.Content.Load<Texture2D>(texture))
            {
                Position = GetPosition(),
                Color = Color.Green
            };
            
            newTarget.MovementSystem = CreateMovement(newTarget);            
            _scene.Objects.Add(newTarget);
            currentTargets.Add(newTarget);
            _hasSpawned = true;
        }

        return new(random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Width - 64),
            random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height - 200)
        ); 
    }

        // This method will be called by the WaveManager to start spawning new targets
        public void StartSpawner(int amountToSpawn, int fakesAmount)
        {
            CreateNewTargets(amountToSpawn, fakesAmount);
        }

        private void CreateNewTargets(int amountToSpawn, int fakesAmount)
        {
            _scene.DeactivateObjects(currentTargets);
            currentTargets.Clear();
            SpawnTargets(amountToSpawn, fakesAmount);
        }

        private void SpawnTargets(int amountToSpawn, int fakesAmount)
        {
            Random random = new Random();

            for (int i = 0; i < amountToSpawn; i++)
            {
                string texture = random.Next(100) < 5 ? "Potoo" : "Target";
                Target newTarget = new Target(SceneManager.Instance.Game.Content.Load<Texture2D>(texture), _scene, 2)
                {
                    Position = GetPosition()
                };
                newTarget.MovementSystem = CreateMovement(newTarget);

                _scene.Objects.Add(newTarget);
                currentTargets.Add(newTarget);
            }

            for (int i = 0; i < fakesAmount; i++)
            {
                string texture = random.Next(2) == 0 ? "Bomb" : "TNT";
                FakeTarget newTarget = new FakeTarget(SceneManager.Instance.Game.Content.Load<Texture2D>(texture))
                {
                    Position = GetPosition(),
                    Color = Color.Green
                };

                newTarget.MovementSystem = CreateMovement(newTarget);
                _scene.Objects.Add(newTarget);
                currentTargets.Add(newTarget);
            }

            _hasSpawned = true;  // Mark the spawn complete
        }

        public Vector2 GetPosition()
        {
            Random random = new();

            return new Vector2(
                random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Width - 64),
                random.Next(64, SceneManager.Instance.Game.GraphicsDevice.Viewport.Height - 64)
            );
        }

        private TargetMovement CreateMovement(BaseTarget pOwner)
        {
            Random random = new();
            int[] speedValues = { 100, 350 };
            int[] elapsedValues = { 1, 5 };

            return new TargetMovement(pOwner, random.Next(elapsedValues[0], elapsedValues[1]), random.Next(speedValues[0], speedValues[1]));
        }
    }
}
