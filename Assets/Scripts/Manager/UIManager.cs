using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject CG;
    public GameObject inputField;
    public TextMeshProUGUI inputText;
    public GameObject[] panels;
    public Sprite[] sprites;
    public TextMeshProUGUI[] records;

    public TextMeshProUGUI chargeUpCountText;
    public TextMeshProUGUI flyCountText;
    public TextMeshProUGUI flyDistanceText;
    public TextMeshProUGUI endDistanceText;

    private GameManager gameManager;
    private Axe axe;
    private RecordManager recordManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        recordManager = RecordManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        axe = Axe.instance;
        if (gameManager.isMenu){
            OpenPanel(0);
            GetRecords();
        }
        if (gameManager.isReady){
            OpenPanel(1);
        }
        else if (gameManager.isChargeUp){
            OpenPanel(2);
            chargeUpCountText.text = gameManager.GetCount().ToString() + "Hit";
        }
        else if (gameManager.isFly){
            OpenPanel(3);
            flyCountText.text = gameManager.GetCount().ToString() + "Hit";
            flyDistanceText.text = axe.GetDistance().ToString("0.00");

        }
        if (gameManager.isEnd){
            OpenPanel(4);
            endDistanceText.text = axe.finalDistance.ToString("0.00");
            GetCG();
        }
    }
    void GetRecords(){
        for (int i = 0;i < 10; i++){
            records[i].text = recordManager.PrintTextFile(i);
        }
    }
    void GetCG(){
        float distance = GameObject.Find("Banana").GetComponent<Axe>().distance;
        if (distance <= 500){
            CG.GetComponent<Image>().sprite = sprites[0];
        }
        else if (distance > 500 && distance <= 1000)
        {
            CG.GetComponent<Image>().sprite = sprites[1];
        }
        else if (distance > 1000 && distance <= 1500)
        {
            CG.GetComponent<Image>().sprite = sprites[2];
        }
        else if (distance > 1500 && distance <= 2000)
        {
            CG.GetComponent<Image>().sprite = sprites[3];
        }
        else if (distance > 2000)
        {
            CG.GetComponent<Image>().sprite = sprites[4];
        }
    }
    public void OK(){
        string name = inputText.text;
        if (name.Length == 1){
            name = "none";
        }
        recordManager.CreateTextFile(name, gameManager.GetCount(), axe.finalDistance);
        recordManager.SortFiles();
        inputField.SetActive(false);
    }
    void OpenPanel(int panelIndex){
        for (int i = 0; i < panels.Length; i++){
            if (i == panelIndex){
                panels[i].SetActive(true);
            }
            else{
                panels[i].SetActive(false);
            }
        }
    }
}
