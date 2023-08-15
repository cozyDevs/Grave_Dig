//Milo Wilson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float smoothSpeed;
    [SerializeField]
    Vector3 offset;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player")[0] != null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        else
        {
            Debug.Log("can't find player");
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desierdPossiton = player.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desierdPossiton, smoothSpeed);
            transform.position = smoothedPos;
        }
    }
}
