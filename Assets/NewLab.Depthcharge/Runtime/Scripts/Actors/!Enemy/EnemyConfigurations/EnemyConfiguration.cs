using UnityEngine;

namespace Depthcharge.Actors
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/Enemy/EnemyConfiguration")]
    public class EnemyConfiguration : ScriptableObject
    {

        [Header("SCORE POINTS CONFIGURATION")]
        [SerializeField]
        [Range(20.0f, 100.0f)]
        private float _scorePoints = 20.0f;
        public float ScorePoints { get => _scorePoints; set => _scorePoints = value; }

        [Header("MOVEMENT CONFIGURATION")]
        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float _movementSpeed = 1.0f;
        public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }

        [Header("HEALTH CONFIGURATION")]
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float _maxHealth = 1.0f;
        public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

        [Header("SHOOT CONFIGURATION")]
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float _shootDelay = 0.0f;
        public float ShootDelay { get => _shootDelay; set => _shootDelay = value; }
        [SerializeField]
        private float _travelledDistanceToShoot = 0.0f;
        public float TravelledDistanceToShoot { get => _travelledDistanceToShoot; }
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float _minRandomDistance = 1.0f;
        public float MinRandomDistance { get => _minRandomDistance; set => _minRandomDistance = value; }
        [SerializeField]
        [Range(1.0f, 100.0f)]
        private float _maxRandomDistance = 1.0f;
        public float MaxRandomDistance { get => _maxRandomDistance; set => _maxRandomDistance = value; }


    }

}