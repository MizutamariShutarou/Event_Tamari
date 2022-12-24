using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MainLifeController
{
    [SerializeField]
    private bool _isGodMode = false;

    private static int _life = CommonConstant.MAX_LIFE;

    public static int MainLife => _life;
    public bool IsDeath => _isDeath;
    public bool IsDamage => _isDamage;

    private Rigidbody2D _rb2D = default;
    private MoveBehavior _mover = default;
    private bool _isDamage = false;
    private bool _isDeath = false;

    protected void Init(MoveBehavior moveController)
    {
        _mover = moveController;
        _rb2D = _mover.Rigidbody2D;
    }

    public void ResetLife()
    {
        _life = CommonConstant.MAX_LIFE;
    }

    [SceneName, SerializeField]
    private string _gameOverSceneName = default;
    public void Damage(int damage, Vector2 dir, float power, int knockBackTime)
    {
        if (!_isGodMode)
        {
            StartKnockBack(knockBackTime);
            if (_life < 0)
            {
                Debug.LogWarning("Playerが倒されました");
                StateUpdateOnDamage();
                _isDeath = true;
                // フェード等行う場合この行に処理を追加する。
                //SceneManager.LoadScene(_gameOverSceneName);
            }
            else
            {
                StateUpdateOnDeath();
                _life -= damage;
            }

            // ノックバックする
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(dir.normalized * power, ForceMode2D.Impulse);
            _mover.StopMove(knockBackTime);
        }
    }
    protected virtual void StateUpdateOnDeath()
    {

    }
    protected virtual void StateUpdateOnDamage()
    {

    }
    private async void StartKnockBack(int knockBackTime)
    {
        _isGodMode = true;
        _isDamage = true;
        await Task.Run(() => Thread.Sleep(knockBackTime));
        _isGodMode = false;
        _isDamage = false;
    }
}