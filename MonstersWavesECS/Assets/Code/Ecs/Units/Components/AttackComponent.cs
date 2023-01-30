using UnityEngine;

namespace Code.Ecs.Units.Components
{
    internal struct AttackComponent
    {
        public Transform PositionAttack;
        public float AttackDistance;
        public LayerMask WhatIsMask;
        public float Damage;
    }
}