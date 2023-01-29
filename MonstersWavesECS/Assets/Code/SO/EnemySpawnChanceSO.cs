using System;
using System.Collections.Generic;
using Code.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "EnemySpawnChanceSO", menuName = "MyAssets/EnemySpawnChanceSO", order = 2)]
    public class EnemySpawnChanceSO : ScriptableObject
    {
        [SerializeField] private List<EnemyTypeSpawnChance> _enemyTypeChance;

        public PoolObjectEnum CallBackFromChance() => 
            _enemyTypeChance.Find(itemChance => itemChance.Value >= Random.Range(0, 100)).PoolObjectEnum;
    }
    
    [Serializable]
    public struct EnemyTypeSpawnChance
    {
        [field: SerializeField] public PoolObjectEnum PoolObjectEnum{ get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}