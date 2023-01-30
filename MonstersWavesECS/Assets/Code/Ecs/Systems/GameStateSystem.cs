using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Models;
using Leopotam.Ecs;

namespace Code.Ecs.Systems
{
    public class GameStateSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag, IsDestroyedTag> _playerDeadFilter = null;
        private readonly EcsFilter<PlayerTag>.Exclude<IsDestroyedTag> _playerLifeFilter = null;
        private readonly EcsFilter<RestartEvent> _restartFilter = null;

        private EcsComponentRef<GameStateComponent> _gameStateRef;
        
        public void PreInit()
        {
            var gameStateEntity = _world.NewEntity();
            gameStateEntity.Get<GameStateComponent>();
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
            _gameStateRef.Unref().GameStateEnum = GameStateEnum.None;
        }

        public void Run()
        {
            ref var gameStateEnum = ref _gameStateRef.Unref().GameStateEnum;
            
            switch (gameStateEnum)
            {
                case GameStateEnum.None:
                    gameStateEnum = GameStateEnum.StartSetup;
                    break;
                case GameStateEnum.StartSetup:
                    if (_playerLifeFilter.GetEntitiesCount() > 0)
                    {                   
                        gameStateEnum = GameStateEnum.Play;
                    }
                    break;
                
                case GameStateEnum.Play:
                    if (_playerDeadFilter.GetEntitiesCount() > 0)
                    {
                        gameStateEnum = GameStateEnum.LoseGame;
                    }
                    break;
                case GameStateEnum.LoseGame:
                    gameStateEnum = GameStateEnum.ShowingRestart;
                    break;
                
                case GameStateEnum.ShowingRestart:
                    if (_restartFilter.GetEntitiesCount() > 0)
                    {
                        gameStateEnum = GameStateEnum.Restart;
                        _restartFilter.GetEntity(0).Destroy();
                    }
                    break;
                
                case GameStateEnum.Restart:
                    gameStateEnum = GameStateEnum.StartSetup;
                    break;
            }
            
        }

    }
}