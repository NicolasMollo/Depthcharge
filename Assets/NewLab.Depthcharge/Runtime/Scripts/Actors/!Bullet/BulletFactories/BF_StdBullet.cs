using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/BulletFactories/BF_StdBullet")]
    public class BF_StdBullet : BaseBulletFactory
    {

        [SerializeField]
        private GameObject prefabBullet = null;

        public override List<BulletController> CreateBulletPool(ShootModule shootModule, int poolSize)
        {
            List<BulletController> bullets = new List<BulletController>();
            BulletController temporary = null;
            for (int i = 0; i < poolSize; i++)
            {
                temporary = CreateBullet(shootModule);
                temporary.gameObject.SetActive(false);
                bullets.Add(temporary);
            }

            return bullets;
        }

        protected override BulletController CreateBullet(ShootModule shootModule)
        {
            GameObject bulletObj = Instantiate(
                prefabBullet, 
                shootModule.ShootPoint.position, 
                Quaternion.identity, 
                shootModule.BulletsParent
                );
            BulletController bulletController = bulletObj.GetComponent<BulletController>();
            return bulletController;
        }

    }

}