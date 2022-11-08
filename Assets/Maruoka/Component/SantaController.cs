using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private SantaMoveBehavior _mover = default;
    [SerializeField]
    private JumpBehavior _jumper = default;
    [SerializeField]
    private SantaLifeController _lifeControler = default;
    [SerializeField]
    private ChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private CombineController _combiner = default;
    [SerializeField]
    private SantaStateController _stateControler = default;
    [SerializeField]
    private SantaAnimationController _animationController = default;
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
        _jumper.Init(rb2D, GetComponent<GroundCheck>());
        _lifeControler.Init(_mover);
        _animationController.Init(_stateControler);
    }
    private void Process()
    {
        _mover.Move();
        _jumper.Jump();
        _operatCharacterChanger.OnChangeOperatCharacter();
        _combiner.Combine();
        _stateControler.Update();
        _animationController.Update();
    }
    #endregion


    #region Test
#if UNITY_EDITOR
    // テストコード群
#endif
    #endregion
}