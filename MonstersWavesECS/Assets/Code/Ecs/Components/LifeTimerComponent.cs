using System;
using UnityEngine;

namespace Code.Ecs.Components
{
    [Serializable]
    public struct LifeTimerComponent
    {
        public float timerDefault;
        public float timer;
    }
}