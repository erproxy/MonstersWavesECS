using System.Linq;
using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Ecs.Units.Providers;
using Code.MonoBehaviours.PoolPreset;
using Code.SO;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Models
{
    public class SpawnInitializeEntity
    {
        private readonly InializePoolDataSO _initializePoolDataSo = null;
        private readonly EcsWorld _world = null;

     
        public SpawnInitializeEntity(InializePoolDataSO initializePoolDataSo, EcsWorld ecsWorld)
        {
            _initializePoolDataSo = initializePoolDataSo;
            _world = ecsWorld;
        }
        
        public void InitializeEntity(PoolObjectEnum poolObjectEnum, GameObject go)
        {
            switch (poolObjectEnum)
            {
                case PoolObjectEnum.Zombie:
                case PoolObjectEnum.Ptero:
                    GenerateEnemyEntity(poolObjectEnum, go);
                    break;
            }
        }
        #region GenerateNewEntity

        private void GenerateEnemyEntity(PoolObjectEnum poolObjectEnum, GameObject go)
        {
            var enemyInitializeDataPool = _initializePoolDataSo.EnemyInitializeDataPool.FirstOrDefault(enemyInitializeDataPool => 
                enemyInitializeDataPool.PoolObjectEnum == poolObjectEnum);
            
            DefaultUnitPoolPreset defaultUnitPoolPreset = go.GetComponent<DefaultUnitPoolPreset>();
            
            var entity = _world.NewEntity();
            defaultUnitPoolPreset.EntityReference.Entity = entity;
            entity.Get<EnemyTag>();
            
            entity.Get<MovableComponent>().rigidbody = defaultUnitPoolPreset.Rigidbody;
            entity.Get<MovableComponent>().speed = enemyInitializeDataPool.Speed;
            
            entity.Get<ModelComponent>().modelTransform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<ModelComponent>().bodyTransform = defaultUnitPoolPreset.BodyTransform;
            
            entity.Get<DirectionComponent>().Direction = Vector3.zero;
            
            entity.Get<PoolComponent>().transform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<PoolComponent>().poolObjectEnum = poolObjectEnum;

            entity.Get<HealthComponent>().healtf = enemyInitializeDataPool.Health;
            entity.Get<HealthComponent>().armor = enemyInitializeDataPool.Armor;
        }
        

        #endregion
        
    }
}