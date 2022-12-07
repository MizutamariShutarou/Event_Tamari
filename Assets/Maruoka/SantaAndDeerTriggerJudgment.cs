using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サンタとトナカイのコライダー判定を、
/// コリジョンマトリックスを使用して無くしたが
/// 互いの接触判定が必要なために作成したクラス。
/// </summary>
public class SantaAndDeerTriggerJudgment : MonoBehaviour
{
    [TagName, SerializeField]
    private string _buddyName = default;

    private CombineController _parentsCombiner = null;

    private void Start()
    {
        if (transform.parent.TryGetComponent(out DeerController deer))
        {
            _parentsCombiner = deer.Combiner;
        }
        else if (transform.parent.TryGetComponent(out SantaController santa))
        {
            _parentsCombiner = santa.Combiner;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _buddyName)
        {
            _parentsCombiner.OnPossibleCombine();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == _buddyName)
        {
            _parentsCombiner.OnImpossibleCombine();
        }
    }
}