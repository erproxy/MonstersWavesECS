using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Models;
using Code.SO;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class CalculatingScoreSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<NeedToDestroyEvent> _needToDestroy;
        private readonly ScoreDataSO _scoreDataSo = null;

        private EcsComponentRef<GameStateComponent> _gameStateRef;

        private float _score = 0;

        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }

        public void Run()
        {

            switch (_gameStateRef.Unref().GameStateEnum)
            {
                case GameStateEnum.StartSetup:
                    _score = 0;
                    break;

                case GameStateEnum.Play:
                    foreach (var i in _needToDestroy)
                    {
                        ref var entity = ref _needToDestroy.GetEntity(i);
                        ref var poolComponent = ref entity.Get<PoolComponent>();

                        switch (poolComponent.PoolObjectEnum)
                        {
                            case PoolObjectEnum.Zombie:
                            case PoolObjectEnum.Ptero:
                                var scorePerType = _scoreDataSo.CallbackScore(poolComponent.PoolObjectEnum);
                                _score += scorePerType.Value;
                                _world.NewEntity().Get<EnemyKilledRequest>().Score = _score;
                                break;
                        }
                    }

                    break;

            }
        }
    }

}