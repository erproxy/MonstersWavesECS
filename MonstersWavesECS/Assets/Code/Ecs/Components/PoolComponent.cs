using System;
using Code.Models;
using UnityEngine;

namespace Code.Ecs.Components
{
    [Serializable]
    public struct PoolComponent
    {
        public PoolObjectEnum poolObjectEnum;
        public Transform transform;
    }
}