using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndPlayer")]
    public class CSEnter_BulletPlayer : BaseCollisionEnterStrategy
    {

        public override void CollisionEnter(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                BulletController bullet = owner as BulletController;
                if (bullet == null) return;
                if (!bullet.CollisionModule.IsEnable) return;
                bullet.CollisionModule.DisableModule();
                bullet.HealthModule.TakeMaxDamage();
                PlayerController playerController = other.GetComponentInParent<PlayerController>();
                playerController.HealthModule.TakeDamage(bullet.Damage);
            }
        }

    }

}