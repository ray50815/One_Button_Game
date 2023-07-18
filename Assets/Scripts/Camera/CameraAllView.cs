using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAllView : MonoBehaviour
{
    public Transform banana;
    public Transform player;
    public float speed;
    private Vector3 dir;
    private Camera currentCamera;
    private bool isInitial = false;
    private bool isArrive;
    private bool isSmall;
    private bool isBig;

    private GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentCamera = GetComponent<Camera>();
        isArrive = (transform.position.x <= player.position.x);
        isBig = (currentCamera.orthographicSize >= 3.5);
        isSmall = (currentCamera.orthographicSize <= 70);
        if (!isInitial && gameManager.isEnd){
            Initialize();
            isInitial = true;
        }
        else{
            if (!isArrive && isSmall){
                ScaleUp();
            }
            if (!isArrive && !isSmall){
                Move();
            }
            if (isArrive && isBig){
                ScaleDown();
            }
        }
    }
    void Initialize(){
        Vector3 initialPos = new Vector3 (banana.position.x, 20, 2);
        transform.position = initialPos;
        currentCamera.orthographicSize = 3;
        //目標方向
        dir = new Vector3(player.position.x - banana.position.x, 0, 0).normalized;
    }
    void Move(){
        transform.Translate(dir * Time.deltaTime * speed);
    }
    void ScaleUp(){
        currentCamera.orthographicSize += speed / 2 * Time.deltaTime;
    }
    void ScaleDown(){
        currentCamera.orthographicSize -= speed / 2 * Time.deltaTime;
    }
}
