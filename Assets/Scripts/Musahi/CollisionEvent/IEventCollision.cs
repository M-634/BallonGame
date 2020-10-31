using System.Collections;

/// <summary>
/// プレイヤーと衝突するとイベントが発生するインタフェース
/// </summary>
public interface IEventCollision
{
    void CollisionEvent(EventSystemInGameScene eventSystem);
}




