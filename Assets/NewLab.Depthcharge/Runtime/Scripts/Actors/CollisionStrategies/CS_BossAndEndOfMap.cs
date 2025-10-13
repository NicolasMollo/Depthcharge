using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BossAndEndOfMap")]
    public class CS_BossAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                BossEnemyController boss = owner.GetComponentInParent<BossEnemyController>();
                if (boss == null)
                {
                    Debug.LogError($"=== {owner.name}.CS_BossAndEndOfMap.OnCollision() === Owner is not a boss!");
                    return;
                }
                boss.OnCollisionWithEndOfMap();
            }
        }

    }
}