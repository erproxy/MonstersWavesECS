using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class EnemyCalculatingMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyTag, ModelComponent, DirectionComponent>.Exclude<IsDestroyedTag> _enemyFilter = null;
        private readonly EcsFilter<PlayerTag, ModelComponent>.Exclude<IsDestroyedTag> _playerFilter = null;

        private Vector3 _playerPosition = Vector3.zero;
        private Transform _playerTransform = null;
        
        public void Run()
        {
            if (_playerFilter.GetEntitiesCount() > 0)
            {
                ref var player = ref _playerFilter.GetEntity(0);
                ref var modelComponent = ref player.Get<ModelComponent>();
                _playerPosition = modelComponent.modelTransform.position;
                _playerTransform = modelComponent.modelTransform;
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
                Vector3 direction = _playerPosition - modelComponent.modelTransform.position;
                direction = direction.normalized;
                directionComponent.Direction = direction;
                
                if (_playerTransform != null) 
                {
                    modelComponent.bodyTransform.LookAt(_playerTransform);
                }
            }
        }
    }
}