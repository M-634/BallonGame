using UnityEngine;
using DG.Tweening;
public class DoTweenTest : MonoBehaviour
{
    [SerializeField] bool swing;
    [SerializeField] bool flexibility;
    [SerializeField] bool meteorite;
    void Start()
    {
        if (swing)
        {   //　振幅運動
            DOTween.Sequence()
                .Append(this.transform.DOMoveX(25, 3f).SetRelative())
                .Join(this.transform.DORotate(new Vector3(0, 180, 0), 3f))  // 1周半回転する
                .Append(this.transform.DOMoveX(-25, 3f).SetRelative())
                .Join(this.transform.DORotate(new Vector3(0, 360, 0), 3f))  // 1周半回転する
                .SetLoops(-1)
                .Play();
        }
        else if (flexibility)
        {   // 伸縮する
            DOTween.Sequence()
                .Append(this.transform.DOScale(new Vector3(15, 5, 3), 3f).SetRelative())
                .Append(this.transform.DOScale(new Vector3(-15, -5, -3), 3f).SetRelative())
                .SetLoops(-1)
                .Play();
        }
        else if (meteorite)
        {   // 隕石っぽいもの
            DOTween.Sequence()
                .Append(this.transform.DOMove(new Vector3(0, 300, 600), 3f))
                .Join(this.transform.DOScale(new Vector3(0, 0, 0), 0f))
                .Append(this.transform.DOMove(new Vector3(0, 0, 300), 8f).SetEase(Ease.InQuad))
                .Join(this.transform.DOScale(new Vector3(15, 15, 15), 5f))
                .SetLoops(-1)
                .Play();
        }
    }
}
