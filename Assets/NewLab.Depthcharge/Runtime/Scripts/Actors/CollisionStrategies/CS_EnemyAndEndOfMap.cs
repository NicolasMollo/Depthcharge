using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_EnemyAndEndOfMap")]
    public class CS_EnemyAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                StdEnemyController enemy = owner.GetComponentInParent<StdEnemyController>();
                enemy.Deactivation();
                // enemy.HealthModule.TakeMaxDamage();
            }
        }

    }

}