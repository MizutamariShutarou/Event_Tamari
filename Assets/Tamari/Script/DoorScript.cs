using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator _doorAnim;
    private void Start()
    {
        _doorAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// ”à‚ªŠJ‚­
    /// </summary>
    public void OpenDoor()
    {
        _doorAnim.SetBool("DoorAnim", true);
        _doorAnim.SetFloat("Speed", 1);
    }

    /// <summary>
    /// ”à‚ª•Â‚¶‚é
    /// </summary>
    public void CloseDoor()
    {
        _doorAnim.SetBool("DoorAnim", false);
        _doorAnim.SetFloat("Speed", -1);
    }
}
