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
        private BaseBulletFactory originalBulletFactory = null;
        private int bulletsShooted = 0;
        private bool canShoot = true;
        private bool _isReloading = false;
        public bool IsReloading { get => _isReloading; }
        public bool IsFullAmmo { get => bulletsShooted == 0; }

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

        private void Awake()
        {
            originalBulletFactory = bulletFactory;
            bullets = bulletFactory.CreateBulletPool(this, poolSize);
            canShoot = true;
        }

        #region API

        public void Shoot()
        {
            if (!canShoot)
            {
                return;
            }

            foreach (BulletController bullet in bullets)
            {
                if (bullet.transform.parent == bulletsParent && !bullet.gameObject.activeSelf)
                {
                    bullet.transform.rotation = shootPoint.rotation;
                    bullet.transform.SetParent(null);
                    bullet.gameObject.SetActive(true);
                    bulletsShooted++;
                    OnShoot?.Invoke();
                    if (bulletsShooted != bullets.Count)
                    {
                        return;
                    }
                }
            }
            if (reloadAutomatically && !_isReloading)
            {
                StartCoroutine(Reload(reloadTime));
            }
        }

        public void Reload()
        {
            _isReloading = true;
            OnStartReload?.Invoke(_isReloading);
            ResetBullets();
            bulletsShooted = 0;
            OnReloaded?.Invoke();
            _isReloading = !_isReloading;
        }

        public void IncreaseShootPointRotation(float rotationOffsetZ)
        {
            shootPoint.rotation *= Quaternion.Euler(shootPoint.forward * rotationOffsetZ);
            float eulerRotationZ = shootPoint.rotation.eulerAngles.z;
            const float STRAIGHT_ANGLE = 180.0f;
            const float FULL_ANGLE = 360.0f;
            if (eulerRotationZ > STRAIGHT_ANGLE)
            {
                eulerRotationZ -= FULL_ANGLE;
            }
        }

        public override void EnableModule()
        {
            canShoot = true;
        }
        public override void DisableModule()
        {
            canShoot = false;
        }

        #endregion
        #region Private

        private IEnumerator Reload(float delay)
        {
            _isReloading = true;
            yield return new WaitUntil(AreBulletsDisabled);
            OnStartReload?.Invoke(_isReloading);
            yield return new WaitForSeconds(delay);
            ResetBullets();
            bulletsShooted = 0;
            OnReloaded?.Invoke();
            _isReloading = !_isReloading;
        }

        private void ResetBullets()
        {
            foreach (BulletController bullet in bullets)
            {
                if (bullet.transform.parent == null && !bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(bulletsParent);
                    bullet.transform.position = shootPoint.position;
                    bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
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
            {
                return true;
            }
            return false;
        }

        #endregion

    }

}