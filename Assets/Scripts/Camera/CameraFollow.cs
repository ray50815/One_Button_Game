using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform banana; 
    private Vector3 offset = new Vector3(-5, 0, 0);//變數型態:Vector3


    // Update is called once per frame
    void LateUpdate()   //LateUpadate:先移動在更新
    {
        transform.position = banana.position + offset ;
    }
}
