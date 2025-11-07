using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseShootStrategy : ScriptableObject
    {
        public abstract void Shoot(ShootModule shootModule);
    }

}