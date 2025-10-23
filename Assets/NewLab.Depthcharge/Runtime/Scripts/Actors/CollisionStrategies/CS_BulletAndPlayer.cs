using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndPlayer")]
    public class CS_BulletAndPlayer : BaseCollisionStrategy
    {

        [SerializeField]
        private string playerTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {

            if (other.CompareTag(playerTag))
            {
                BulletController bulletController = owner.GetComponentInParent<BulletController>();
                PlayerController playerController = other.GetComponentInParent<PlayerController>();
                playerController.HealthModule.TakeDamage(bulletController.Damage);
                bulletController.HealthModule.TakeMaxDamage();
                bulletController.CollisionModule.DisableModule();
                // bulletController.Deactivation();
                Debug.Log($"Collision with {other.name}");
            }

        }

    }

}