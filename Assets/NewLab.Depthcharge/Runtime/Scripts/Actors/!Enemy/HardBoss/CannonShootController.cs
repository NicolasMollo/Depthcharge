using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    [Serializable]
    internal class CannonShootController
    {

        private SS_CannonShoot _currentCannonShootStrategy = null;
        [SerializeField]
        private List<SS_CannonShoot> _shootStrategies = null;
        private ShootModuleManager _cannons = null;

        internal SS_CannonShoot CurrentCannonShootStrategy
        {
            get => _currentCannonShootStrategy;
        }


        internal void SetUp(ShootModuleManager cannons)
        {
            _cannons = cannons;
        }

        internal void ReloadCannons()
        {
            _cannons.ReloadShootModules();
        }
        internal void EnableCannons()
        {
            _cannons.EnableShootModules();
        }
        internal void DisableCannons()
        {
            _cannons.DisableShootModules();
        }

        internal void SubscribeOnCannonsShoot(Action method)
        {
            _cannons.OnShoot += method;
        }
        internal void UnsubscribeFromCannonsShoot(Action method)
        {
            _cannons.OnShoot -= method;
        }

        internal void SetCurrentCannonShootStrategy(ShootModuleManager.ShootType shootType)
        {
            _currentCannonShootStrategy = GetCannonShootStrategy(shootType);
        }

        private SS_CannonShoot GetCannonShootStrategy(ShootModuleManager.ShootType shootType)
        {
            SS_CannonShoot cannonShootStrategy = null;
            foreach (SS_CannonShoot strategy in _shootStrategies)
            {
                if (strategy.ShootTypes == shootType)
                {
                    cannonShootStrategy = strategy;
                    break;
                }
            }
            return cannonShootStrategy;
        }

    }

}