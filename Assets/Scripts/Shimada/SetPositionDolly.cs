
using Cinemachine;
using UnityEngine;

public class SetPositionDolly : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float cycleTime = 10.0f;

    private CinemachineTrackedDolly dolly;
    private float pathPositionMax;
    private float pathPositionMin;

    private void Start()
    {
        // バーチャルカメラがセットされていなければ中止
        if (this.virtualCamera == null)
        {
            this.enabled = false;
            return;
        }

        // ドリーコンポーネントを取得できなければ中止
        this.dolly = this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (this.dolly == null)
        {
            this.enabled = false;
            return;
        }

        // Positionの単位をトラック上のウェイポイント番号基準にするよう設定
        this.dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;

        // ウェイポイントの最大番号・最小番号を取得
        this.pathPositionMax = this.dolly.m_Path.MaxPos;
        this.pathPositionMin = this.dolly.m_Path.MinPos;
    }

    private void Update()
    {
        // cycleTime秒かけてトラック上を往復させる
        var t = 0.5f - (0.5f * Mathf.Cos((Time.time * 2.0f * Mathf.PI) / this.cycleTime));
        this.dolly.m_PathPosition = Mathf.Lerp(this.pathPositionMin, this.pathPositionMax, t);
    }
}
