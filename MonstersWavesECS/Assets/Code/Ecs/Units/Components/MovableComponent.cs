using System;
using UnityEngine;

namespace Code.Ecs.Units.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public Rigidbody rigidbody;
        public float speed;
    }
}