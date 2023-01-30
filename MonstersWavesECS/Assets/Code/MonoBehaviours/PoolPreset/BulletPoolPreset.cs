using Code.Ecs.Components;
using UnityEngine;

namespace Code.MonoBehaviours.PoolPreset
{
    public class BulletPoolPreset : DefaultPoolPreset
    {
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public LayerMask WhatIsMask { get; private set; }
        [field: SerializeField] public Transform PositionAttack { get; private set; }
    }
}