using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;
        private readonly EcsFilter<GunTag>.Exclude<ReloadingDurationRequest> _gunFilter = null;
        public void Run()
        {
            MoveInput();
            FireInput();
        }

        private void MoveInput()
        {
            foreach (var i in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(i);
                
                directionComponent.Direction.x = Input.GetAxis("Horizontal");
                directionComponent.Direction.z = Input.GetAxis("Vertical");
            }
        }

        private void FireInput()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach (var i in _gunFilter)
                {
                    ref var entity = ref _gunFilter.GetEntity(i);
                    entity.Get<GunFireEvent>();
                }
            }
        }
    }
}