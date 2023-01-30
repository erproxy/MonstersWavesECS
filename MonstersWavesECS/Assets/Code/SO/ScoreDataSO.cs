using System;
using System.Collections.Generic;
using System.Linq;
using Code.Models;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "ScoreDataSO", menuName = "MyAssets/ScoreDataSO", order = 3)]
    public class ScoreDataSO : ScriptableObject
    {
        [SerializeField] private List<ScorePerType> _scorePerTypes;
        
        public ScorePerType CallbackScore(PoolObjectEnum poolObjectEnum) => _scorePerTypes.FirstOrDefault(scorePerTypes => 
            scorePerTypes.PoolObjectEnum == poolObjectEnum);
    }

    [Serializable]
    public class ScorePerType
    {
        [field: SerializeField] public PoolObjectEnum PoolObjectEnum { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}