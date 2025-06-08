using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchButtonHandler : MonoBehaviour
{
    public PlayerJoystickMovement player;

    public void OnPunchPressed()
    {
        if (player != null)
        {
            player.PunchRequest();
        }
    }
}
