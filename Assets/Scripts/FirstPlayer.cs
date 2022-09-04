using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    private Rigidbody rB;
    [SerializeField] bool isGrounded;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material = playerManager.collectedMat;

        //temas ettigimiz nesneleri PlayerManager'den gorebilmek icin 
        playerManager.collidedList.Add(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) 
        {
            Grounded();
        }
    }

    void Grounded()
    {
        isGrounded = true;
        playerManager.playerState = PlayerManager.PlayerState.Move;
        rB.useGravity = false;
        rB.constraints = RigidbodyConstraints.FreezeAll; //karakterin yer ile temasindan sonra isGrounded falsea girecek

        Destroy(this, 1);
    }
}