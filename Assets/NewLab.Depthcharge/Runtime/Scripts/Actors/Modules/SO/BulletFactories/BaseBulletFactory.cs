using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseBulletFactory : ScriptableObject
    {

        protected abstract BulletController CreateBullet(ShootModule shootModule);
        public abstract List<BulletController> CreateBulletPool(ShootModule shootModule, int poolSize);

    }

}