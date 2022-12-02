using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaController : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    private SantaMoveBehavior _mover = default;
    [SerializeField]
    private JumpBehavior _jumper = default;
    [SerializeField]
    private SantaLifeController _lifeController = default;
    [SerializeField]
    private ChangeOperatCharacter _operatCharacterChanger = default;
    [SerializeField]
    private CombineController _combiner = default;
    [SerializeField]
    private SantaStateController _stateControler = default;
    [SerializeField]
    private SantaAnimationController _animationController = default;

    public SantaMoveBehavior Mover => _mover;
    public JumpBehavior Jumper => _jumper;
    public SantaLifeController LifeController => _lifeController;
    public ChangeOperatCharacter OperatCharacterChanger => _operatCharacterChanger;
    public CombineController Combiner => _combiner;
    public SantaStateController StateControler => _stateControler;
    public SantaAnimationController AnimationController => _animationController;
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
        var gc = GetComponent<GroundCheck>();
        _mover.Init(rb2D);
        _jumper.Init(rb2D, gc);
        _stateControler.Init(rb2D, gc,this);
        _lifeController.Init(_mover);
        _animationController.Init(_stateControler);

    }
    private void Process()
    {
        _mover.Update();
        _jumper.Update();
        _operatCharacterChanger.Update();
        _combiner.Update();
        _stateControler.Update();
        _animationController.Update();
    }
    #endregion

    #region Test
#if UNITY_EDITOR
    // テストコード群
    public void TestDamage()
    {
        _lifeController.Damage(1, new Vector2(1, 1), 10f);
    }
#endif
    #endregion
}