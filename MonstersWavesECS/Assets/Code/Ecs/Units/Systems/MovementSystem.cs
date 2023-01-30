using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    sealed class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent>.Exclude<IsDestroyedTag> _movableFilter = null;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        public void Run()
        {
            
            if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
            {
                return;
            }
            
            foreach (var i in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(i);
                ref var movableComponent = ref _movableFilter.Get2(i);
                ref var directionComponent = ref _movableFilter.Get3(i);

                ref var direction = ref directionComponent.Direction;
                if (direction == null)
                {
                    direction = Vector3.zero;
                }

                ref var characterController = ref movableComponent.Rigidbody;
                ref var speed = ref movableComponent.Speed;
 
                characterController.MovePosition(modelComponent.ModelTransform.position + directionComponent.Direction * speed * Time.deltaTime);
            }
        }
    }
}