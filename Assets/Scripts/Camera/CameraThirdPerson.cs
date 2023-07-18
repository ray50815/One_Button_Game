using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPerson : MonoBehaviour
{
    public Transform banana; 

    void Update()
    {
        LookAtTarget();
    }
    
    void LookAtTarget()
    {
        Vector3 directionToTarget = banana.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }
}
