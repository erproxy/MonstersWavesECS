using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Units.Components;
using Code.Models;
using Leopotam.Ecs;

namespace Code.Ecs.Units.Systems
{
    public class AttackingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<HealthComponent, ObjectAttackedRequest> _attackedFilter;
            
                    
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        public void Run()
        {
            foreach (var i in _attackedFilter)
            {
                if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
                {
                    ref var entity = ref _attackedFilter.GetEntity(i);
                    entity.Del<ObjectAttackedRequest>();
                    return;
                }
                ref var healthComponent = ref _attackedFilter.Get1(i);
                ref var objectAttackedRequest = ref _attackedFilter.Get2(i);

                ref var health = ref healthComponent.Health;
                ref var armor = ref healthComponent.Armor;
                ref var damage = ref objectAttackedRequest.Damage;

                health -= damage * armor;

                if (health <= 0)
                {
                    health = 0;

                    ref var entity = ref _attackedFilter.GetEntity(i);
                    entity.Get<NeedToDestroyEvent>();
                }
            }
        }
    }
}