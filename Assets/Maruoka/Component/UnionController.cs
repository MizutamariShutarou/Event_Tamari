using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private MainLifeController _lifeController = default;
    [SerializeField]
    private MoveBehavior _mover = default;
    [SerializeField]
    private WireAction _wireActioner = default;
    [SerializeField]
    private FlyingSquirrelAction _flyingSquirrelActioner = default;
    [SerializeField]
    private ShootGunBehavior _shootGun = default;
    [SerializeField]
    private SeparationInstruction _separationInstructioner = default;
    [SerializeField]
    private UnionStateController _stateController = default;
    [SerializeField]
    private UnionAnimationController _animationController = default;
    #endregion

    #region Member Variables
    #endregion

    #region Unity Methods
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        Process();
    }
    #endregion

    #region Private Methods
    private void Init()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        _mover.Init(rb2D);
        _stateController.Init(rb2D);
        _animationController.Init(_stateController);
        _lifeController.Init(_mover);
    }
    private void Process()
    {
        _mover.Move();
        _wireActioner.Fire();
        _shootGun.Fire();
        _flyingSquirrelActioner.Fire();
        _stateController.Update();
        _animationController.Update();
        _separationInstructioner.Execution();
    }
    #endregion

    #region Test
#if UNITY_EDITOR
    // テストコード群
#endif
    #endregion
}