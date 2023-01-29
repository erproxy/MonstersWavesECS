using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class LifeTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilter <LifeTimerComponent>.Exclude<IsDestroyedTag> _lifeTimerFilter = null;
        
        public void Run()
        {
            foreach (var i in _lifeTimerFilter)
            {
                ref var lifeTimer = ref _lifeTimerFilter.Get1(i);
                
                lifeTimer.timer -= Time.deltaTime;
                if (lifeTimer.timer <= 0)
                {
                    ref var entity = ref _lifeTimerFilter.GetEntity(i);
                    entity.Get<NeedToDestroyEvent>();
                }
            }
        }
    }
}