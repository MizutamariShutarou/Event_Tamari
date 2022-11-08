using System;
using UnityEngine;

[System.Serializable]
public abstract class AnimationControllerBase<T> where T : Enum
{
    protected StateControllerBase<T> _state = default;

    public void Init(StateControllerBase<T> _stateController)
    {
        _state = _stateController;
    }
    public abstract void Update();
}