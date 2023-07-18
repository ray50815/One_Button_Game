using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject background;

    private AudioSource bgAudio;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        bgAudio = background.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (gameManager.isReady){
        // }
        // else if (gameManager.isChargeUp){
        // }
        // else if (gameManager.isFly){
        // }
        if (gameManager.isEnd){
            bgAudio.mute = true;
        }
    }
}
