using System.Collections.Generic;
using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;

namespace Code.Ecs.Units.Systems
{
    public class GunFireSystem : IEcsRunSystem
    {
        private readonly EcsFilter <GunTag, ModelComponent, GunFireEvent, DefaultTimeAttackComponent>.Exclude<IsDestroyedTag> _funFireFilter = null;
        private readonly EcsFilter <ScenePoolSpawnerTag> _scenePoolSpawnerFilter = null;
        
        public void Run()
        {
            foreach (var i in _funFireFilter)
            {
                ref var entity = ref _funFireFilter.GetEntity(i);
                ref var model = ref _funFireFilter.Get2(i);
                ref var defaultTimeAttackComponent = ref _funFireFilter.Get4(i);
                ref var defaultTime = ref defaultTimeAttackComponent.Time;
                ref var getFirstSpawner = ref _scenePoolSpawnerFilter.GetEntity(0);
                
                ref var goToSpawnRequest = ref getFirstSpawner.Get<GOToSpawnRequest>();
                    
                goToSpawnRequest.GoToSpawns ??= new Stack<GoToSpawn>();
                    
                goToSpawnRequest.GoToSpawns.Push( new GoToSpawn
                {
                    poolObjectEnum = PoolObjectEnum.Bullet,
                    position = model.bodyTransform.position,
                    quaternion = model.bodyTransform.rotation
                });

                entity.Get<ReloadingDurationRequest>().TimerReloading = 0;
            }
        }
    }
}