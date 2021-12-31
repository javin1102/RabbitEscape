using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehavior : MonoBehaviour
{
    private InGameManager instance;
    private Transform generatePosStraight,generatePosLeft,generatePosRight;
    public Transform powPosList;


    private void Start()
    {
        instance = InGameManager.instance;
        instance.onGenerateLane += GenerateLane;
        generatePosStraight = transform.GetChild(4);
        generatePosLeft = transform.GetChild(5);
        generatePosRight = transform.GetChild(6);
        powPosList = transform.Find("PowerUpPos");
    }


    void GenerateLane(bool generatePow)
    {


        if (instance.nextLaneDir == Direction.STRAIGHT)
        {
            GenerateGround(Quaternion.Euler(transform.eulerAngles), generatePosStraight.position, 0,generatePow);
        }


        if (instance.nextLaneDir == Direction.LEFT)
        {
            Quaternion rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y - 90,0);
            GenerateGround(rotation, generatePosLeft.position, 1, generatePow);


        }

        if(instance.nextLaneDir == Direction.RIGHT)
        {
            Quaternion rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 90, 0);
            GenerateGround(rotation, generatePosRight.position, 2, generatePow);
        }





    }

    //generate next ground
    void GenerateGround(Quaternion rotation, Vector3 pos, int groundIdx,bool generatePow)
    {
        GameObject go = Instantiate(instance.groundList[groundIdx], pos, rotation);

        int randObsIdx = Random.Range(0, instance.groundList.Length);
        GameObject obs = Instantiate(instance.obsList[randObsIdx], go.transform, false);
        obs.transform.SetParent(go.transform);

        if (generatePow)
        {
            GeneratePow(go.transform);
        }
    }

    void GeneratePow(Transform parent)
    {
        int randPowIdx = Random.Range(0, instance.powList.Length);
        GroundBehavior nextGround = parent.GetComponent<GroundBehavior>();
        nextGround.powPosList = nextGround.transform.Find("PowerUpPos");
        int randomPowPosIdx = Random.Range(0, nextGround.powPosList.childCount);
        Quaternion rot = Quaternion.Euler(90, 0, 0);
        Instantiate(instance.powList[randPowIdx], nextGround.powPosList.GetChild(randomPowPosIdx).position, rot);

    }
    private void OnDestroy()
    {
        InGameManager.instance.onGenerateLane -= GenerateLane;
    }

}
