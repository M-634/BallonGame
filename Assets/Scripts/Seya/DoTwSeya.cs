using UnityEngine;
using DG.Tweening;
public class DoTwSeya : MonoBehaviour
{
    [SerializeField] Vector3 setSwing;
    [SerializeField, Range(30f, -30f)] float swingRightX = 25.0f;
    private float swingLeftX = 0;
    [SerializeField] Vector3 setRoll;
    [SerializeField] Vector3 startRoll;
    [SerializeField] Vector3 Roll1;
    [SerializeField] Vector3 Roll2;
    [SerializeField] Vector3 Roll3;
    [SerializeField] Vector3 Roll4;
    [SerializeField] Vector3 Roll5;
    [SerializeField] bool swing;
    [SerializeField] bool flexibility;
    [SerializeField] bool meteorite;
    [SerializeField] bool roll;
    void Start()
    {
        if (swing)
        {   //　振幅運動
            swingLeftX -= swingRightX;
            DOTween.Sequence()
                .Append(this.transform.DOMove(setSwing,0f))
                .Append(this.transform.DOMoveX(swingRightX, 3f).SetRelative())
                .Append(this.transform.DOMoveX(swingLeftX, 3f).SetRelative())
                .SetLoops(3)
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
        else if (roll)
        {   // 公転運動
            DOTween.Sequence()
                .Append(this.transform.DOMove(setRoll, 0f))
                .Append(this.transform.DOPath(new Vector3[] { startRoll, Roll1, Roll2, Roll3, Roll4, Roll5, setRoll }, 3.0f, PathType.CatmullRom).SetEase(Ease.Linear))
                .SetLoops(-1)
                .Play();
        }
    }
}
