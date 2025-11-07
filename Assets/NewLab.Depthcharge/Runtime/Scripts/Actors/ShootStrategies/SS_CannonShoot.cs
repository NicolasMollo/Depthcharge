using Depthcharge.Environment;
using Depthcharge.LevelManagement;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/ShootStrategies/SS_CannonShoot")]
    public class SS_CannonShoot : BaseShootStrategy
    {

        [SerializeField]
        private ShootModuleManager.ShootType _shootType = ShootModuleManager.ShootType.FromMiddle;
        public ShootModuleManager.ShootType ShootTypes { get => _shootType; }

        public override void Shoot(ShootModule shootModule)
        {
            BossLevelController bossLevel = FindFirstObjectByType<BossLevelController>();
            string message = $"=== SS_CannonShoot === There isn't a \"BossLevelController\" in this scene!";
            Assert.IsNotNull(bossLevel, message);
            ShootFromSelectedCannons(bossLevel.Cannons, _shootType);
        }

        private void ShootFromSelectedCannons(ShootModuleManager shootModuleManager, ShootModuleManager.ShootType shootType)
        {
            switch (shootType)
            {
                case ShootModuleManager.ShootType.FromAll:
                    shootModuleManager.ShootFromAll();
                    break;
                case ShootModuleManager.ShootType.FromAllExceptSides:
                    shootModuleManager.ShootFromAllExceptSides();
                    break;
                case ShootModuleManager.ShootType.FromSides:
                    shootModuleManager.ShootFromSides();
                    break;
                case ShootModuleManager.ShootType.FromMiddle:
                    shootModuleManager.ShootFromMiddle();
                    break;
                case ShootModuleManager.ShootType.Random:
                    shootModuleManager.ShootRandom();
                    break;
            }
        }

    }

}