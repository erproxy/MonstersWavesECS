using Code.Models;
using UnityEngine;

namespace Code.Ecs.Requests
{
    internal struct GOToSpawnRequest
    {
        public PoolObjectEnum PoolObjectEnum;
        public Vector3 Position;
        public Quaternion Quaternion;
    }
}