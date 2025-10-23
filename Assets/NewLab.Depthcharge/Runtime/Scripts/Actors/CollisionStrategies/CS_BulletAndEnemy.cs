using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndEnemy")]
    public class CS_BulletAndEnemy : BaseCollisionStrategy
    {

        [SerializeField]
        private string enemyTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {

            if (other.CompareTag(enemyTag))
            {
                BulletController bulletController = owner.GetComponentInParent<BulletController>();
                BaseEnemyController enemyController = other.GetComponentInParent<BaseEnemyController>();
                enemyController.HealthModule.TakeDamage(bulletController.Damage);
                bulletController.HealthModule.TakeMaxDamage();
                bulletController.CollisionModule.DisableModule();
            }

        }
    }

}