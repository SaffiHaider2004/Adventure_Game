using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

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
