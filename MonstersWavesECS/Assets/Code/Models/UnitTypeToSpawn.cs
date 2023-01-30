using System;
using UnityEngine;

namespace Code.Models
{
    [Serializable]
    public struct UnitTypeToSpawn
    {
        [field: SerializeField] public PoolObjectEnum PoolObjectEnum { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}