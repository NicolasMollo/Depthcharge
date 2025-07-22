using System;
using UnityEngine;
using Depthcharge.Actors.Modules;

[Serializable]
public struct MovementContext
{

    [SerializeField] private BaseMovementAdapter _target;
    public BaseMovementAdapter Target { get => _target; }

    [SerializeField] private Vector2 _direction;
    public Vector2 Direction { get => _direction; set => _direction = value; }

    [SerializeField] private float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    private Vector2 _startPosition;
    public Vector2 StartPosition { get => _startPosition; set => _startPosition = value; }

}