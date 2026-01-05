using Depthcharge.Actors.Modules;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [Serializable]
    internal class NormalBossPhaseController
    {

        private EC_NormalBoss _boss = null;
        private MovementModule _movementModule = null;

        [SerializeField]
        private float _halfHealthSpeedMulti = 1.5f;
        [SerializeField]
        private float _quarterHealthSpeedMulti = 2.0f;
        [SerializeField]
        private int _halfHealthBulletToShootDiv = 2;
        [SerializeField]
        private int _quarterHealthBulletToShootDiv = 1;


        internal NormalBossPhaseController(EC_NormalBoss boss)
        {
            _boss = boss;
            _movementModule = _boss.MovementModule;
        }

        internal void OnHalfHealth(float startSpeed)
        {
            float speed = startSpeed * _halfHealthSpeedMulti;
            _movementModule.SetMovementSpeed(speed);
            _boss.BulletToShootDivider = _halfHealthBulletToShootDiv;
        }
        internal void OnAQuarterHealth(float startSpeed)
        {
            float speed = startSpeed * _quarterHealthSpeedMulti;
            _movementModule.SetMovementSpeed(speed);
            _boss.BulletToShootDivider = _quarterHealthBulletToShootDiv;
        }

    }

}