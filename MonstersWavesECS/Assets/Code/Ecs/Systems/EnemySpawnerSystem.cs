using System.Collections.Generic;
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
    public class EnemySpawnerSystem : IEcsRunSystem
    {
        private readonly EnemySpawnChanceSO _enemySpawnChance = null;
        private readonly SceneEnvironment _sceneEnvironment = null;
        
        private readonly EcsFilter <EnemyTag, PoolComponent>.Exclude<IsDestroyedTag> _enemy = null;
        private readonly EcsFilter <ScenePoolSpawnerTag> _scenePoolSpawnerFilter = null;
       
        private const float SpawnRadius = 60;
        
        public void Run()
        {
            if (_enemy.GetEntitiesCount() < 100)
            {
                ref var getFirstSpawner = ref _scenePoolSpawnerFilter.GetEntity(0);
                
                ref var goToSpawnRequest = ref getFirstSpawner.Get<GOToSpawnRequest>();
                
                goToSpawnRequest.GoToSpawns ??= new Stack<GoToSpawn>();
                    
                Vector3 startPoint = _sceneEnvironment.CameraMain.transform.position;
                Vector2 randomPos = Random.insideUnitCircle;
                if (Mathf.Abs(randomPos.x) < 0.3f || Mathf.Abs(randomPos.y) < 0.3f )
                {
                    return;
                }
                startPoint.z += randomPos.y * SpawnRadius;
                startPoint.x += randomPos.x * SpawnRadius;
                startPoint.y = 0;
                    
                goToSpawnRequest.GoToSpawns.Push( new GoToSpawn
                {
                    poolObjectEnum = _enemySpawnChance.CallBackFromChance(),
                    position = startPoint,
                    quaternion = Quaternion.identity
                });
            }
        }

    }
}