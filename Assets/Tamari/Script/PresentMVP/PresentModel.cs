using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PresentModel : MonoBehaviour
{
    private int _maxNum;
    private int _currentNum;
    private bool _isAll;

    public int MaxNum
    {
        get { return _maxNum; }
        set { _maxNum = value; OnSetMaxNum?.Invoke(_maxNum); }
    }

    public int CurrentNum
    {
        get { return _currentNum; }
        set { _currentNum = value; OnSetCurrentNum?.Invoke(_currentNum); }
    }

    public bool IsAll
    {
        get { return _isAll; }
        set { _isAll = value; OnSetBool?.Invoke(_isAll); }
    }

    public event Action<int> OnSetMaxNum;
    public event Action<int> OnSetCurrentNum;
    public event Action<bool> OnSetBool;

    public PresentModel()
    {
        _currentNum = 0;
        _isAll = false;
    }

    public void CountNum()
    {
        CurrentNum++;
    }

    public void ResetNum()
    {
        CurrentNum = 0;
    }

    public void BoolChange()
    {
        _isAll = true;
        Debug.Log(_isAll);
    }
}
