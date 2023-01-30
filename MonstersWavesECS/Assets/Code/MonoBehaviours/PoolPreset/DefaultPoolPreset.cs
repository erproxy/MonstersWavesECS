using Code.MonoBehaviours.Ecs;
using UnityEngine;

namespace Code.MonoBehaviours.PoolPreset
{
    public class DefaultPoolPreset : MonoBehaviour
    {
        [field: SerializeField] public EntityReference EntityReference { get; private set; }
        [field: SerializeField] public Transform ModelTransform;
        [field: SerializeField] public Transform BodyTransform;
        
    }
}