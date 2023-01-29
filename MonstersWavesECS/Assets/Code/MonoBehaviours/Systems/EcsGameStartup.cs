using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Systems;
using Code.Ecs.Units.Systems;
using Code.MonoBehaviours.World;
using Code.SO;
using Code.Test;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Code.MonoBehaviours.Systems
{
    public class EcsGameStartup : MonoBehaviour
    {
        [SerializeField] private SpawnReferenceSO _spawnReferenceSo;
        [SerializeField] private SceneEnvironment _sceneEnvironment;
        [SerializeField] private EnemySpawnChanceSO _enemySpawnChanceSo;
        [SerializeField] private InializePoolDataSO _initializePoolDataSo;
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
            // DontDestroyOnLoad(this);
            // SceneManager.LoadScene(StringStatic.Game);
        }

        private void Update()
        {
            _systems.Run();
        }

        private void AddInjections()
        {
            _systems.
                Inject(_sceneEnvironment).
                Inject(_enemySpawnChanceSo).
                Inject(_initializePoolDataSo).
                Inject(_spawnReferenceSo);
        }
        
        private void AddSystems()
        {
            _systems.
                Add(new InputSystem()).
                Add(new EnemySpawnerSystem()).
                Add(new EnemyCalculatingMovementSystem()).
                Add(new BulletsAttackSystem()).
                Add(new ReloadingSystem()).
                Add(new MovementSystem()).
                Add(new MouseRotationSystem()).
                Add(new GunFireSystem()).
                Add(new MoveForwardSystem()).
                Add(new AttackingSystem()).
                Add(new LifeTimerSystem()).
                Add(new SceneSpawnPoolSystem()).
                Add(new EntityInitializeSystem());
        }

        private void AddOneFrames()
        {
            _systems.
                OneFrame<GOToSpawnRequest>().
                OneFrame<NeedToDestroyEvent>().
                OneFrame<ObjectAttackedRequest>().
                OneFrame<GunFireEvent>();
        }

        private void OnDestroy()
        {
            if (_systems == null) return;
            
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}
