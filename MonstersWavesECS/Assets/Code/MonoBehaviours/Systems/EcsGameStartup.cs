using Code.Ecs.Requests;
using Code.Ecs.Systems;
using Code.Ecs.Ui.Systems;
using Code.Ecs.Units.Systems;
using Code.MonoBehaviours.World;
using Code.SO;
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
        [SerializeField] private ScoreDataSO _scoreDataSO;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _systemsSpawner;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systemsSpawner = new EcsSystems(_world);
            
            _systems.ConvertScene();
            _systemsSpawner.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
            _systemsSpawner.Init();
        }

        private void Update()
        {
            _systems.Run();
            _systemsSpawner.Run();
        }

        private void AddInjections()
        {
            _systemsSpawner.
                Inject(_sceneEnvironment).
                Inject(_enemySpawnChanceSo);
            
            _systems.
                Inject(_sceneEnvironment).
                Inject(_enemySpawnChanceSo).
                Inject(_initializePoolDataSo).
                Inject(_scoreDataSO).
                Inject(_spawnReferenceSo);
        }
        
        private void AddSystems()
        {
            _systemsSpawner.
                Add(new EnemySpawnerSystem()).
                Add(new PlayerSpawnerSystem());
            
            _systems.
                Add(new EntityInitializeSystem()).
                Add(new InputSystem()).
                Add(new EnemyCalculatingMovementSystem()).
                Add(new BulletsAttackSystem()).
                Add(new AttackMeleeCollisionSystem()).
                Add(new ReloadingSystem()).
                Add(new MovementSystem()).
                Add(new MouseRotationSystem()).
                Add(new GunFireSystem()).
                Add(new MoveForwardSystem()).
                Add(new AttackingSystem()).
                Add(new LifeTimerSystem()).
                Add(new CalculatingScoreSystem()).
                Add(new SceneSpawnPoolSystem()).
                Add(new UiControlSystem()).
                Add(new GameStateSystem());
        }

        private void AddOneFrames()
        {
            _systems.
                OneFrame<ObjectAttackedRequest>().
                OneFrame<EnemyKilledRequest>().
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
