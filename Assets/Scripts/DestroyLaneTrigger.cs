using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLaneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //destroy ENV
            Destroy(transform.parent.gameObject);
        }
    }
}
