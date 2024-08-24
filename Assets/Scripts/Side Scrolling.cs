using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class SideScrolling : MonoBehaviour
{    
    private Transform player;

    public float height = 6.5f;
    public float undergourndHeight = -9.9f;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;    
    }
    

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); //player.position.x
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergourndHeight : height;
        transform.position = cameraPosition;
    }

}
