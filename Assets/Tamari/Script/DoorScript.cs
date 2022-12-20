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
    /// �����J��
    /// </summary>
    public void OpenDoor()
    {
        _doorAnim.SetBool("DoorAnim", true);
        //_doorAnim.SetFloat("Speed", 1);
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void CloseDoor()
    {
        _doorAnim.SetBool("DoorAnim", false);
        //_doorAnim.SetFloat("Speed", -3);
    }
}


