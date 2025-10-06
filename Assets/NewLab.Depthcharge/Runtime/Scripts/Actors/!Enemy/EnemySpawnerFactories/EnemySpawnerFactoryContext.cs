using Codice.CM.Client.Differences;
using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [Serializable]
    public struct EnemySpawnerFactoryContext
    {

        [SerializeField]
        private MovementDirection _enemiesDirection;
        public MovementDirection EnemiesDirection { get => _enemiesDirection; set => _enemiesDirection = value; }

        [SerializeField]
        private Transform _parent;
        public Transform Parent { get => _parent; }

        [SerializeField]
        private Vector2 _targetPosition;
        public Vector2 TargetPosition { get => _targetPosition; set => _targetPosition = value; }

    }

}