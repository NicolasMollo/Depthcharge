using Depthcharge.Actors.Modules;
using UnityEngine;

namespace Depthcharge.Actors
{

    internal class NormalBossHelper
    {

        private EC_NormalBoss _boss = null;
        private SpriteRenderer _sr = null;
        private MovementModule _movementModule = null;
        private Vector2 _seaPosition = default;
        private float _seaWidth = 0f;


        internal NormalBossHelper(EC_NormalBoss boss)
        {
            _boss = boss;
            _sr = _boss.SpriteRenderer;
            _seaPosition = _boss.SeaPosition;
            _seaWidth = _boss.SeaWidth;
            _movementModule = boss.MovementModule;
        }

        internal void InvertBossDirection()
        {
            _sr.flipX = !_sr.flipX;
            _boss.MovementDirection *= -1.0f;
        }

        internal Vector2 CalculateStallPosition()
        {
            const float SPRITE_OFFSET_X = 1.0f;
            float halfWidth = (_sr.sprite.bounds.size.x * 0.5f) - SPRITE_OFFSET_X;
            float halfSeaWidth = _seaWidth * 0.5f;
            float calculatedMinX = _seaPosition.x - halfSeaWidth + halfWidth;
            float calculatedMaxX = _seaPosition.x + halfSeaWidth - halfWidth;
            float positionX = Random.Range(calculatedMinX, calculatedMaxX);

            Vector2 stallPosition = new Vector2(positionX, _movementModule.Target.GetPosition().y);
            return stallPosition;
        }

    }

}