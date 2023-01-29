using System;

namespace Code.EventHelper
{
    public static class EventBussStateMachine
    {
        public static event Action StartGame;

        public static void OnStartGame() => StartGame?.Invoke();
    }
}