﻿using Code.Ecs.Components;
using Code.Ecs.Requests;
using Code.Ecs.Tags;
using Code.Test;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Ecs.Systems
{
    public class BulletsAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BulletTag, AttackComponent>.Exclude<IsDestroyedTag> _bulletAttackFilter;
        public void Run()
        {
            foreach (var i in _bulletAttackFilter)
            {
                ref var bulletAttack = ref _bulletAttackFilter.Get2(i);

                ref var positionAttack = ref bulletAttack.positionAttack;
                ref var attackDistance = ref bulletAttack.attackDistance;
                ref var whatIsMask = ref bulletAttack.whatIsMask;
                ref var damage = ref bulletAttack.damage;
                
                Collider[] objects =
                    Physics.OverlapSphere(positionAttack.position, attackDistance, whatIsMask);

                if (objects.Length != 0)
                {
                    var entityReference =  objects[0].GetComponent<EntityReference>();
                    ref var entity = ref entityReference.Entity;
                    entity.Get<ObjectAttackedRequest>().Damage = damage;
                    ref var bulletEntity = ref _bulletAttackFilter.GetEntity(i);
                    bulletEntity.Get<NeedToDestroyEvent>();
                }
            }
        }
    }
}