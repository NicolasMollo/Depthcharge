using Depthcharge.Actors.Modules;
using UnityEngine;


namespace Depthcharge.Actors
{
    internal class HardBossHelper
    {

        private EC_HardBoss _boss = null;
        private MovementModule _movementModule = null;
        private SpriteRenderer _sr = null;

        internal HardBossHelper(EC_HardBoss boss)
        {
            _boss = boss;
            _movementModule = _boss.MovementModule;
            _sr = _boss.SpriteRenderer;
        }

        internal void InvertBossDirection()
        {
            _sr.flipX = !_sr.flipX;
            _boss.MovementDirection *= -1.0f;
        }
        internal void MoveToTargetY()
        {
            const float SEA_SIZEY_OFFSET = 1.0f;
            float halfSeaSizeY = _boss.SeaSize.y * 0.5f;
            float halfBossSizeY = _sr.bounds.size.y * 0.5f;
            float topSeaY = _boss.SeaPosition.y + (halfSeaSizeY - SEA_SIZEY_OFFSET) - halfBossSizeY;
            float bottomSeaY = _boss.SeaPosition.y - (halfSeaSizeY + SEA_SIZEY_OFFSET) + halfBossSizeY;
            float positionY = _movementModule.Target.GetPosition().y;
            positionY--;
            if (positionY <= bottomSeaY)
            {
                positionY = topSeaY;
            }
            Vector2 position = new Vector2(_movementModule.Target.GetPosition().x, positionY);
            _movementModule.Target.MoveTo(position);
        }

    }

}