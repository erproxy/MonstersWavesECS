using System;
using UnityEngine;

namespace Code.Ecs.Units.Components
{
    [Serializable]
    public struct ModelComponent
    {
        public Transform modelTransform;
        public Transform bodyTransform;
    }
}