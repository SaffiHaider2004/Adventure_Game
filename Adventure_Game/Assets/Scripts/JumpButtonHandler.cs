using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpButtonHandler : MonoBehaviour
{
    public PlayerJoystickMovement player;

    public void OnJumpPressed()
    {
        if (player != null)
        {
            player.JumpRequest();
        }
    }
}
