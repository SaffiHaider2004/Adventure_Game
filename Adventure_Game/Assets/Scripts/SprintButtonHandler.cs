using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SprintButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerStats playerStats;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerStats.StartSprinting();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerStats.StopSprinting();
    }
}
