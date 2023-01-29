using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, MovableComponent, DirectionComponent>.Exclude<IsDestroyedTag> _movableFilter = null;
        
        public void Run()
        {

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

                ref var characterController = ref movableComponent.rigidbody;
                ref var speed = ref movableComponent.speed;
 
                characterController.MovePosition(modelComponent.modelTransform.position + directionComponent.Direction * speed * Time.deltaTime);
            }
        }
    }
}