using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
