using Codice.CM.Client.Differences;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/Enemy/Factories/EF_StdEnemy")]
    public class EF_StdEnemy : BaseEnemyFactory
    {

        // Inherited members:
        // protected EnemyConfiguration configuration = null;

        [SerializeField]
        private GameObject prefabStdEnemy = null;

        public override List<EnemyController> CreateEnemyPool(EnemyFactoryContext context, int poolSize)
        {

            List<EnemyController> enemies = new List<EnemyController>();
            EnemyController temporary = null;
            for (int i = 0; i < poolSize; i++)
            {
                temporary = CreateEnemy(context);
                temporary.gameObject.SetActive(false);
                enemies.Add(temporary);
            }

            return enemies;

        }

        protected override EnemyController CreateEnemy(EnemyFactoryContext context)
        {

            GameObject enemyObj = Instantiate(
                prefabStdEnemy,
                context.SpawnPoint.position,
                Quaternion.identity,
                context.Parent
                );
            EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
            Vector2 movementDirection = MovementDirectionToVector(context.MovementDirection);
            enemyController.SetUpEnemy(configuration, movementDirection);

            return enemyController;

        }

        private Vector2 MovementDirectionToVector(MovementDirection movementDirection)
        {

            Vector2 direction = Vector2.zero;
            switch (movementDirection)
            {
                case MovementDirection.Left:
                    direction = Vector2.left;
                    break;
                case MovementDirection.Right:
                    direction = Vector2.right;
                    break;
                case MovementDirection.Up:
                    direction = Vector2.up;
                    break;
                case MovementDirection.Down:
                    direction = Vector2.down;
                    break;
                case MovementDirection.Override:
                    direction = Vector2.zero;
                    break;
            }

            return direction;

        }


    }

}