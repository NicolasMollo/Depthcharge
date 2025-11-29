using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/CS_BulletAndEndOfMap")]
    public class CS_BulletAndEndOfMap : BaseCollisionStrategy
    {

        [SerializeField]
        private string endOfMapTag = string.Empty;

        public override void OnCollision(Actor owner, Collider2D other)
        {
            if (other.CompareTag(endOfMapTag))
            {
                BulletController bullet = owner as BulletController;
                if (bullet == null) return;
                bullet.OnCollisionWithEndOfMap(endOfMapTag);
            }
        }

    }

}