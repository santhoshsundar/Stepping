using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;

public class facebookManagerScript : MonoBehaviour
{
    private static facebookManagerScript instance;

    public static facebookManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<facebookManagerScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }
    
    public void shareOnFacebookCall()
    {
        FB.ShareLink(
            contentURL:new System.Uri("http://play.google.com/store/apps/details?id=com.TapAndTapGames.Stepping"), 
            contentTitle: "Stepping Game",
            contentDescription: "Here is a link to Download Stepping Game from Android Play Store.",
            callback: ShareCallback);
    }
    private void ShareCallback(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink Error: " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("ShareLink success!");
        }
    }
}
