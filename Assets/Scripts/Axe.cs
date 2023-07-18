using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public static Axe instance;
    void Awake() {
        if (instance != null){
            return;
        }
        instance = this;
    }

    public float throwAngle = 45f;

    private float startPos_x;
    private float endPos_x;
    [HideInInspector]
    public float finalDistance;
    [HideInInspector]
    public float distance;

    private GameManager gameManager;
    private Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        RB = GetComponent<Rigidbody>();
        startPos_x = transform.position.x;
    }

    public void ThrowWithForce(int count){
        //拋物線
        Quaternion rotation = Quaternion.Euler(0f, 0f, throwAngle);
        Vector3 forceDirection = rotation * transform.right;
        Vector3 force = forceDirection * count;

        RB.AddForce(force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.isFly){
            //清空速度
            RB.velocity = Vector3.zero;
            RB.angularVelocity = Vector3.zero;
            //計算最終距離
            finalDistance = (transform.position.x - startPos_x) * 10;
            gameManager.End();
        }
    }
    //計距離
    public float GetDistance(){
        endPos_x = transform.position.x;
        distance = (endPos_x - startPos_x) * 10;
        return distance;
    }
}