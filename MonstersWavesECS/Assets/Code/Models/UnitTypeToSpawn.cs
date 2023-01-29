using System;
using Code.Test;
using UnityEngine;

namespace Code.Models
{
    [Serializable]
    public struct UnitTypeToSpawn
    {
        [field: SerializeField] public PoolObjectEnum PoolObjectEnum { get; private set; }
        [field: SerializeField] public EntityReference Prefab { get; private set; }
    }
}