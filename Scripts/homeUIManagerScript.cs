using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class homeUIManagerScript : MonoBehaviour {

    public Animator TapToPlayAnim;
    private AudioSource audioSource;
    public Sprite[] audioSprites;
    public Button AudioButton;
    public AudioClip buttonClip;

    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    public string TwitterNameParameter = "Check this amazing Stepping Game";
    public string TwitterDescriptionParameter = "";
    private string PlayStoreAddress = "https://play.google.com/store/apps/details?id=com.TapAndTapGames.Stepping";

    public bool isConnectedToGoogle { set; get; }

    void Awake()
    {
       audioSource = GetComponent<AudioSource>();
    }

    void Start ()
    {
        if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            AudioButton.GetComponent<Image>().sprite = audioSprites[1];
            audioSource.volume = 0;
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            AudioButton.GetComponent<Image>().sprite = audioSprites[0];
            audioSource.volume = 1;
        }
        if (saveLoadManagerScript.Saveload.isFirst)
        {
            saveLoadManagerScript.Saveload.deleteOnLoad();
            saveLoadManagerScript.Saveload.setGameInfo();
            saveLoadManagerScript.Saveload.loadGameInfo();
            saveLoadManagerScript.Saveload.saveGameInfo();
        }
        audioSource.Play();
        TapToPlayAnimPlay();
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = false;
        connectToGoogleServices();
	}
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
                Application.Quit();       
        }
    }
    public void connectToGoogleServices()
    {
        if (!isConnectedToGoogle)
        {
            Social.localUser.Authenticate((bool success) => {
                isConnectedToGoogle = success;
            });
        }
    }

    public void TapToPlayAnimPlay()
    {
        TapToPlayAnim.SetBool("isBegin", true);
    }

    public void TapToPlayAnimStop()
    {
        TapToPlayAnim.SetBool("isBegin", false);
    }

    public void MuteUnmute()
    {
        
        if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            saveLoadManagerScript.Saveload.audioOnOff = 0;
            saveLoadManagerScript.Saveload.saveGameInfo();
            AudioButton.GetComponent<Image>().sprite = audioSprites[1];
            audioSource.volume = 0; 
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            saveLoadManagerScript.Saveload.audioOnOff = 1;
            saveLoadManagerScript.Saveload.saveGameInfo();
            AudioButton.GetComponent<Image>().sprite = audioSprites[0];
            audioSource.volume = 1;
        }
        audioSource.PlayOneShot(buttonClip);
    }

    public void shopMenuFunction()
    {
        audioSource.PlayOneShot(buttonClip);
        sceneManagerScript.Instance.MenuSceneLoad(); 
    }

    public void HelpFunction()
    {
        audioSource.PlayOneShot(buttonClip);
        sceneManagerScript.Instance.HelpSceneLoad();
    }

    public void GoToMainMenu()
    {
        if (saveLoadManagerScript.Saveload.isFirst)
        {
            audioSource.PlayOneShot(buttonClip);
            sceneManagerScript.Instance.HelpSceneLoad();
            saveLoadManagerScript.Saveload.isFirst = false;
            saveLoadManagerScript.Saveload.saveGameInfo();
        }
        else
        {
            audioSource.PlayOneShot(buttonClip);
            sceneManagerScript.Instance.MainSceneLoad();
        }
    }

    public void NoAds()
    {
        audioSource.PlayOneShot(buttonClip);
        Purchaser.Instance.BuyNoAds();
    }

    public void Achievements()
    {
        audioSource.PlayOneShot(buttonClip);
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }

    public void LeaderBoards()
    {
        audioSource.PlayOneShot(buttonClip);
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void shareToTwitter()
    {
        audioSource.PlayOneShot(buttonClip);
        Application.OpenURL(TWITTER_ADDRESS + "?text=" +
            WWW.EscapeURL(TwitterNameParameter + "\n" + TwitterDescriptionParameter + "\n" + PlayStoreAddress)+
            "&amp;lang=" + WWW.EscapeURL(TWEET_LANGUAGE));
    }

    public void shareOnFacebook()
    {
        audioSource.PlayOneShot(buttonClip);
        facebookManagerScript.Instance.shareOnFacebookCall();
    }
}
