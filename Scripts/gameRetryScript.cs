using UnityEngine;
using System.Collections;
using System.IO;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class gameRetryScript : MonoBehaviour
{    
    private string message = " Download the Stepping Game in Play Store."+"\n"+ "\n"+
        "https://play.google.com/store/apps/details?id=com.TapAndTapGames.Stepping";
    private string title = "Stepping Game";
    public int totalPickupPoints;
    public int gamesPlayed;
    public int bestScore;
    public int doubleJumped;
    public int totalSteps;
    public int showInterstitialAds;
    private int showInterstitialMe;
    
    public AudioSource audioSourceRetry;
    public AudioClip[] retryClip;

    public GameObject rewardPanel;
    public GameObject rewardSuccessPanel;

    public void Awake()
    {
        audioSourceRetry = GetComponent<AudioSource>();
        rewardPanel.SetActive(false);
        rewardSuccessPanel.SetActive(false);

    }
    private static gameRetryScript instance;

    public static gameRetryScript Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<gameRetryScript>();
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Start()
    {
        if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            audioSourceRetry.volume = 0;
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            audioSourceRetry.volume = 1;
        }
        showInterstitialAds = saveLoadManagerScript.Saveload.showInterstitialAds;
        showInterstitialMe = saveLoadManagerScript.Saveload.showInterstitialMe;
        saveLoadManagerScript.Saveload.saveGameInfo();
    }

    public void restartGame()
    {
        audioSourceRetry.PlayOneShot(retryClip[0]);
        if (!saveLoadManagerScript.Saveload.noAds)
        {
            if (showInterstitialAds==showInterstitialMe)
            {
                showInterstitialAds += 5;
                saveLoadManagerScript.Saveload.showInterstitialAds = showInterstitialAds;
                saveLoadManagerScript.Saveload.saveGameInfo();
                GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
            }
            
        }
        showInterstitialMe++;
        saveLoadManagerScript.Saveload.showInterstitialMe = showInterstitialMe;
        saveLoadManagerScript.Saveload.saveGameInfo();
        UIManagerScript.Instance.retryAnimStop();
        sceneManagerScript.Instance.MainSceneLoad();
    }

    void LateUpdate()
    {
        if (GoogleMobileAdsDemoScript.Instance.isRewardedClosed)
        {
            int totalPickUpPoints = saveLoadManagerScript.Saveload.totalPickupPoints;
            totalPickUpPoints = totalPickUpPoints + 20;
            saveLoadManagerScript.Saveload.totalPickupPoints = totalPickUpPoints;
            saveLoadManagerScript.Saveload.saveGameInfo();
            GoogleMobileAdsDemoScript.Instance.isRewardedClosed = false;
            rewardPanel.SetActive(false);
            rewardSuccessPanel.SetActive(true);
        }
    }
    public void goToHome()
    {
        audioSourceRetry.PlayOneShot(retryClip[0]);
        if (!saveLoadManagerScript.Saveload.noAds)
        {
            GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
        }
        UIManagerScript.Instance.retryAnimStop();
        sceneManagerScript.Instance.homeSceneLoad();
    }
    public void rateUs()
    {
        audioSourceRetry.PlayOneShot(retryClip[0]);
        Application.OpenURL("market://details?id=com.TapAndTapGames.Stepping");
    }
    public void rewardFunction()
    {
        audioSourceRetry.PlayOneShot(retryClip[0]);
        GoogleMobileAdsDemoScript.Instance.ShowRewardBasedVideo();

    }
    public void showRewardSuccess()
    {
        audioSourceRetry.PlayOneShot(retryClip[0]);
        rewardSuccessPanel.SetActive(false);
    }
    public void shareUs()
    {
        Application.CaptureScreenshot("retryMenuScreenShot.png");
        ShareImage(Application.persistentDataPath + "/retryMenuScreenShot.png", title, message);
    }
    public static void ShareImage(string imageFileName, string title, string message)
    {
         #if UNITY_ANDROID

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), title);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", imageFileName);
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);

            bool fileExist = fileObject.Call<bool>("exists");
            Debug.Log("File exist : " + fileExist);
            
            if (fileExist)
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
            currentActivity.Call("startActivity", jChooser);
        
        #endif
    }
   
   public void CheckAchievementsLeaderboard()
    {
        totalPickupPoints = saveLoadManagerScript.Saveload.totalPickupPoints;
        gamesPlayed = saveLoadManagerScript.Saveload.gamesPlayed;
        totalSteps = saveLoadManagerScript.Saveload.totalSteps;
        bestScore = saveLoadManagerScript.Saveload.bestScore;
        doubleJumped = saveLoadManagerScript.Saveload.doubleJumped;
        saveLoadManagerScript.Saveload.saveGameInfo();

        if (Social.localUser.authenticated)
        {
            Social.ReportScore(saveLoadManagerScript.Saveload.bestScore, SteppingGooglePlayServiceResources.leaderboard_best_steps, (bool success) => {

            });
            if (bestScore >= 25 && bestScore<=49)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_25_steps, 100.0f, (bool success) =>
                {
              
                });
            
            }
           if (bestScore >= 50 && bestScore<=74)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_50_steps, 100.0f, (bool success) =>
                {
               
                });
            
            }
            if (bestScore >= 75 && bestScore<=99)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_75_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
           if (bestScore >= 100 && bestScore<=249)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_100_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (bestScore >= 250 && bestScore<=499)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_250_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (bestScore >= 500 )
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_500_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
            if (doubleJumped >= 20 && doubleJumped <=39)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_20_double_jump, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (doubleJumped >= 40 && doubleJumped<=59)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_40_double_jump, 100.0f, (bool success) =>
                {
               
                });
            
            }
             if (doubleJumped >= 60 && doubleJumped <= 79)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_60_double_jump, 100.0f, (bool success) =>
                { 
               
                });
           
            }
             if (doubleJumped >= 80 && doubleJumped <= 99)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_80_double_jump, 100.0f, (bool success) =>
                {
                
                });
           
            }
             if (doubleJumped >= 100 && doubleJumped <= 249)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_100_double_jump, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (doubleJumped >= 250 && doubleJumped <= 499)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_250_double_jump, 100.0f, (bool success) =>
                {
               
                });
           
            }
             if (doubleJumped >= 500)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_500_double_jump, 100.0f, (bool success) =>
                {
                
                });
            
            }
            if (gamesPlayed >= 100 && gamesPlayed<=199)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_100_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (gamesPlayed >= 200 && gamesPlayed <= 299)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_200_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (gamesPlayed >= 300 && gamesPlayed <= 399)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_300_games, 100.0f, (bool success) =>
                {
               
                });
            
            }
             if (gamesPlayed >= 400 && gamesPlayed <= 499)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_400_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (gamesPlayed >= 500 && gamesPlayed <= 749)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_500_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (gamesPlayed >= 750 && gamesPlayed <= 999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_750_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (gamesPlayed >= 1000)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_1000_games, 100.0f, (bool success) =>
                {
                
                });
            
            }
            if (totalPickupPoints >= 25 && totalPickupPoints<=49)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_25_cubes, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalPickupPoints >= 50 && totalPickupPoints <= 74)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_50_cubes, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalPickupPoints >= 75 && totalPickupPoints <= 99)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_75_cubes, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalPickupPoints >= 100 && totalPickupPoints <= 249)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_100_cubes, 100.0f, (bool success) =>
                {
               
                });
            
            }
             if (totalPickupPoints >= 250 && totalPickupPoints <= 499)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_250_cubes, 100.0f, (bool success) =>
                {
               
                });
            
            }
             if (totalPickupPoints >= 500 && totalPickupPoints <= 749)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_500_cubes, 100.0f, (bool success) =>
                {
               
                });
           
            }
             if (totalPickupPoints >= 750 && totalPickupPoints <= 999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_750_cubes, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalPickupPoints >= 1000)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_1000_cubes, 100.0f, (bool success) =>
                {
                
                });
            
            }
            if (totalSteps >= 100 && totalSteps<=199)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_100_total_steps, 100.0f, (bool success) =>
                {
                
                });
           
            }
            if (totalSteps >= 200 && totalSteps <= 299)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_200_total_steps, 100.0f, (bool success) =>
                {
               
                });
            
            }
           if (totalSteps >= 300 && totalSteps <= 399)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_300_total_steps, 100.0f, (bool success) =>
                {
                
                });
           
            }
             if (totalSteps >= 400 && totalSteps <= 499)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_400_total_steps, 100.0f, (bool success) =>
                {
                
                });
           
            }
             if (totalSteps >= 500 && totalSteps <= 749)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_500_total_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalSteps >= 750 && totalSteps <= 999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_750_total_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
            if (totalSteps >= 1000 && totalSteps<=1999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_1000_total_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalSteps >= 2000 && totalSteps<=2999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_2000_total_steps, 100.0f, (bool success) =>
                {
                
                });
           
            }
             if (totalSteps >= 3000 && totalSteps <= 3999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_3000_total_steps, 100.0f, (bool success) =>
                {

                });

            }
             if (totalSteps >= 4000 && totalSteps <= 4999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_4000_total_steps, 100.0f, (bool success) =>
                {

                });

            }
             if (totalSteps >= 5000 && totalSteps<=9999)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_5000_total_steps, 100.0f, (bool success) =>
                {
                
                });
            
            }
             if (totalSteps >= 10000)
            {
                Social.ReportProgress(SteppingGooglePlayServiceResources.achievement_10000_total_steps, 100.0f, (bool success) =>
                {

                });

            }

        }      
    }
}
   
