using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private MoveBehavior _mover = default;
    [SerializeField]
    private HornSwordAttackBehavior _hornSwordAttacker = default;
    [SerializeField]
    private RushAttackBehavior _rushAttacker = default;
    [SerializeField]
    private MainLifeController _lifeControler = default;
    [SerializeField]
    private ChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private CombineController _combiner = default;
    [SerializeField]
    private DeerStateController _stateController = default;
    [SerializeField]
    private DeerAnimationController _animationController = default;
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
        _mover.Init(GetComponent<Rigidbody2D>());
        _hornSwordAttacker.Init();
        _rushAttacker.Init();
        _lifeControler.Init(_mover);
        _animationController.Init(_stateController);
    }
    private void Process()
    {
        _mover.Move();
        _hornSwordAttacker.Fire();
        _rushAttacker.Fire();
        _operatCharacterChanger.OnChangeOperatCharacter();
        _combiner.Combine();
        _stateController.Update();
        _animationController.Update();
    }

    private void OnReadyFire()
    {

    }
    #endregion


    #region Test
#if UNITY_EDITOR
    // テストコード群
#endif
    #endregion
}