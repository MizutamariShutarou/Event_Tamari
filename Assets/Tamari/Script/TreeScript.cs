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
    /// –Ø‚ª“|‚ê‚é
    /// </summary>
    public void BreakTree()
    {
        _treeAnim.SetBool("TreeAnim", true);
    }

}
