using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{
    [Serializable]
    internal class HardBossPhaseController
    {

        private EC_HardBoss _boss = null;
        private MovementModule _movementModule = null;
        private CannonShootController _shootController = null;

        [SerializeField]
        private float _halfHealthRotDivider = 0f;
        [SerializeField]
        private float _quarterHealthRotDivider = 0f;
        [SerializeField]
        private float _halfHealthSpeedMulti = 0f;
        [SerializeField]
        private float _quarterHealthSpeedMulti = 0f;


        internal void SetUp(EC_HardBoss boss, CannonShootController shootController)
        {
            _boss = boss;
            _movementModule = _boss.MovementModule;
            _shootController = shootController;
        }

        internal void OnHalfHealth(float startRotationOffset, float startSpeed)
        {
            _shootController.SetCurrentCannonShootStrategy(ShootModuleManager.ShootType.FromSides);
            float halfHealthRotationOffset = startRotationOffset / _halfHealthRotDivider;
            _boss.RotationOffset = halfHealthRotationOffset;
            float halfHealthSpeed = startSpeed * _halfHealthSpeedMulti;
            _movementModule.SetMovementSpeed(halfHealthSpeed);
        }
        internal void OnAQuarterHealth(float startRotationOffset, float startSpeed)
        {
            _shootController.SetCurrentCannonShootStrategy(ShootModuleManager.ShootType.FromAll);
            float quarterHealthRotationOffset = startRotationOffset / _quarterHealthRotDivider;
            _boss.RotationOffset = quarterHealthRotationOffset;
            float quarterHealthSpeed = startSpeed * _quarterHealthSpeedMulti;
            _movementModule.SetMovementSpeed(quarterHealthSpeed);
        }

    }

}