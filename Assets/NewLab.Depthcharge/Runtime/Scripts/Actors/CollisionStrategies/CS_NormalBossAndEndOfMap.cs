using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_NormalBossAndEndOfMap")]
    public class CS_NormalBossAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                EC_NormalBoss boss = owner.GetComponentInParent<EC_NormalBoss>();
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