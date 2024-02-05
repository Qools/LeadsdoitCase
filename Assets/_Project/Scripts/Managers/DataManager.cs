using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : Singleton<DataManager>
{
    public PlayerData playerData;
    public string filePath;
    private bool isReady = false;
    public string originName = "playerData.json";
    //for Android
    string sourcePath;
    string result = "";


    public override void Awake()
    {
        base.Awake();
        LoadGameData();
    }

    public override IEnumerator WaitInit(Action _initAction = null)
    {
        yield return new WaitForSeconds(0.1f);
        while (!GetIsDone())
        {
            Debug.Log("Waiting....! Data Manager is loading");
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        if (playerData.firstOpen == false)
        {
            Debug.Log("New User");
            SetFirstOpen();
        }
        else
        {
            Debug.Log("Returning User");
            SetOpenCount();
        }

        yield return new WaitForSeconds(0.05f);

        //Set Data Manager Version
        SetVersion(Application.version);

        isReady = true;
        //Wait small buffer for fail safe conditions
        yield return new WaitForSeconds(0.1f);
    }   

    public bool GetIsDone()
    {
        return isReady;
    }

    public void SetRateUs()
    {
        playerData.isRateUs = true;
        SaveData();
    }

    public bool GetRateUs()
    {
        return playerData.isRateUs;
    }

    public void SetFirstOpen()
    {
        playerData.openCount++;
        playerData.firstOpen = true;
        SaveData();
    }

    public bool GetFirstOpen()
    {

        return playerData.firstOpen;
    }

    public void SetOpenCount()
    {

        playerData.openCount++;
        SaveData();
    }

    public int GetOpenCount()
    {
        return playerData.openCount;
    }   

    public bool GetVibrateOn()
    {
        //Debug.Log(playerData.vibrateOn);
        return playerData.vibrateOn;
    }

    public void SetVibrateOn(bool setBool)
    {
        playerData.vibrateOn = setBool;
        SaveData();
    }

    public bool GetMuteSFX()
    {
        return playerData.muteSFX;
    }

    public void SetMuteSFX(bool setBool)
    {
        playerData.muteSFX = setBool;
        SaveData();
    }

    public void SetTutorialComplete(bool setBool)
    {
        playerData.isTutorialCompleted = setBool;
        SaveData();
    }

    public void SetVersion(string setString)
    {
        playerData.version = setString;
        SaveData();
    }

    public string GetVersion()
    {
        return playerData.version;
    }

    public void SetLevel(int level)
    {
        playerData.level = level;
        SaveData();
    }

    public int GetLevel()
    {
        return playerData.level;
    }

    #region Save Functionality

    public void SaveData()
    {

        StartCoroutine(SaveDataAsync(filePath, playerData));
    }

    private IEnumerator SaveDataAsync(string dataPath, PlayerData pData)
    {

        string tempData = JsonUtility.ToJson(pData);
        File.WriteAllText(dataPath, tempData);
        yield return null;
    }

    #endregion



    #region Load Game Data

    private void LoadGameData()
    {
        StartCoroutine(LoadGameDataAsync());
    }

    private IEnumerator LoadGameDataAsync()
    {

        filePath = Application.persistentDataPath + "/" + originName;
        sourcePath = Path.Combine(Application.streamingAssetsPath, originName);

        if (!File.Exists(filePath))
        {

            Debug.Log("File is not Exist");
#if UNITY_IOS

            string sPath = Application.streamingAssetsPath + "/" + originName;
            File.Copy(sPath, filePath);
            playerData = GetPersistendData(filePath);


#elif UNITY_ANDROID && !UNITY_EDITOR

            StartCoroutine(GetDataForAndroid());


#elif UNITY_EDITOR

            string sPath = Application.streamingAssetsPath + "/" + originName;
            File.Copy(sPath,filePath);
            playerData = GetPersistendData(filePath);
#endif

        }
        else
        {
            Debug.Log("File Exists");
            playerData = GetPersistendData(filePath);
        }

        isReady = true;
        yield return null;
    }


    private PlayerData GetPersistendData(string path)
    {
        string rawData = File.ReadAllText(path);
        PlayerData pData = JsonUtility.FromJson<PlayerData>(rawData);

        return pData;
    }

    IEnumerator GetDataForAndroid()
    {

        if (sourcePath.Contains("://"))
        {

            WWW www = new WWW(sourcePath);
            yield return www;
            result = www.text;
            File.WriteAllText(filePath, result);
            playerData = GetPersistendData(filePath);

        }
        else
        {

            result = File.ReadAllText(sourcePath);
            File.Copy(result, filePath);
            playerData = GetPersistendData(filePath);
        }

    }
    #endregion
}
