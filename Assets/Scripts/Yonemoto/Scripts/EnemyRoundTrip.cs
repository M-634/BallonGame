using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoundTrip : MonoBehaviour
{
    [SerializeField] private float startX, startY, startZ;
    [SerializeField] private float endX, endY, endZ;
    private float moveTimer;
    private float roundTripSpan = 5;

    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer < roundTripSpan / 2)
        {
            //this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(24, 1.5f, -20), 0.01f);
            this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(endX, endY, endZ), 0.02f);
        }

        if (moveTimer >= roundTripSpan / 2 && moveTimer < roundTripSpan)
        {
            //this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(1, 1.5f, -40), 0.01f);
            this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startX, startY, startZ), 0.02f);
        }

        if (moveTimer >= roundTripSpan)
        {
            moveTimer = 0;
        }
    }
}
