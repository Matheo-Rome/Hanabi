using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onground : MonoBehaviour
{
    private PlayerMovementSolo _playerMovementSolo;

    private void Start()
    {
        _playerMovementSolo = gameObject.GetComponent<PlayerMovementSolo>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        _playerMovementSolo.onGround = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _playerMovementSolo.onGround = false;
    }
}
