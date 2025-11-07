using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{
    public class BaseBossController : BaseEnemyController
    {

        [SerializeField]
        protected SpriteRenderer spriteRenderer = null;

        protected void InvertBossDirection()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            movementDirection *= -1.0f;
        }

        public virtual void OnCollisionWithEndOfMap() { }

    }
}