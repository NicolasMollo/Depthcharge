using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndEnemy")]
    public class CS_BulletAndEnemy : BaseCollisionStrategy
    {

        [SerializeField]
        private string enemyTag = string.Empty;

        public override void OnCollision(Actor owner, Collider2D other)
        {
            if (other.CompareTag(enemyTag))
            {
                BulletController bullet = owner as BulletController;
                if (bullet == null) return;
                BaseEnemyController enemyController = other.GetComponentInParent<BaseEnemyController>();
                enemyController.HealthModule.TakeDamage(bullet.Damage);
                bullet.HealthModule.TakeMaxDamage();
            }
        }
    }

}