using UnityEngine;

namespace Code.MonoBehaviours.PoolPreset
{
    public class DefaultUnitPoolPreset : DefaultPoolPreset
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    }
}