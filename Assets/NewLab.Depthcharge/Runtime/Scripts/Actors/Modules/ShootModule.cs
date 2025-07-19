using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [DisallowMultipleComponent]
    public class ShootModule : BaseModule
    {

        private List<BulletController> bullets = null;
        private List<BulletController> deadBullets = null;
        private BaseBulletFactory originalBulletFactory = null;
        private int bulletsShooted = 0;
        private bool isReloading = false;

        #region References

        [SerializeField]
        private Transform shootPoint = null;
        public Transform ShootPoint { get => shootPoint; }

        [SerializeField]
        private Transform bulletsParent = null;
        public Transform BulletsParent { get => bulletsParent; }

        [SerializeField]
        private BaseBulletFactory bulletFactory = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private bool reloadAutomatically = false;

        [SerializeField]
        private float reloadTime = 5.0f;

        #endregion

        public Action OnShoot = null;
        public Action OnReload = null;
        public Action OnChangeBullets = null;


        #region API

        public override void SetUpModule(GameObject owner = null)
        {
            originalBulletFactory = bulletFactory;
            bullets = bulletFactory.CreateBulletPool(this);
            deadBullets = new List<BulletController>();
        }

        public void Shoot()
        {

            foreach (BulletController bullet in bullets)
            {
                if (bullet.transform.parent == bulletsParent && !bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(null);
                    bullet.gameObject.SetActive(true);
                    bulletsShooted++;
                    OnShoot?.Invoke();
                    if (bulletsShooted != bullets.Count)
                        return;
                }
            }

            if (reloadAutomatically)
                ReloadAutomatically();

        }

        public void Reload()
        {

            isReloading = true;
            ResetBulletsTransform();
            bulletsShooted = 0;
            OnReload?.Invoke();
            isReloading = !isReloading;

        }

        public void ChangeBulletsType(BaseBulletFactory bulletFactory)
        {

            this.bulletFactory = bulletFactory;
            foreach (BulletController bullet in bullets)
            {
                //if (bullet.IsShooted)
                //    deadBullets.Add(bullet);
                //else
                //    Destroy(bullet.gameObject);
                if (!bullet.IsShooted)
                    Destroy(bullet.gameObject);

            }
            bullets.Clear();
            bullets = this.bulletFactory.CreateBulletPool(this);
            OnChangeBullets?.Invoke();

        }

        private void ClearDeadBullets()
        {
            foreach (BulletController bullet in deadBullets)
            {
                if (!bullet.gameObject.activeSelf)
                    Destroy(bullet.gameObject);
            }
            deadBullets.Clear();
        }

        public void ResetBulletsType()
        {
            this.bulletFactory = originalBulletFactory;
            ChangeBulletsType(this.bulletFactory);
        }

        #endregion
        #region Private

        private void ReloadAutomatically()
        {
            if (!isReloading)
                StartCoroutine(ReloadAutomatically(reloadTime));
        }
        private IEnumerator ReloadAutomatically(float delay)
        {

            isReloading = true;
            yield return new WaitUntil(AreBulletsDisabled);
            yield return new WaitForSeconds(delay);
            ResetBulletsTransform();
            bulletsShooted = 0;
            OnReload?.Invoke();
            isReloading = !isReloading;

        }

        private void ResetBulletsTransform()
        {

            foreach (BulletController bullet in bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(bulletsParent);
                    bullet.transform.position = shootPoint.position;
                }
            }

        }

        private bool AreBulletsDisabled()
        {

            int deactivatedBullets = 0;
            foreach (BulletController bullet in bullets)
            {
                if (!bullet.gameObject.activeSelf)
                    deactivatedBullets++;
            }

            if (deactivatedBullets == bullets.Count)
                return true;
            return false;

        }

        #endregion

    }

}