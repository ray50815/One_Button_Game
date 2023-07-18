using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//----------ReadME----------//
// 確認目前狀態:
// isReady
// 呼叫新的狀態:
// Ready()
//----------ReadME----------//
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() {
        if (instance != null){
            return;
        }
        instance = this;
    }

    public GameObject banana;
    public int countdown = 5;
    public int count = 0;
    public TextMeshProUGUI countdownText;

    public bool isMenu = true;
    public bool isReady = false;
    public bool isChargeUp = false;
    public bool isFly = false;
    public bool isEnd = false;

    void Start()
    {
        Menu();
        banana.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space") && isChargeUp){
            count++;
        }
        if (Input.GetKeyDown("space") && isEnd){
            Restart();
        }
    }
    //開始
    public void Menu(){
        isEnd = false;
        isMenu = true;
    }
    //準備
    public void Ready(){
        isMenu = false;
        isReady = true;
    }
    //蓄力
    public void ChargeUp(){
        isReady = false;
        isChargeUp = true;
        StartCoroutine(ActivateBananaAfterDelay(4.99f));
        StartCoroutine(Countdown());
    }
    //飛行過程
    public void Fly(){
        isChargeUp = false;
        isFly = true;
    }
    //落地結束
    public void End(){
        isFly = false;
        isEnd = true;
    }
    //重開(動作)
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ActivateBananaAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        banana.SetActive(true);
    }
    IEnumerator Countdown()
    {
        count = 0;
        for (int i = countdown; i > 0; --i){
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        banana.GetComponent<Axe>().ThrowWithForce(count);
        Fly();
    }

    public int GetCount(){
        return count;
    }
}
