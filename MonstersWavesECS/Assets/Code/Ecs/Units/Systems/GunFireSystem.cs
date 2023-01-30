using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;

namespace Code.Ecs.Units.Systems
{
    public class GunFireSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter <GunTag, ModelComponent, GunFireEvent, DefaultReloadingTimeComponent>.Exclude<IsDestroyedTag> _gunFireFilter = null;
           
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

            foreach (var i in _gunFireFilter)
            {
                ref var entity = ref _gunFireFilter.GetEntity(i);
                ref var model = ref _gunFireFilter.Get2(i);
                ref var defaultTimeAttackComponent = ref _gunFireFilter.Get4(i);
                ref var defaultTime = ref defaultTimeAttackComponent.Time;

                ref var goToSpawnRequest = ref _world.NewEntity().Get<GOToSpawnRequest>();
                goToSpawnRequest.PoolObjectEnum = PoolObjectEnum.Bullet;
                goToSpawnRequest.Position = model.BodyTransform.position;
                goToSpawnRequest.Quaternion = model.BodyTransform.rotation;
                
                entity.Get<ReloadingDurationRequest>().TimerReloading = defaultTime;
            }
        }
    }
}