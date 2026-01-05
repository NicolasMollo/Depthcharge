using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [DisallowMultipleComponent]
    public class ShootModule : BaseModule
    {

        #region Settings

        [Header("SETTINGS")]
        [SerializeField]
        private Transform _shootPoint = null;
        [SerializeField]
        private Transform _bulletsParent = null;
        [SerializeField]
        private BaseBulletFactory _bulletFactory = null;
        [SerializeField]
        private int _ammo = 0;
        [SerializeField]
        [Range(1, 3)]
        [Tooltip("Number that will be multiplied to \"ammo\" field and will be define the start count of bullets pool")]
        private int _poolSizeMultiplier = 2;
        [SerializeField]
        private bool _reloadAutomatically = true;
        [SerializeField]
        private float _reloadTime = 5.0f;

        #endregion

        private List<BulletController> _bullets = null;
        private int _currentAmmo = 0;
        private bool _canShoot = true;
        private bool _isReloading = false;

        public Transform ShootPoint { get => _shootPoint; }
        public Transform BulletsParent { get => _bulletsParent; }
        public int Ammo { get => _ammo; }
        public bool IsFullAmmo { get => _currentAmmo == _ammo; }
        public float ReloadTime { get => _reloadTime; }
        public bool IsReloading { get => _isReloading; }

        public Action OnShoot = null;
        public Action<bool> OnStartReload = null;
        public Action OnReloaded = null;


        private void Awake()
        {
            _currentAmmo = _ammo;
            int poolSize = _ammo * _poolSizeMultiplier;
            _bullets = _bulletFactory.CreateBulletPool(this, poolSize);
            _canShoot = true;
            _isReloading = false;
        }

        #region API

        public void Shoot()
        {
            if (!_canShoot)
            {
                return;
            }

            foreach (BulletController bullet in _bullets)
            {
                if (bullet.transform.parent == _bulletsParent && !bullet.gameObject.activeSelf)
                {
                    bullet.transform.rotation = _shootPoint.rotation;
                    bullet.transform.SetParent(null);
                    bullet.gameObject.SetActive(true);
                    _currentAmmo--;
                    OnShoot?.Invoke();
                    if (_currentAmmo == 0)
                    {
                        _canShoot = false;
                        if (_reloadAutomatically)
                        {
                            Reload(_reloadTime);
                        }
                    }
                    return;
                }
            }
        }

        public void Reload(float delay = 0.0f)
        {
            if (_isReloading)
            {
                return;
            }
            StartCoroutine(ReloadCoroutine(delay));
        }

        public void IncreaseShootPointRotation(float rotationOffsetZ)
        {
            _shootPoint.rotation *= Quaternion.Euler(_shootPoint.forward * rotationOffsetZ);
            float eulerRotationZ = _shootPoint.rotation.eulerAngles.z;
            const float STRAIGHT_ANGLE = 180.0f;
            const float FULL_ANGLE = 360.0f;
            if (eulerRotationZ > STRAIGHT_ANGLE)
            {
                eulerRotationZ -= FULL_ANGLE;
            }
        }

        public override void EnableModule()
        {
            _canShoot = true;
        }
        public override void DisableModule()
        {
            _canShoot = false;
        }

        #endregion
        #region Private

        private IEnumerator ReloadCoroutine(float delay)
        {
            _isReloading = true;
            OnStartReload?.Invoke(_isReloading);
            yield return new WaitForSeconds(delay);
            ResetExistingBullets();
            int availableBullets = GetAvailableBullets();
            EnsureAmmoPoolSize(availableBullets);
            _currentAmmo = _ammo;
            OnReloaded?.Invoke();
            _isReloading = false;
            _canShoot = true;
        }

        private void ResetExistingBullets()
        {
            foreach (BulletController bullet in _bullets)
            {
                if (bullet.transform.parent == _bulletsParent)
                {
                    continue;
                }
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(_bulletsParent);
                    bullet.transform.position = _shootPoint.position;
                    bullet.transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }

        private void EnsureAmmoPoolSize(int availableBullets)
        {
            int missingAmmo = _ammo - availableBullets;
            if (missingAmmo > 0)
            {
                 List<BulletController> additionalBullets = _bulletFactory.CreateBulletPool(this, missingAmmo);
                _bullets.AddRange(additionalBullets);
            }
        }
        private int GetAvailableBullets()
        {
            int availableBullets = 0;
            foreach (BulletController bullet in _bullets)
            {
                if (bullet.transform.parent == _bulletsParent)
                {
                    availableBullets++;
                }
            }
            return availableBullets;
        }


        #endregion

    }

}