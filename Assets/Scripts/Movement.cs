using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float movementSpeed;
    [SerializeField] float controlSpeed;
    [SerializeField] float horizontalLimit;

    [SerializeField] bool isTouching;
    float touchPosX;
    Vector3 direction;

    private void Start()
    {
        
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (playerManager.playerState == PlayerManager.PlayerState.Move)
        {
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
        }
        if (isTouching)
        {
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        }
        touchPosX = Mathf.Clamp(touchPosX, -horizontalLimit, horizontalLimit); //ground disina cikmamasi icin
        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z); 
    }
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }
}