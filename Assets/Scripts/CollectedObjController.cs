using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObjController : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] Transform sphere;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

        sphere = transform.GetChild(0);

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();

            Rigidbody rB = GetComponent<Rigidbody>();

            rB.useGravity = false;
            rB.constraints = RigidbodyConstraints.FreezeAll;

            GetComponent<Renderer>().material = playerManager.collectedMat;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectibleObj"))
        {
            if (!playerManager.collidedList.Contains(other.gameObject))
            {
                other.gameObject.tag = "CollectedObj";
                other.transform.parent = playerManager.collectedPoolTransform;
                playerManager.collidedList.Add(other.gameObject);
                other.gameObject.AddComponent<CollectedObjController>();
            }
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            DestroyTheObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectibleList"))
        {
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.parent = playerManager.collectedPoolTransform;

            foreach (Transform child in other.transform)
            {
                if (!playerManager.collidedList.Contains(child.gameObject))
                {
                    playerManager.collidedList.Add(child.gameObject);
                    child.gameObject.tag = "CollectedObj";
                    child.gameObject.AddComponent<CollectedObjController>();
                }
            }
        }

        if (other.gameObject.CompareTag("FinishLine"))
        {
            if (playerManager.levetState != PlayerManager.LevelState.Finished)
            {
                playerManager.levetState = PlayerManager.LevelState.Finished;
                playerManager.CallMakeSphere();
            }
        }
    }

    void DestroyTheObject()
    {
        playerManager.collidedList.Remove(gameObject);
        Destroy(gameObject);

        Transform particle = Instantiate(playerManager.particlePrefab, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().startColor = playerManager.collectedMat.color;
        Destroy(particle,1);
    }

    public void MakeSphere()
    {
        //DepthMask e girecek kuplerin collider ve meshlerin kapali oldugunu bilgisayara soyleyen kod 
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //DepthMask e giren kuplerin sekil degistirmesi icin gereken kod 
        sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;

        sphere.gameObject.GetComponent<Renderer>().material = playerManager.collectedMat;
    }

    public void DropObj()
    {
        sphere.gameObject.layer = 6;

        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        sphere.gameObject.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = true;
    }
}