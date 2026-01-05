using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [Serializable]
    public class EasyBossPhaseController
    {

        private EC_EasyBoss _boss = null;
        private MovementModule _movementModule = null;

        [SerializeField]
        private float _halfHealthSpeedMulti = 1.5f;
        [SerializeField]
        private float _quarterHealthSpeedMulti = 2.0f;
        [SerializeField]
        private float _halfHealthShootDelayMulti = 0.5f;
        [SerializeField]
        private float _quarterHealthShootDelayMulti = 0.25f;


        public EasyBossPhaseController(EC_EasyBoss boss)
        {
            _boss = boss;
            _movementModule = boss.MovementModule;
        }

        public void OnHalfHealth(float startSpeed, float startShootDelay)
        {
            _boss.RetreatSpeed = startSpeed * _halfHealthSpeedMulti;
            _movementModule.SetMovementSpeed(_boss.RetreatSpeed);
            _boss.ShootDelay = startShootDelay * _halfHealthShootDelayMulti;
        }

        public void OnAQuarterHealth(float startSpeed, float startShootDelay)
        {
            _boss.RetreatSpeed = startSpeed * _quarterHealthSpeedMulti;
            _movementModule.SetMovementSpeed(_boss.RetreatSpeed);
            _boss.ShootDelay = startShootDelay * _quarterHealthShootDelayMulti;
        }

    }

}