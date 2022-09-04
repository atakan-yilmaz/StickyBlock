using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    public Transform target;

    [SerializeField] public float smoothSpeed;
    [SerializeField] Vector3 offset;
  

    private void LateUpdate()
    { 
        if (playerManager.levetState == PlayerManager.LevelState.NotFinished)
        {
            Vector3 desirePos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desirePos, smoothSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPos.z);
        }
    }
}