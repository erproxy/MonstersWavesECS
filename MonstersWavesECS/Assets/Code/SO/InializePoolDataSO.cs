using System;
using System.Collections.Generic;
using System.Linq;
using Code.Models;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "InializePoolDataSO", menuName = "MyAssets/InializePoolDataSO", order = 3)]
    public class InializePoolDataSO : ScriptableObject
    {
        [field: SerializeField] public BulletInitializeDataPool BulletInitializeDataPool { get; private set; }
        [field: SerializeField] public PlayerInitializeDataPool PlayerInitializeDataPool { get; private set; }
        
        [SerializeField] private List<EnemyInitializeDataPool> _enemyInitializeDataPool;
        public IReadOnlyList<EnemyInitializeDataPool> EnemyInitializeDataPool => _enemyInitializeDataPool;

        public EnemyInitializeDataPool CallbackEnemyData(PoolObjectEnum poolObjectEnum) => _enemyInitializeDataPool.FirstOrDefault(enemyInitializeDataPool => 
            enemyInitializeDataPool.PoolObjectEnum == poolObjectEnum);
    }

    [Serializable]
    public class BulletInitializeDataPool
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTimer { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
    
    [Serializable]
    public class PlayerInitializeDataPool
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ReloadingTimer { get; private set; }
    }
    
    [Serializable]
    public class EnemyInitializeDataPool
    {
        [field: SerializeField] public PoolObjectEnum PoolObjectEnum { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ReloadingTimer { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Armor { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}