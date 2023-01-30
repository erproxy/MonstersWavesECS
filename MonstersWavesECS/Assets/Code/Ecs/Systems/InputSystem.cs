using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;
        private readonly EcsFilter<GunTag>.Exclude<ReloadingDurationRequest> _gunFilter = null;
        
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