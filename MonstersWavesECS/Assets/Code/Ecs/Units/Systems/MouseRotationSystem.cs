using Code.Ecs.Tags;
using Code.Ecs.Units.Components;
using Code.MonoBehaviours.World;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Units.Systems
{
    public class MouseRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MouseRotationTag, ModelComponent>.Exclude<IsDestroyedTag> _rotationFilter = null;
        private readonly SceneEnvironment _sceneEnvironment = null;

        public void Run()
        {
            foreach (var i in _rotationFilter)
            {
                ref var model = ref _rotationFilter.Get2(i);
                ref var bodyTransform = ref model.bodyTransform;
                Plane modelPlane = new Plane(Vector3.up, bodyTransform.position);
                Ray ray = _sceneEnvironment.CameraMain.ScreenPointToRay(Input.mousePosition);
                if (!modelPlane.Raycast(ray, out var hitDistance)) continue;

                bodyTransform.forward = ray.GetPoint(hitDistance) - bodyTransform.position;
            }
        }
    }
}