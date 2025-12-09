using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndBullet")]
    public class CSEnter_BulletBullet : BaseCollisionEnterStrategy
    {

        public override void CollisionEnter(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                BulletController ownerBullet = owner as BulletController;
                if (ownerBullet == null) return;
                BulletController otherBullet = other.GetComponentInParent<BulletController>();
                if (otherBullet == null) return;
                ownerBullet.HealthModule.TakeDamage(otherBullet.Damage);
                otherBullet.HealthModule.TakeDamage(ownerBullet.Damage);
            }
        }

    }

}