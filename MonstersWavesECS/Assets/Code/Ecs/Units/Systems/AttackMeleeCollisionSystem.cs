using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;

namespace Code.Ecs.Units.Systems
{
    public class AttackMeleeCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<OpponentAttackingMeleeRequest, DamageComponent, DefaultReloadingTimeComponent>.Exclude<ReloadingDurationRequest> _opponentAttackingSystem;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        
        public void Run()
        {
 
            foreach (var i in _opponentAttackingSystem)
            {
                ref var entity = ref _opponentAttackingSystem.GetEntity(i);
                
                ref var defaultReloadingTimeComponent = ref _opponentAttackingSystem.Get3(i);
                ref var defaultTime = ref defaultReloadingTimeComponent.Time;
                
                if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
                {
                    entity.Del<OpponentAttackingMeleeRequest>();
                    entity.Get<ReloadingDurationRequest>().TimerReloading = defaultTime;
                    return;
                }
                
                ref var opponentAttackingMeleeRequest = ref _opponentAttackingSystem.Get1(i);
                ref var damageComponent = ref _opponentAttackingSystem.Get2(i);
                ref var opponentEntity = ref opponentAttackingMeleeRequest.EcsEntity;

                ref var damage = ref damageComponent.Damage;
                opponentEntity.Get<ObjectAttackedRequest>().Damage = damage;
                

                entity.Get<ReloadingDurationRequest>().TimerReloading = defaultTime;
            }
        }
    }
}