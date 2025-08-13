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
        private bool _isReloading = false;
        public bool IsReloading { get => _isReloading; }

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
        private int poolSize = 0;
        public int PoolSize { get => poolSize; }

        [SerializeField]
        private bool reloadAutomatically = false;

        [SerializeField]
        private float reloadTime = 5.0f;
        public float ReloadTime { get => reloadTime; }

        #endregion

        public Action OnShoot = null;
        public Action<bool> OnStartReload = null;
        public Action OnReloaded = null;
        public Action OnChangeBullets = null;


        #region API

        public override void SetUpModule(GameObject owner = null)
        {
            originalBulletFactory = bulletFactory;
            bullets = bulletFactory.CreateBulletPool(this, poolSize);
            deadBullets = new List<BulletController>();
            base.SetUpModule(owner); // IsModuleSetUp = true;
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

            OnStartReload?.Invoke(_isReloading);
            _isReloading = true;
            ResetBulletsTransform();
            bulletsShooted = 0;
            OnReloaded?.Invoke();
            _isReloading = !_isReloading;

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
            bullets = this.bulletFactory.CreateBulletPool(this, poolSize);
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
            if (!_isReloading)
                StartCoroutine(ReloadAutomatically(reloadTime));
        }
        private IEnumerator ReloadAutomatically(float delay)
        {

            _isReloading = true;
            yield return new WaitUntil(AreBulletsDisabled);
            OnStartReload?.Invoke(_isReloading);
            yield return new WaitForSeconds(delay);
            ResetBulletsTransform();
            bulletsShooted = 0;
            OnReloaded?.Invoke();
            _isReloading = !_isReloading;

        }

        private void ResetBulletsTransform()
        {

            foreach (BulletController bullet in bullets)
            {
                if (!bullet.IsShooted)
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

        public void ResetBullets()
        {
            foreach (BulletController bullet in bullets)
            {
                if (bullet.transform.parent == null && !bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(bulletsParent);
                    bullet.transform.position = shootPoint.position;
                }
            }
        }

        #endregion

    }

}