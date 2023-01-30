using System;
using Code.MonoBehaviours.Ecs;

namespace Code.Ecs.Requests
{
    [Serializable]
    public struct InitializeEntityRequest
    {
        public EntityReference entityReference;
    }
}