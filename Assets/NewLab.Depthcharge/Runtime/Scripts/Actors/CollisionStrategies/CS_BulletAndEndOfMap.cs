using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndEndOfMap")]
    public class CS_BulletAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {

            if (other.CompareTag(endOfMapTag))
            {
                BulletController bulletController = owner.GetComponentInParent<BulletController>();
                bulletController.HealthModule.TakeMaxDamage();
                bulletController.CollisionModule.DisableModule();
                // bulletController.Deactivation();
                Debug.Log($"Collision with {other.name}");
            }

        }

    }

}