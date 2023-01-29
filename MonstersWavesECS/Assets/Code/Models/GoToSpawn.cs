using System;
using UnityEngine;

namespace Code.Models
{
    [Serializable]
    public struct GoToSpawn
    {
        public PoolObjectEnum poolObjectEnum;
        public Vector3 position;
        public Quaternion quaternion;
    }
}