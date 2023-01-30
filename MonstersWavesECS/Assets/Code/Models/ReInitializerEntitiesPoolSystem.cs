using Code.Ecs.Components;
using Code.Ecs.Units.Components;
using Code.SO;
using Leopotam.Ecs;

namespace Code.Models
{
    public class ReInitializerEntitiesPoolSystem
    {
        private readonly InializePoolDataSO _initializePoolDataSo = null;
        public ReInitializerEntitiesPoolSystem(InializePoolDataSO initializePoolDataSo)
        {
            _initializePoolDataSo = initializePoolDataSo;
        }

        public void ReInitializeByType(ref EcsEntity entity, PoolObjectEnum poolObjectEnum)
        {
            switch (poolObjectEnum)
            {
                case PoolObjectEnum.Zombie:
                case PoolObjectEnum.Ptero:
                    ReInitializeEnemy(ref entity, poolObjectEnum);
                    break;
                
                case PoolObjectEnum.Player:
                    ReInitializePlayer(ref entity);
                    break;
                                
                case PoolObjectEnum.Bullet:
                    ReInitializeBullet(ref entity);
                    break;
            }
        }
        private void ReInitializeEnemy(ref EcsEntity entity, PoolObjectEnum poolObjectEnum)
        {
            var enemyInitializeDataPool = _initializePoolDataSo.CallbackEnemyData(poolObjectEnum);
            ReInitHealth(ref entity, enemyInitializeDataPool.Health);

        }
        
        private void ReInitializePlayer(ref EcsEntity entity)
        {
            var playerInitializeDataPool = _initializePoolDataSo.PlayerInitializeDataPool;
            ReInitHealth(ref entity, playerInitializeDataPool.Health);
        }
        
        private void ReInitializeBullet(ref EcsEntity entity)
        {
            var bulletInitializeDataPool = _initializePoolDataSo.BulletInitializeDataPool;
            
            ref var lifeTimerComponent = ref entity.Get<LifeTimerComponent>();
            lifeTimerComponent.Timer = bulletInitializeDataPool.LifeTimer;
        }

        private void ReInitHealth(ref EcsEntity entity, float health)
        {
            ref var lifeTimerComponent = ref entity.Get<HealthComponent>();
            lifeTimerComponent.Health = health;
        }
    }
}