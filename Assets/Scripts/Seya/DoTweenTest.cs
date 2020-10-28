using UnityEngine;
using DG.Tweening;
public class DoTweenTest : MonoBehaviour
{
    [SerializeField] bool swing;
    [SerializeField] bool flexibility;
    void Start()
    {
        if (swing)
        {
            DOTween.Sequence()
                .Append(this.transform.DOMoveX(25, 3f).SetRelative())
                .Join(this.transform.DORotate(new Vector3(0, 180, 0), 3f))
                .Append(this.transform.DOMoveX(-25, 3f).SetRelative())
                .Join(this.transform.DORotate(new Vector3(0, 360, 0), 3f))
                .SetLoops(-1)
                .Play();
        }
        else if (flexibility)
        {
            DOTween.Sequence()
                .Append(this.transform.DOScale(new Vector3(15, 5, 3), 3f).SetRelative())
                .Append(this.transform.DOScale(new Vector3(-15, -5, -3), 3f).SetRelative())
                .SetLoops(-1)
                .Play();
        }
    }
}
