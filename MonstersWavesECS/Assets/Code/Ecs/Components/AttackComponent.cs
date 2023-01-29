using System;
using UnityEngine;

namespace Code.Ecs.Components
{
    [Serializable]
    public struct AttackComponent
    {
        public Transform positionAttack;
        public float attackDistance;
        public LayerMask whatIsMask;
        public float damage;
    }
}