using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    private Transform posListTransform;

    private void Start()
    {
        posListTransform = transform.GetChild(10);
    }

    
}
