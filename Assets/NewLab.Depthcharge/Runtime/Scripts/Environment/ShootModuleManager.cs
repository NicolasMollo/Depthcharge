using Depthcharge.Actors.Modules;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Environment
{

    [DisallowMultipleComponent]
    public class ShootModuleManager : MonoBehaviour
    {

        public enum ShootType : byte { FromAll, FromAllExceptSides, FromSides, FromMiddle, Random }

        [SerializeField]
        private List<ShootModule> shootModules = null;

        public Action OnShoot = null;

        private void Awake()
        {
            string message = $"=== ShootModuleManager.Awake() === Be ensure to fill \"shootModules\" list";
            Assert.IsTrue(shootModules.Count > 0, message);
        }

        public void ShootFromAll()
        {
            foreach (ShootModule shootModule in shootModules)
            {
                shootModule.Shoot();
            }
            OnShoot?.Invoke();
        }

        public void ShootFromAllExceptSides()
        {
            int lastIndex = shootModules.Count - 1;
            for (int i = 1; i < lastIndex; i++)
            {
                shootModules[i].Shoot();
            }
            OnShoot?.Invoke();
        }

        public void ShootFromSides()
        {
            int lastIndex = this.shootModules.Count - 1;
            ShootModule[] shootModules = new ShootModule[] { 
                this.shootModules[0], 
                this.shootModules[lastIndex] 
            };
            for (int i = 0; i < shootModules.Length; i++)
            {
                shootModules[i].Shoot();
            }
            OnShoot?.Invoke();
        }

        public void ShootFromMiddle()
        {
            int middleIndex = Mathf.FloorToInt(shootModules.Count / 2);
            ShootModule shootModule = this.shootModules[middleIndex];
            shootModule.Shoot();
            OnShoot?.Invoke();
        }
        public void ShootRandom()
        {
            int randomValue = 0;
            foreach (ShootModule shootModule in shootModules)
            {
                randomValue = UnityEngine.Random.Range(0, 2);
                if (randomValue == 0)
                {
                    shootModule.Shoot();
                }
            }
            OnShoot?.Invoke();
        }

        public void DisableShootModules()
        {
            foreach (ShootModule module in shootModules)
            {
                module.DisableModule();
            }
        }
        public void EnableShootModules()
        {
            foreach (ShootModule module in shootModules)
            {
                module.EnableModule();
            }
        }

        public void ReloadShootModules()
        {
            foreach (ShootModule module in shootModules)
            {
                module.Reload();
            }
        }

    }

}