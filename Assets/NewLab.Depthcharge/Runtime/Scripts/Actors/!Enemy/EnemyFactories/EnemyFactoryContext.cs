using System;
using UnityEngine;

namespace Depthcharge.Actors
{

    [Serializable]
    public struct EnemyFactoryContext
    {

        [SerializeField]
        private Transform _spawnPoint;
        public Transform SpawnPoint { get => _spawnPoint; }

        [SerializeField]
        private Transform _parent;
        public Transform Parent { get => _parent; }

        [SerializeField]
        private MovementDirection _movementDirection;
        public MovementDirection MovementDirection { get => _movementDirection; set => _movementDirection = value; }

    }

}