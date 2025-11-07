using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_HardBossAndEndOfMap")]
    public class CS_HardBossAnEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(GameObject owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                EC_HardBoss boss = owner.GetComponentInParent<EC_HardBoss>();
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