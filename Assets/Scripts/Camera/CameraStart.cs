using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStart : MonoBehaviour
{
    private GameManager gameManager;
    public void GoReady(){
        gameManager = GameManager.instance;
        gameManager.Ready();
    }
}
