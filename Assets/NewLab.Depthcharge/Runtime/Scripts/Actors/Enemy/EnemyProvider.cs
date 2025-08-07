using System;
using System.Collections.Generic;
using UnityEngine;
using Codice.CM.Client.Differences;

namespace Depthcharge.Actors
{

    public class EnemyProvider : MonoBehaviour
    {

        private List<EnemyController> _enemies = null;
        public List<EnemyController> Enemies { get => _enemies; }

        #region Enemy factory

        [Header("ENEMY FACTORY")]

        [SerializeField]
        private BaseEnemyFactory enemyFactory = null;

        [SerializeField]
        private EnemyFactoryContext context = default;

        [SerializeField]
        [Range(1, 10)]
        private int poolSize = 0;

        #endregion

        public void SetUp(MovementDirection movementDirection)
        {
            context.MovementDirection = movementDirection;
            _enemies = enemyFactory.CreateEnemyPool(context, poolSize);
            foreach (EnemyController enemy in _enemies)
                enemy.OnDeactivation += ResetEnemyPosition;
            if (context.MovementDirection != MovementDirection.Left)
            {
                FlipEnemies();
            }
        }

        public void CleanUp()
        {
            foreach (EnemyController enemy in _enemies)
                enemy.OnDeactivation -= ResetEnemyPosition;
        }

        private void FlipEnemies()
        {
            SpriteRenderer spriteRenderer = null;
            foreach (EnemyController enemy in _enemies)
            {
                spriteRenderer = enemy.GetComponentInChildren<SpriteRenderer>();
                spriteRenderer.flipX = true;
            }
        }

        private void ResetEnemyPosition(EnemyController enemy)
        {
            enemy.transform.position = context.SpawnPoint.position;
            enemy.transform.SetParent(context.Parent);
        }

    }

}