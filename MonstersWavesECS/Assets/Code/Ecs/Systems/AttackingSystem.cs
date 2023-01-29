using Code.Ecs.Components;
using Code.Ecs.Requests;
using Leopotam.Ecs;

namespace Code.Ecs.Systems
{
    public class AttackingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, ObjectAttackedRequest> _attackedFilter;
            
            
        public void Run()
        {
            foreach (var i in _attackedFilter)
            {
                ref var healthComponent = ref _attackedFilter.Get1(i);
                ref var objectAttackedRequest = ref _attackedFilter.Get2(i);

                ref var health = ref healthComponent.healtf;
                ref var armor = ref healthComponent.armor;
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