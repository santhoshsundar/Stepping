using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class GoogleMobileAdsDemoScript : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    public bool isRewardedClosed;
    private static GoogleMobileAdsDemoScript instance;

    public static GoogleMobileAdsDemoScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GoogleMobileAdsDemoScript>();

            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<GoogleMobileAdsDemoScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        this.RequestBanner();
        this.RequestInterstitial();
        isRewardedClosed = false;
    }

    public void Start()
    {
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();
    }
    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }

    public void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
        #if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-7593849644911740/7178900119";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdLoaded += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }
   
    public void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
        #if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-7593849644911740/1132366516";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void RequestRewardBasedVideo()
    {
        #if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-7593849644911740/7039299311";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_AD_UNIT_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
    }

    public void ShowBanner()
    {
        this.bannerView.Show();
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial is not ready yet");
        }
    }

   public void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
        else
        {
            MonoBehaviour.print("Reward based video ad is not ready yet");
        }
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
        //RequestBanner();
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        //bannerView.Destroy();
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received.");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        //interstitial.Destroy();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion
  
    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        isRewardedClosed = true;
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
  
}
