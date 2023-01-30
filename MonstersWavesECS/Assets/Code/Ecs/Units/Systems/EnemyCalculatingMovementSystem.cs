using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class EnemyCalculatingMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EnemyTag, ModelComponent, DirectionComponent>.Exclude<IsDestroyedTag> _enemyFilter = null;
        private readonly EcsFilter<PlayerTag, ModelComponent>.Exclude<IsDestroyedTag> _playerFilter = null;

        private Vector3 _playerPosition = Vector3.zero;
        private Transform _playerTransform = null;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        public void Run()
        {
            if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
                return;
            
            if (_playerFilter.GetEntitiesCount() > 0)
            {
                ref var player = ref _playerFilter.GetEntity(0);
                ref var modelComponent = ref player.Get<ModelComponent>();
                _playerPosition = modelComponent.ModelTransform.position;
                _playerTransform = modelComponent.ModelTransform;
            }
            else
            {
                _playerPosition = Vector3.zero;
                _playerTransform = null;
            }
            
            foreach (var i in _enemyFilter)
            {
                ref var modelComponent = ref _enemyFilter.Get2(i);
                ref var directionComponent = ref _enemyFilter.Get3(i);
                Vector3 direction = _playerPosition - modelComponent.ModelTransform.position;
                direction = direction.normalized;
                directionComponent.Direction = direction;
                
                if (_playerTransform != null) 
                {
                    modelComponent.BodyTransform.LookAt(_playerTransform);
                }
            }
        }
    }
}