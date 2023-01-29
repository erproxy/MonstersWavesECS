using Code.Ecs.Components;
using Code.SO;
using Leopotam.Ecs;

namespace Code.Models
{
    public class ReInitializerEntitiesPoolSystem
    {
        public void ReInitializeByType(ref EcsEntity entity, PoolObjectEnum poolObjectEnum,
            InializePoolDataSO inializePoolDataSo)
        {
            switch (poolObjectEnum)
            {
                case PoolObjectEnum.Bullet:
                    ReInitializeBullet(ref entity);
                    break;
            }
        }

        private void ReInitializeBullet(ref EcsEntity entity)
        {
            ref var lifeTimerComponent = ref entity.Get<LifeTimerComponent>();
            lifeTimerComponent.timer = lifeTimerComponent.timerDefault;
        }
    }
}