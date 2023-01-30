using System.Collections.Generic;
using System.Linq;
using Code.Models;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "SpawnReferenceSO", menuName = "MyAssets/SpawnReferenceSO", order = 1)]
    public class SpawnReferenceSO : ScriptableObject
    {
        [SerializeField] private List<UnitTypeToSpawn> _unitTypeToSpawns;

        public IReadOnlyList<UnitTypeToSpawn> UnitTypeToSpawns => _unitTypeToSpawns;
        
        public UnitTypeToSpawn CallbackUnityType(PoolObjectEnum poolObjectEnum) => _unitTypeToSpawns.FirstOrDefault(unitTypeToSpawn => 
            unitTypeToSpawn.PoolObjectEnum == poolObjectEnum);
    }
}