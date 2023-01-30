using Code.Ecs.Components;
using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.Models;
using Code.MonoBehaviours.World;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class MouseRotationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<MouseRotationTag, ModelComponent>.Exclude<IsDestroyedTag> _rotationFilter = null;
        private readonly SceneEnvironment _sceneEnvironment = null;
        
        private EcsComponentRef<GameStateComponent> _gameStateRef;
        public void Init()
        {
            var gameStateEntity = _world.GetFilter(typeof(EcsFilter<GameStateComponent>)).GetEntity(0);
            _gameStateRef = gameStateEntity.Ref<GameStateComponent>();
        }
        
        public void Run()
        {
            if (_gameStateRef.Unref().GameStateEnum != GameStateEnum.Play)
            {
                return;
            }
            
            foreach (var i in _rotationFilter)
            {
                ref var model = ref _rotationFilter.Get2(i);
                ref var bodyTransform = ref model.BodyTransform;
                Plane modelPlane = new Plane(Vector3.up, bodyTransform.position);
                Ray ray = _sceneEnvironment.CameraMain.ScreenPointToRay(Input.mousePosition);
                if (!modelPlane.Raycast(ray, out var hitDistance)) continue;

                bodyTransform.forward = ray.GetPoint(hitDistance) - bodyTransform.position;
            }
        }
    }
}