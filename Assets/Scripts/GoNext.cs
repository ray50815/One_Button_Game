using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNext : MonoBehaviour
{
    private GameManager gameManager;
    void Start(){
        gameManager = GameManager.instance;
    }
    public void GoReady(){
        gameManager.Ready();
    }
    public void GoChargeUp(){
        gameManager.ChargeUp();
    }
    // public void GoEnd(){
    //     gameManager.End();
    // }
}
