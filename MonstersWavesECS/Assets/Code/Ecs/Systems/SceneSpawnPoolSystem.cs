using System.Linq;
using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Code.MonoBehaviours.World;
using Code.SO;
using Code.Test;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class SceneSpawnPoolSystem :  IEcsInitSystem, IEcsRunSystem
    {
        #region fields
        private readonly SpawnReferenceSO _spawnReferenceSo = null;
        private readonly SceneEnvironment _sceneEnvironment = null;
        private readonly InializePoolDataSO _initializePoolDataSo = null;
        
        private readonly EcsFilter<ScenePoolSpawnerTag, GOToSpawnRequest> _spawnFilter = null;
        private readonly EcsFilter<NeedToDestroyEvent, PoolComponent> _needToDestroyFilter = null;
        private readonly EcsWorld _world = null;

        private ReInitializerEntitiesPoolSystem _reInitializerEntities;
        private SpawnInitializeEntity _spawnInitializeEntity;
        #endregion
        
        public void Init()
        {
            _reInitializerEntities = new ReInitializerEntitiesPoolSystem(_initializePoolDataSo);
            _spawnInitializeEntity = new SpawnInitializeEntity(_initializePoolDataSo, _world);
        }
        
        public void Run()
        {
            SpawnUnits();
            DeSpawnUnits();
        }

        #region Private
        private void SpawnUnits()
        {
            foreach (var i in _spawnFilter)
            {
                ref var goToSpawn = ref _spawnFilter.Get2(i);
                
                var isDeadFilter =
                    _world.GetFilter(typeof(EcsFilter<PoolComponent, IsDestroyedTag>.Exclude<NeedToDestroyEvent>));

                bool isSpawned = false;
                foreach (var go in goToSpawn.GoToSpawns)
                {
                    foreach (var j in isDeadFilter)
                    {
                        ref var deadEntity = ref isDeadFilter.GetEntity(j);
                        ref var deadEntityPoolComponent = ref deadEntity.Get<PoolComponent>();

                        ref var deadpoolObjectEnum = ref deadEntityPoolComponent.poolObjectEnum;
                        ref var deadTransform = ref deadEntityPoolComponent.transform;
                        
                        // if (deadpoolObjectEnum == go.poolObjectEnum)
                        // {
                        //     deadTransform.position = go.position;
                        //     deadTransform.rotation = go.quaternion;
                        //     deadTransform.SetParent(_sceneEnvironment.DynamicParent);
                        //     deadEntity.Del<IsDestroyedTag>();
                        //     _reInitializerEntities.ReInitializeByType(ref deadEntity, go.poolObjectEnum, _initializePoolDataSo);
                        //     isSpawned = true;
                        //     var gun = _world.NewEntity();
                        //     break;
                        // }
                    }

                    if (!isSpawned)
                    {
                        var spawnRef = _spawnReferenceSo.UnitTypeToSpawns.FirstOrDefault(entityReference => 
                            entityReference.PoolObjectEnum == go.poolObjectEnum);

                        var goGameObject = Object.Instantiate(spawnRef.Prefab, go.position, go.quaternion,
                            _sceneEnvironment.DynamicParent);

                        _spawnInitializeEntity.InitializeEntity(go.poolObjectEnum, goGameObject);
                        
                        isSpawned = false;
                    }
                }
            }
        }
        
        private void DeSpawnUnits()
        {
            foreach (var i in _needToDestroyFilter)
            {
                ref var entity = ref _needToDestroyFilter.GetEntity(i);
                entity.Get<IsDestroyedTag>();
                ref var poolComponent = ref entity.Get<PoolComponent>();
                ref var transform = ref poolComponent.transform;
                transform.SetParent(_sceneEnvironment.NonActiveParent);
            }
        }
        #endregion
        
    }
}