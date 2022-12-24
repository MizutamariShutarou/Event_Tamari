using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GoalManager : MonoBehaviour
{
    [SerializeField] PresentPresenter _presentPresenter;

    [SerializeField] RectTransform _resultPanel;
    [SerializeField] Text _goalText;
    [SerializeField] Button _backTitleButton;
    [SerializeField] float _resultDuration = 0.2f;
    [SerializeField] float _textDuration = 0.1f;
    [SerializeField] float _tweenDelay = 0.5f;
    [SerializeField] Ease _textEase = Ease.OutQuint;

    SpriteRenderer _spriteRenderer;

    private static bool _isGoaled;
    public static bool IsGoaled => _isGoaled;

    private void Start()
    {
        _isGoaled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void ChangeBool()
    {
        _isGoaled = true;
        _backTitleButton.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_presentPresenter.BoolChange() && collision.gameObject.layer == LayerMask.NameToLayer("Union"))
        {
            Debug.Log("Goal");
            _spriteRenderer.enabled = false;
            var seq = DOTween.Sequence();
            seq.Append(_resultPanel.DOScale(Vector3.one, _resultDuration).SetEase(Ease.OutBounce)).SetDelay(_textDuration)
               .Append(_goalText.DOFade(1f, _textDuration)).SetEase(_textEase)
               .OnComplete(() => ChangeBool());
        }
    }
}
