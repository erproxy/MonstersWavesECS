using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class PlayerSpawnerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        
        public void Run()
        {
            switch (_gameStateRef.Unref().GameStateEnum)
            {
                case GameStateEnum.StartSetup:
                    SpawnPlayer();
                    break;
            }
        }

        private void SpawnPlayer()
        {
            ref var goToSpawnRequest = ref _world.NewEntity().Get<GOToSpawnRequest>();
            
            goToSpawnRequest.PoolObjectEnum = PoolObjectEnum.Player;
            goToSpawnRequest.Position = Vector3.zero;
            goToSpawnRequest.Quaternion = Quaternion.identity;
        }
    }
}