using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    Animator _treeAnim;
    private void Start()
    {
        _treeAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// �؂��|���
    /// </summary>
    public void BreakTree()
    {
        _treeAnim.SetBool("TreeAnim", true);
    }

}
