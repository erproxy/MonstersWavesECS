using System.Linq;
using Code.Ecs.Requests;
using Code.MonoBehaviours.Ecs;
using Code.Tools;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.MonoBehaviours.Units
{
    public class EnemyColliderHit : MonoBehaviour
    {
        [SerializeField] private EntityReference _entityReference;

        private bool _playerCollision = false;
        
        
        protected void OnCollisionEnter(Collision collision)
        {
            if (!_playerCollision)
            {
                foreach (var contact in collision.contacts)
                {
                    if (contact.otherCollider.CompareTag(StringStatic.Player))
                    {
                        var playerEntityReference = contact.otherCollider.GetComponent<EntityReference>();
                        ref var entityPlayer = ref playerEntityReference.Entity;
                        ref var entity = ref _entityReference.Entity;
                        ref var enemyAttackingMeleeRequest = ref entity.Get<OpponentAttackingMeleeRequest>();
                        enemyAttackingMeleeRequest.EcsEntity = entityPlayer;
                        
                        _playerCollision = true;
                        return;
                    }
                }
            }
        }
        
        protected void OnCollisionExit(Collision collision)
        {
            if (_playerCollision)
            {
                if (!collision.contacts.Any(contant => contant.otherCollider.CompareTag(StringStatic.Player)))
                {
                    var entity = _entityReference.Entity;
                    entity.Del<OpponentAttackingMeleeRequest>();
                    _playerCollision = false;
                }
            }
        }
    }
}