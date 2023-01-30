using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Ecs.Ui.Components;
using Code.Ecs.Units.Components;
using Code.Models;
using Code.SO;
using Leopotam.Ecs;

namespace Code.Ecs.Ui.Systems
{
    public class UiControlSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly InializePoolDataSO _initializePoolDataSo = null;

        private readonly EcsFilter<PlayerTag, HealthComponent, ObjectAttackedRequest> _playerAttackedFilter = null;
        private readonly EcsFilter<EnemyKilledRequest> _enemyKilledFilter = null;
        
        private float _defaultPlayerHp = 100;

        private EcsComponentRef<GameStateComponent> _gameStateRef;
        private EcsComponentRef<GameWindowComponent> _gameWindowComponentRef;
        private EcsComponentRef<RestartWindowComponent> _restartWindowComponentRef;
        
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
            
            var gameWindowEntity = _world.GetFilter(typeof(EcsFilter<GameWindowComponent>)).GetEntity(0);
            _gameWindowComponentRef = gameWindowEntity.Ref<GameWindowComponent>();
            
            var restartWindowEntity = _world.GetFilter(typeof(EcsFilter<RestartWindowComponent>)).GetEntity(0);
            _restartWindowComponentRef = restartWindowEntity.Ref<RestartWindowComponent>();
            
            
            _defaultPlayerHp = _initializePoolDataSo.PlayerInitializeDataPool.Health;
        }
 
        public void Run()
        {
            if (_playerAttackedFilter.GetEntitiesCount() > 0)
            {
                SetupHealth();
            }

            foreach (var i in _enemyKilledFilter)
            {
                ref var enemyKilledRequest = ref _enemyKilledFilter.Get1(i);
                ref var score = ref enemyKilledRequest.Score;

                _gameWindowComponentRef.Unref().scoreValueLabel.text = score.ToString();
            }

            switch (_gameStateRef.Unref().GameStateEnum)
            {
                case GameStateEnum.StartSetup:
                    _gameWindowComponentRef.Unref().hpValueLabel.text = _initializePoolDataSo.PlayerInitializeDataPool.Health.ToString();
                    _gameWindowComponentRef.Unref().barSlide.fillAmount = 1;
                    _gameWindowComponentRef.Unref().scoreValueLabel.text = 0.ToString();
                    break;
                    
                case GameStateEnum.LoseGame:
                    SetupRestartWindow(true);
                    break;
                    
                case GameStateEnum.Restart:
                    SetupRestartWindow(false);
                    break;
            }
        }

        private void SetupHealth()
        {
            ref var healthComponent = ref _playerAttackedFilter.Get2(0);

            ref var health = ref healthComponent.Health;
            _gameWindowComponentRef.Unref().hpValueLabel.text = health.ToString();
            _gameWindowComponentRef.Unref().barSlide.fillAmount = health / _defaultPlayerHp;
        }

        private void SetupRestartWindow(bool value)
        {
            _restartWindowComponentRef.Unref().restartWindow.SetActive(value);
        }
    }
}