using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndBullet")]
    public class CS_BulletAndBullet : BaseCollisionStrategy
    {

        [SerializeField]
        private string bulletTag = string.Empty;

        public override void OnCollision(Actor owner, Collider2D other)
        {
            if (other.CompareTag(bulletTag))
            {
                BulletController ownerBullet = owner as BulletController;
                if (ownerBullet == null) return;
                BulletController otherBullet = other.GetComponentInParent<BulletController>();
                ownerBullet.HealthModule.TakeDamage(otherBullet.Damage);
                otherBullet.HealthModule.TakeDamage(ownerBullet.Damage);
            }
        }

    }

}