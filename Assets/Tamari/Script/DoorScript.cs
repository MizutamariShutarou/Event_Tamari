using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// <summary>
    /// �����J��
    /// </summary>
    public void OpenDoor()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void CloseDoor()
    {
        gameObject.SetActive(true);
    }
}
