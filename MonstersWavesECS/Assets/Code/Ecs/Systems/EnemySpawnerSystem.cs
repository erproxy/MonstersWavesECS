using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Models;
using Code.MonoBehaviours.World;
using Code.SO;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class EnemySpawnerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EnemySpawnChanceSO _enemySpawnChance = null;
        private readonly SceneEnvironment _sceneEnvironment = null;
        
        private readonly EcsFilter <EnemyTag, PoolComponent>.Exclude<IsDestroyedTag> _enemy = null;
       
        private const float SpawnRadius = 50;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        
        public void Run()
        {
            ref var gameState = ref _gameStateRef.Unref().GameStateEnum;

            switch (gameState)
            {
                case GameStateEnum.Play:
                    SetupEnemy();
                    break;
                case GameStateEnum.ShowingRestart:
                    DeleteEnemy();
                    break;
            }
        }

        private void SetupEnemy()
        {
            if (_enemy.GetEntitiesCount() < 100)
            {
                Vector3 startPoint = _sceneEnvironment.CameraMain.transform.position;
                Vector2 randomPos = Random.insideUnitCircle;
                if (Mathf.Abs(randomPos.x) < 0.6f || Mathf.Abs(randomPos.y) < 0.6f )
                {
                    return;
                }
                
                startPoint.z += randomPos.y * SpawnRadius;
                startPoint.x += randomPos.x * SpawnRadius;
                startPoint.y = 0;
                
                ref var goToSpawnRequest = ref _world.NewEntity().Get<GOToSpawnRequest>();

                goToSpawnRequest.PoolObjectEnum = _enemySpawnChance.CallBackFromChance();
                goToSpawnRequest.Position = startPoint;
                goToSpawnRequest.Quaternion = Quaternion.identity;
            }
        }

        private void DeleteEnemy()
        {
            foreach (var i in _enemy)
            {
                ref var entity = ref _enemy.GetEntity(i);
                entity.Get<NeedToDestroyEvent>();
            }
        }
    }
}