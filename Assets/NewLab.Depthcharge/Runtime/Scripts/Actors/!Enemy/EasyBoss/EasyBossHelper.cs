using Depthcharge.Actors.Modules;
using UnityEngine;

namespace Depthcharge.Actors
{

    internal class EasyBossHelper
    {

        private EC_EasyBoss _boss = null;
        private MovementModule _movementModule = null;
        private SpriteRenderer _sr = null;


        internal EasyBossHelper(EC_EasyBoss boss)
        {
            _boss = boss;
            _sr = _boss.SpriteRenderer;
            _movementModule = _boss.MovementModule;
        }


        internal void InvertBossDirection()
        {
            _sr.flipX = !_sr.flipX;
            _boss.MovementDirection *= -1.0f;
        }

        internal void MoveToTargetY(float targetY)
        {
            Vector2 position = new Vector2(_movementModule.Target.GetPosition().x, targetY);
            _movementModule.Target.MoveTo(position);
        }

    }

}