using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class LifeTimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter <LifeTimerComponent>.Exclude<IsDestroyedTag> _lifeTimerFilter = null;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        
        public void Run()
        {
 
            foreach (var i in _lifeTimerFilter)
            {
                if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
                {
                    NeedToDestroy(i);
                }
                
                ref var lifeTimer = ref _lifeTimerFilter.Get1(i);
                
                lifeTimer.Timer -= Time.deltaTime;
                
                if (lifeTimer.Timer <= 0)
                {
                    NeedToDestroy(i);
                }
            }
        }

        private void NeedToDestroy(int i)
        {
            ref var entity = ref _lifeTimerFilter.GetEntity(i);
            entity.Get<NeedToDestroyEvent>();
        }
    }
}