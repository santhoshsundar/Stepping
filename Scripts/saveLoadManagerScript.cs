using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
class gameInfo
{
    public int currentBallTextureIndex;
    public int totalPickupPoints;
    public int ballTextureAvailable;
    public int gamesPlayed;
    public int audioOnOff;
    public int doubleJumped;
    public int totalSteps;
    public int bestScore;
    public bool noAds;
    public int showInterstitialAds;
    public int showInterstitialMe;
    public bool isFirst;

}

public class saveLoadManagerScript : MonoBehaviour
{
    private static saveLoadManagerScript saveload;

    public int currentBallTextureIndex;
    public int totalPickupPoints;
    public int ballTextureAvailable;
    public int gamesPlayed;
    public int audioOnOff;
    public int doubleJumped;
    public int totalSteps;
    public int bestScore;
    public bool noAds;
    public int showInterstitialAds;
    public int showInterstitialMe;
    public bool isFirst;

    public static saveLoadManagerScript Saveload
    {
        get
        {
            if (!saveload)
            {
                saveload = FindObjectOfType<saveLoadManagerScript>();
            }
            return saveload;
        }

        set
        {
            saveload = value;
        }
    }
    void Awake()
    {
       //deleteOnLoad();
       //setGameInfo(); 
        loadGameInfo();
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<saveLoadManagerScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void saveGameInfo()
    {
        BinaryFormatter bformatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameInfo.dat", FileMode.Create);
        gameInfo gameInformation = new gameInfo();

        gameInformation.currentBallTextureIndex = currentBallTextureIndex;
        gameInformation.ballTextureAvailable = ballTextureAvailable;
        gameInformation.totalPickupPoints = totalPickupPoints;
        gameInformation.gamesPlayed = gamesPlayed;
        gameInformation.audioOnOff = audioOnOff;
        gameInformation.doubleJumped = doubleJumped;
        gameInformation.totalSteps = totalSteps;
        gameInformation.bestScore = bestScore;
        gameInformation.noAds = noAds;
        gameInformation.showInterstitialAds = showInterstitialAds;
        gameInformation.showInterstitialMe = showInterstitialMe;
        gameInformation.isFirst = isFirst;

        bformatter.Serialize(stream, gameInformation);
        stream.Close();
    }
    public void loadGameInfo()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameInfo gameInformation = bformatter.Deserialize(stream) as gameInfo;
            stream.Close();

            currentBallTextureIndex= gameInformation.currentBallTextureIndex;
            ballTextureAvailable=gameInformation.ballTextureAvailable;
            totalPickupPoints= gameInformation.totalPickupPoints;
            gamesPlayed = gameInformation.gamesPlayed;
            audioOnOff= gameInformation.audioOnOff;
            doubleJumped = gameInformation.doubleJumped;
            totalSteps = gameInformation.totalSteps;
            bestScore = gameInformation.bestScore;
            noAds = gameInformation.noAds;
            showInterstitialAds = gameInformation.showInterstitialAds;
            showInterstitialMe = gameInformation.showInterstitialMe;
            isFirst = gameInformation.isFirst;
        }
    }
    public void deleteOnLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
            File.Delete(Application.persistentDataPath + "/gameInfo.dat");
    }
    public void setGameInfo()
    {
        BinaryFormatter bformatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameInfo.dat", FileMode.Create);
        gameInfo gameInformation = new gameInfo();

        gameInformation.currentBallTextureIndex = 0;
        gameInformation.ballTextureAvailable = 3;
        gameInformation.totalPickupPoints = 0;
        gameInformation.gamesPlayed = 0;
        gameInformation.audioOnOff = 1;
        gameInformation.doubleJumped = 0;
        gameInformation.totalSteps = 0;
        gameInformation.bestScore = 0;
        gameInformation.noAds = false;
        gameInformation.showInterstitialAds = 0;
        gameInformation.showInterstitialMe = 0;
        gameInformation.isFirst = true;

        bformatter.Serialize(stream, gameInformation);
        stream.Close();
    }
}
