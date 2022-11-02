using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaController : MonoBehaviour
{
    #region Properties
    public SantaLifeControl LifeControler => _lifeControler;
    #endregion

    #region Inspector Variables
    [SerializeField]
    private SantaMoveBehavior _mover = default;
    [SerializeField]
    private JumpBehavior _jumper = default;
    [SerializeField]
    private SantaLifeControl _lifeControler = default;
    [SerializeField]
    private ChangeOperatCharacter _changeOperatCharacter = default;
    #endregion

    #region Member Variables
    private SantaStateControl _stateControler = new SantaStateControl();
    #endregion

    #region Constant
    #endregion

    #region Events
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

    #region Enums
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Init()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        _mover.Init(rb2D);
        _jumper.Init(rb2D, GetComponent<GroundCheck>());
        _lifeControler.Init(rb2D, _mover);
    }
    private void Process()
    {
        _mover.Move();
        _jumper.Jump();
    }
    #endregion

#if UNITY_EDITOR
    // テストコード
    public void OnSantaDamage()
    {
        LifeControler.Damage(1, new Vector2(1f, 1f), 10f);
    }
#endif
}