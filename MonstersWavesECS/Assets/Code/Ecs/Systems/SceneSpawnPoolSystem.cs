using System.Linq;
using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Code.MonoBehaviours.World;
using Code.SO;
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
        
        private readonly EcsFilter<GOToSpawnRequest> _spawnFilter = null;
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
                ref var entitySpawn = ref _spawnFilter.GetEntity(i);
                ref var goToSpawnRequest = ref _spawnFilter.Get1(i);
                var poolObjectEnumToSpawn =  goToSpawnRequest.PoolObjectEnum;
                
                var isDeadFilter =
                    _world.GetFilter(typeof(EcsFilter<PoolComponent, IsDestroyedTag>.Exclude<NeedToDestroyEvent>));

                bool isSpawned = false;
                foreach (var j in isDeadFilter)
                {
                    ref var deadEntity = ref isDeadFilter.GetEntity(j);
                    ref var deadEntityPoolComponent = ref deadEntity.Get<PoolComponent>();

                    ref var deadpoolObjectEnum = ref deadEntityPoolComponent.PoolObjectEnum;
                    ref var deadTransform = ref deadEntityPoolComponent.Transform;
                        
                    if (deadpoolObjectEnum == goToSpawnRequest.PoolObjectEnum)
                    {
                        deadTransform.position = goToSpawnRequest.Position;
                        deadTransform.rotation = goToSpawnRequest.Quaternion;
                        deadTransform.SetParent(_sceneEnvironment.DynamicParent);
                        deadEntity.Del<IsDestroyedTag>();
                        
                        _reInitializerEntities.ReInitializeByType(ref deadEntity, poolObjectEnumToSpawn);
                        
                        isSpawned = true;
                        break;
                    }
                }

                if (!isSpawned)
                {
                    var spawnRef = _spawnReferenceSo.CallbackUnityType(poolObjectEnumToSpawn);

                    var goGameObject = Object.Instantiate(spawnRef.Prefab, goToSpawnRequest.Position, goToSpawnRequest.Quaternion,
                        _sceneEnvironment.DynamicParent);

                    _spawnInitializeEntity.InitializeEntity(goToSpawnRequest.PoolObjectEnum, goGameObject);
                }
                
                entitySpawn.Destroy();
            }
        }
        
        private void DeSpawnUnits()
        {
            foreach (var i in _needToDestroyFilter)
            {
                ref var entity = ref _needToDestroyFilter.GetEntity(i);
                entity.Get<IsDestroyedTag>();
                entity.Del<NeedToDestroyEvent>();
                ref var poolComponent = ref entity.Get<PoolComponent>();
                ref var transform = ref poolComponent.Transform;
                transform.SetParent(_sceneEnvironment.NonActiveParent);
            }
        }
        #endregion
        
    }
}