using UnityEngine;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour
{
    public PlayerState playerState;
    public LevelState levetState;
    public Material collectedMat;
    public Transform particlePrefab;

    public List<GameObject> collidedList;//temas ettigimiz nesnelere ulasabilmek icin 

    public Transform collectedPoolTransform;
    public enum PlayerState
    {
        Stop,
        Move
    }

    public enum LevelState
    {
        NotFinished,
        Finished
    }

    public void CallMakeSphere()
    {
        foreach (GameObject obj in collidedList)
        {
            obj.GetComponent<CollectedObjController>().MakeSphere();
        }
    }
}