using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// <summary>
    /// ”à‚ªŠJ‚­
    /// </summary>
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ”à‚ª•Â‚¶‚é
    /// </summary>
    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }
}
