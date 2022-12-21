using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PresentView : MonoBehaviour
{
    [SerializeField] private List<Image> _emptyIcons;
    [SerializeField] private List<Image> _presentIcons;

    [SerializeField] private List<GameObject> _presentObj;

    private int _presentMaxCount;
    private int _presentCurrentCount;
    public void Initialize(int startPresentCount, int presentMaxCount)
    {
        _presentMaxCount = presentMaxCount;
    }
    public void SetPresentCount(int presentCount)
    {
        _emptyIcons[presentCount - 1].gameObject.SetActive(true);
        _presentIcons[presentCount - 1].gameObject.SetActive(false);
    }

    public void ShowPresentIcon(int index)
    {
        _emptyIcons[index - 1].gameObject.SetActive(true);
        _presentIcons[index - 1].gameObject.SetActive(true);
    }
    
    public void HidePresentIcon(int presentMaxCount)
    {
        _emptyIcons[presentMaxCount].gameObject.SetActive(false);
        _presentIcons[presentMaxCount].gameObject.SetActive(false);
    }

    public void HidePresent(int index)
    {
        _presentObj[index].gameObject.SetActive(false);
    }
}
