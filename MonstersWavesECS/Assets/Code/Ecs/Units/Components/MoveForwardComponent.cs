using System;
using UnityEngine;

namespace Code.Ecs.Units.Components
{
    [Serializable]
    public struct MoveForwardComponent
    {
        public Transform modelTransform;
        public float speed;
    }
}