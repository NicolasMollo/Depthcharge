using UnityEngine;

namespace Depthcharge.Actors
{
    internal abstract class BaseBulletBehaviour : MonoBehaviour
    {

        [SerializeField]
        protected BulletController owner = null;

        public virtual void OnBulletStart() { }
        public virtual void OnBulletDestroy() { }
        public virtual void OnBulletEnable() { }
        public virtual void OnBulletDisable() { }
        public virtual void OnBulletSetUp() { }
        public virtual void OnBulletDeath(string executionerTag) { }

    }

}