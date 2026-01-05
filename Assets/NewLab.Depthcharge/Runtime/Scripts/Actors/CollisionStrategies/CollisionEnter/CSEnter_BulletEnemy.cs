using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndEnemy")]
    public class CSEnter_BulletEnemy : BaseCollisionEnterStrategy
    {

        public override void CollisionEnter(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                BulletController bullet = owner as BulletController;
                if (bullet == null) return;
                BaseEnemyController enemyController = other.GetComponentInParent<BaseEnemyController>();
                enemyController.OnCollideWithBullet(bullet.Damage);
                bullet.HealthModule.TakeMaxDamage();
            }
        }

    }

}