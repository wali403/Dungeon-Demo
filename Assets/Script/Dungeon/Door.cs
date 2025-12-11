using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Door : MonoBehaviour
{
    [HideInInspector] public bool isBoosRoomDoor = false;
    private BoxCollider2D doorCollider;
    private bool isOpen = false;

    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorCollider.enabled = false;
        }
    }

    public void LockDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            doorCollider.enabled = true;
        }
    }
}
