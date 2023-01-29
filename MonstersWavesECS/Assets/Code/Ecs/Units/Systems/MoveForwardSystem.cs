using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class MoveForwardSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveForwardComponent>.Exclude<IsDestroyedTag> _moveForwardComponent = null;

        public void Run()
        {
            foreach (var i in _moveForwardComponent)
            {
                ref var model = ref _moveForwardComponent.Get1(i);

                model.modelTransform.Translate(model.modelTransform.forward * model.speed * Time.deltaTime, Space.World);
            }
        }
    }
}