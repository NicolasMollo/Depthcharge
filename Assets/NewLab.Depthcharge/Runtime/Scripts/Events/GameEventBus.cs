using System;

namespace Depthcharge.Events
{

    public static class GameEventBus
    {

        public static Action OnGameStart = null;
        public static void CallOnGameStart()
        {
            OnGameStart?.Invoke();
        }

        public static Action OnGameUpdate = null;
        public static void CallOnGameUpdate()
        {
            OnGameUpdate?.Invoke();
        }

        public static Action OnGameOver = null;
        public static void CallOnGameOver()
        {
            OnGameOver?.Invoke();
        }

    }

}