using Code.Ecs.Requests;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class ReloadingSystem : IEcsRunSystem
    {
        private readonly EcsFilter <ReloadingDurationRequest> _reloadingFilter = null;
        
        public void Run()
        {
            foreach (var i in _reloadingFilter)
            {
                ref var entity = ref _reloadingFilter.GetEntity(i);
                ref var reloading = ref _reloadingFilter.Get1(i);
                
                reloading.TimerReloading -= Time.deltaTime;
                if ( reloading.TimerReloading <= 0)
                {
                    entity.Del<ReloadingDurationRequest>();
                }
            }
        }
    }
}