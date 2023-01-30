using System.Linq;
using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
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
                
                case PoolObjectEnum.Player:
                    GeneratePlayerEntity(poolObjectEnum, go);
                    break;
                                
                case PoolObjectEnum.Bullet:
                    GenerateBulletEntity(poolObjectEnum, go);
                    break;
            }
        }
        #region GenerateNewEntity

        private void GenerateEnemyEntity(PoolObjectEnum poolObjectEnum, GameObject go)
        {
            var enemyInitializeDataPool = _initializePoolDataSo.CallbackEnemyData(poolObjectEnum);
            
            DefaultUnitPoolPreset defaultUnitPoolPreset = go.GetComponent<DefaultUnitPoolPreset>();
            
            var entity = _world.NewEntity();
            defaultUnitPoolPreset.EntityReference.Entity = entity;
            entity.Get<EnemyTag>();
            
            entity.Get<MovableComponent>().Rigidbody = defaultUnitPoolPreset.Rigidbody;
            entity.Get<MovableComponent>().Speed = enemyInitializeDataPool.Speed;
            
            entity.Get<ModelComponent>().ModelTransform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<ModelComponent>().BodyTransform = defaultUnitPoolPreset.BodyTransform;
            
            entity.Get<DirectionComponent>().Direction = Vector3.zero;
            
            entity.Get<PoolComponent>().Transform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<PoolComponent>().PoolObjectEnum = poolObjectEnum;
            
            entity.Get<HealthComponent>().Health = enemyInitializeDataPool.Health;
            entity.Get<HealthComponent>().Armor = enemyInitializeDataPool.Armor;
            
            entity.Get<DamageComponent>().Damage = enemyInitializeDataPool.Damage;
            entity.Get<DefaultReloadingTimeComponent>().Time = enemyInitializeDataPool.ReloadingTimer;
        }
        
        private void GeneratePlayerEntity(PoolObjectEnum poolObjectEnum, GameObject go)
        {
            var playerInitializeDataPool = _initializePoolDataSo.PlayerInitializeDataPool;
            
            DefaultUnitPoolPreset defaultUnitPoolPreset = go.GetComponent<DefaultUnitPoolPreset>();
            
            var entity = _world.NewEntity();
            defaultUnitPoolPreset.EntityReference.Entity = entity;
            entity.Get<PlayerTag>();
            
            entity.Get<MovableComponent>().Rigidbody = defaultUnitPoolPreset.Rigidbody;
            entity.Get<MovableComponent>().Speed = playerInitializeDataPool.Speed;
            
            entity.Get<ModelComponent>().ModelTransform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<ModelComponent>().BodyTransform = defaultUnitPoolPreset.BodyTransform;
            
            entity.Get<DirectionComponent>().Direction = Vector3.zero;
            
            entity.Get<PoolComponent>().Transform = defaultUnitPoolPreset.ModelTransform;
            entity.Get<PoolComponent>().PoolObjectEnum = poolObjectEnum;

            entity.Get<HealthComponent>().Health = playerInitializeDataPool.Health;
            entity.Get<HealthComponent>().Armor = playerInitializeDataPool.Armor;
            
            entity.Get<DefaultReloadingTimeComponent>().Time = playerInitializeDataPool.ReloadingTimer;
            entity.Get<MouseRotationTag>();
            entity.Get<GunTag>();
                        

        }
        
        private void GenerateBulletEntity(PoolObjectEnum poolObjectEnum, GameObject go)
        {
            var bulletInitializeDataPool = _initializePoolDataSo.BulletInitializeDataPool;
            
            BulletPoolPreset bulletPoolPreset = go.GetComponent<BulletPoolPreset>();
            
            var entity = _world.NewEntity();
            bulletPoolPreset.EntityReference.Entity = entity;
            entity.Get<BulletTag>();
            
            entity.Get<MoveForwardComponent>().ModelTransform = bulletPoolPreset.ModelTransform;
            entity.Get<MoveForwardComponent>().Speed = bulletInitializeDataPool.Speed;
            
            entity.Get<ModelComponent>().ModelTransform = bulletPoolPreset.ModelTransform;
            entity.Get<ModelComponent>().BodyTransform = bulletPoolPreset.BodyTransform;
            
            entity.Get<DirectionComponent>().Direction = Vector3.zero;
            
            entity.Get<PoolComponent>().Transform = bulletPoolPreset.ModelTransform;
            entity.Get<PoolComponent>().PoolObjectEnum = poolObjectEnum;
        
            entity.Get<LifeTimerComponent>().Timer = bulletInitializeDataPool.LifeTimer;

            entity.Get<AttackComponent>().Damage = bulletInitializeDataPool.Damage;
            entity.Get<AttackComponent>().AttackDistance = bulletPoolPreset.AttackDistance;
            entity.Get<AttackComponent>().PositionAttack = bulletPoolPreset.PositionAttack;
            entity.Get<AttackComponent>().WhatIsMask = bulletPoolPreset.WhatIsMask;
        }
        #endregion
        
    }
}