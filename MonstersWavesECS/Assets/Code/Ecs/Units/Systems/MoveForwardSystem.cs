using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class MoveForwardSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<MoveForwardComponent>.Exclude<IsDestroyedTag> _moveForwardComponent = null;
        
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
            
            foreach (var i in _moveForwardComponent)
            {
                ref var model = ref _moveForwardComponent.Get1(i);

                model.ModelTransform.Translate(model.ModelTransform.forward * model.Speed * Time.deltaTime, Space.World);
            }
        }
    }
}