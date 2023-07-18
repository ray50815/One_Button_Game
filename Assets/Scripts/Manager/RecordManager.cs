using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;
    public GameObject bannerPrefab;

    void Awake() 
    {
        if (instance != null){
            return;
        }
        instance = this;

        SortFiles(); 
    }
    private string filePath;
    private int dataAmount = 10;
    //獲取可存入路徑
    private string GetPath()
    {
        for (int i = 0; i < dataAmount; i++){
            string filePath = Application.streamingAssetsPath + "/data" + i + ".txt";
            if (!System.IO.File.Exists(filePath)){
                return filePath;
            }
        }
        return Application.streamingAssetsPath + "/data10.txt";
    }
    //獲取路徑(指定)
    private string GetPath(int i)
    {
        return Application.streamingAssetsPath + "/data" + i + ".txt";
    }
    //獲取全部路徑 //如果還是GetPath的話，空集合會重複
    private List<string> GetAllPath()
    {
        List<string> filePaths = new List<string>();
        for (int i = 0; i < dataAmount + 1; i++)
        {
            string filePath = GetPath(i);
            if (File.Exists(filePath)){
                filePaths.Add(filePath);
            }
        }
        return filePaths;
    }
    //建立檔案
    public void CreateTextFile(string name, int count, float distance)
    {
        filePath = GetPath();
        // 创建文本文件并写入数据
        PlayerData data = new PlayerData();
        data.name = name;
        data.count = count;
        data.distance = distance;

        string json = JsonConvert.SerializeObject(data);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.Write(json);
        }
    }
    //複寫檔案
    public void RewriteTextFile(int fileIndex, string name, int count, float distance)
    {
        filePath = GetPath(fileIndex);
        // 创建文本文件并写入数据
        PlayerData data = new PlayerData();
        data.name = name;
        data.count = count;
        data.distance = distance;

        string json = JsonConvert.SerializeObject(data);

        using (StreamWriter file = new StreamWriter(filePath))
        {
            file.Write(json);
        }
    }
    //輸出檔案
    public string PrintTextFile(int fileIndex)
    {
        filePath = GetPath(fileIndex);
        if (File.Exists(filePath))
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                string json = file.ReadToEnd();
                PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);
                return (fileIndex+1) + "." + data.name + ": " 
                + data.count + ", " + data.distance.ToString("0.00");
            }
        }
        return null;
    }
    //排序檔案
    public List<PlayerData> SortFiles()
    {
        List<string> filePaths = GetAllPath();
        List<PlayerData> sortedData = new List<PlayerData>();

        foreach (string filePath in filePaths)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                string json = file.ReadToEnd();
                PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json);
                sortedData.Add(data);
            }
        }
        sortedData.Sort((x, y) => y.count.CompareTo(x.count));
        //複寫
        for (int i = 0; i < dataAmount + 1; i++)
        {
            if (i < sortedData.Count)
            {
                PlayerData data = sortedData[i];
                RewriteTextFile(i, data.name, data.count, data.distance);
            }
        }
        // Create a new banner for the player with the highest score
        if (sortedData.Count > 0)
        {
            CreateBanner(sortedData[0]);
        }
        return sortedData;
    }

    
    public void CreateBanner(PlayerData data)
    {
    DestroyOldBanner();
    Vector3 position = new Vector3(data.distance/10, 0, 0);

    GameObject banner = Instantiate(bannerPrefab, position, Quaternion.Euler(0, 90, 0));

    banner.name = data.name;
    }

    public void DestroyOldBanner()
    {
        GameObject oldBanner = GameObject.FindGameObjectWithTag("Banner");
        if (oldBanner != null)
        {
            Destroy(oldBanner);
        }
    }


}
