using System;
using Player;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private PlayerEntity _player;

    private float _direction;

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            _player.Jump();
    }

    private void FixedUpdate()
    {
        _player.MoveHorizontally(_direction);
    }
}