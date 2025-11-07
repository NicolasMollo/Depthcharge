using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/ShootStrategies/SS_StdShoot")]
    public class SS_StdShoot : BaseShootStrategy
    {

        public override void Shoot(ShootModule shootModule)
        {
            shootModule.Shoot();
        }

    }

}