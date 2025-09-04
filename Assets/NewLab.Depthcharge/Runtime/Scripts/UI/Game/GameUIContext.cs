using Depthcharge.Actors;
using Depthcharge.LevelManagement;
using System;

namespace Depthcharge.UI
{

    [Serializable]
    public struct GameUIContext
    {
        public PlayerController player;
        public BaseLevelController levelController;
        public int levelNumber;
    }

}