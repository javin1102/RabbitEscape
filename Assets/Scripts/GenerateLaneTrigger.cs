using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLaneTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randDir = Random.Range(0, 3);
            int randPow = Random.Range(0, 3);

            if(randDir == 0)
            {
                InGameManager.instance.nextLaneDir = Direction.STRAIGHT;
            }
            else if (randDir == 1)
            {
                InGameManager.instance.nextLaneDir = Direction.LEFT;
            }
            else 
            {
                InGameManager.instance.nextLaneDir = Direction.RIGHT;
            }

            if (randPow == 0)
                InGameManager.instance.GenerateLaneEnter(true);
            else
                InGameManager.instance.GenerateLaneEnter(true);
        }
    }


}
