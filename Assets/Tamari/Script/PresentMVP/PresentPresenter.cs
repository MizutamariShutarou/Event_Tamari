using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentPresenter : MonoBehaviour
{
    private const int PresentCountLimit = 3;

    private PresentModel _model;
    [SerializeField] private PresentView _view;
    [SerializeField] private int _startPresentCount = 0;
    private void Awake()
    {
        _model = new PresentModel();
        _view.Initialize(_startPresentCount, PresentCountLimit);

        Bind();
    }
    public void Bind()
    {
        _model.OnSetCurrentNum += _view.SetPresentCount;
    }
    public void CountPresent()
    {
        if(PresentCountLimit > _model.CurrentNum)
        {
            _model.CountNum();
        }
    }
    public void ResetCount()
    {
        _model.ResetNum();
    }
    public void BoolChange()
    {
        _model.BoolChange();
    }

    public void ActivePresent()
    {
        CountPresent();
        _view.HidePresent(_model.CurrentNum - 1);
        _view.ShowPresentIcon(_model.CurrentNum);
        Debug.Log(_model.CurrentNum);
        if (_model.CurrentNum == PresentCountLimit)
        {
            BoolChange();
        }
    }
}
