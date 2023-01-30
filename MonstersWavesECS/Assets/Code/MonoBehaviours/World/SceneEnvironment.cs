using Cinemachine;
using UnityEngine;

namespace Code.MonoBehaviours.World
{
    public class SceneEnvironment : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _dynamicParent;
        [SerializeField] private Transform _nonActiveParent;

        public Camera CameraMain => _camera;
        public Transform DynamicParent => _dynamicParent;
        public Transform NonActiveParent => _nonActiveParent;
    }
}