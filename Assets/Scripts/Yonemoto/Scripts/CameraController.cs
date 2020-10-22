using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    Vector3 playerPos;
    [SerializeField] private float y, z;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + y, playerPos.z + z); 
    }
}
