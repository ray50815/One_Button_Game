using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator devilAnimator;
    public Animator hitAnimator;
    public Animator bananaAnimator;
    public Animator startCameraAnimator;
    public Animator mainCameraAnimator;

    private GameManager gameManager;

    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && gameManager.isMenu){
            startCameraAnimator.SetTrigger("Start");
        }
        else if (gameManager.isChargeUp){
            devilAnimator.SetTrigger("Start");
            mainCameraAnimator.SetTrigger("Transition_ChargeUp");
            StartCoroutine(ActivateThrowAfterDelay(4));
        }
        else if (gameManager.isEnd){
            bananaAnimator.enabled = false;
        }

        if (Input.GetKeyDown("space") && gameManager.isChargeUp && hitAnimator != null){
            hitAnimator.Play("Hit", 0, 0f);
        }
    }
    IEnumerator ActivateThrowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        devilAnimator.SetTrigger("Throw");
    }
}
