using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndBullet")]
    public class CS_BulletAndBullet : BaseCollisionStrategy
    {

        [SerializeField]
        private string bulletTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {
            if (other.CompareTag(bulletTag))
            {
                BulletController ownerBullet = owner.GetComponentInParent<BulletController>();
                BulletController otherBullet = other.GetComponentInParent<BulletController>();
                ownerBullet.HealthModule.TakeDamage(otherBullet.Damage);
                otherBullet.HealthModule.TakeDamage(ownerBullet.Damage);
            }
        }

    }

}