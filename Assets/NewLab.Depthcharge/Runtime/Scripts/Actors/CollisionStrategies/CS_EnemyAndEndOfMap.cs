using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_EnemyAndEndOfMap")]
    public class CS_EnemyAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(Actor owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                BaseEnemyController enemy = owner as BaseEnemyController;
                enemy.OnCollisionWithEndOfMap();
            }
        }

    }

}