using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Player player;

    public float followSpeed = 5f;
    public float followThresholdX = 2.5f;
    Vector3 startPos = new Vector3(-52.16f, -2.2f, -10f);
 

    private void Start()
    {
        transform.position = startPos;
    }
    private void Update()
    {
       
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < followThresholdX)
        {
            FollowPlayer();
        }
        //Debug.Log("카메라 위치 " + transform.position.x);
        //Debug.Log("플레이어 위치 " + player.transform.position.x);
        //if(player.transform.position)
        //transform.position = new Vector3(player.transform.position.x, -2.2f, -10f);
    }

    private void FollowPlayer()
    {
        // 플레이어를 따라가게 카메라 위치 조정
      
        transform.position = new Vector3(player.transform.position.x + followThresholdX, transform.position.y, transform.position.z);

    }
}
