using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class homeMenuManagerScript : MonoBehaviour {


    public GameObject ContentPanel;
    public GameObject ShopButtonPrefab;
    public Text headerTotalPickupPoints;
    public Texture[] ballTexture;
    public Material playerMaterial;

    public AudioSource audio1;
    public AudioClip[] bClip;

    private int[] costs = { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100,100,100,100,100,100,100,100,100,100,100 };
    void Awake()
    {
        ballTexture = Resources.LoadAll<Texture>("BallTextures");
        audio1 = GetComponent<AudioSource>();
        if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            audio1.volume = 0;
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            audio1.volume = 1;
        }
        if (!saveLoadManagerScript.Saveload.noAds)
        {
           // GoogleMobileAdsDemoScript.Instance.RequestBanner();
            GoogleMobileAdsDemoScript.Instance.RequestInterstitial();
        }
    }
    void Start () {
        
        headerTotalPickupPoints.GetComponent<Text>().text = saveLoadManagerScript.Saveload.totalPickupPoints.ToString();
        createBallTexture();
    }
	
	void Update () {
	
	}
    private void createBallTexture()
    {
        Sprite[] textures = Resources.LoadAll<Sprite>("BallTextures1");
        int i = 0;
        foreach (Sprite texture in textures)
        {   
            GameObject Container = Instantiate(ShopButtonPrefab) as GameObject;
            Container.GetComponent<Image>().sprite = texture;
            Container.transform.SetParent(ContentPanel.transform, false);
            Container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text=costs[i].ToString();
            int index = i;
            Container.GetComponent<Button>().onClick.AddListener(() => changeBallTexture(index));
            if ((saveLoadManagerScript.Saveload.ballTextureAvailable & 1 << index) == 1 << index)
            {
                Container.transform.GetChild(0).gameObject.SetActive(false);
            }
            i++;
        }

    }

    private void changeBallTexture(int index)
    {
        if ((saveLoadManagerScript.Saveload.ballTextureAvailable & 1 << index) == 1 << index)
        {
            playerMaterial.SetTexture("_MainTex", ballTexture[index]);
            saveLoadManagerScript.Saveload.currentBallTextureIndex = index;
            saveLoadManagerScript.Saveload.saveGameInfo();
            audio1.PlayOneShot(bClip[1]);
        }
        else
        {
            int cost = costs[index];
            if (saveLoadManagerScript.Saveload.totalPickupPoints >= cost)
            {
                saveLoadManagerScript.Saveload.totalPickupPoints -= cost;
                saveLoadManagerScript.Saveload.ballTextureAvailable += 1 << index;
                
                saveLoadManagerScript.Saveload.currentBallTextureIndex = index;
                saveLoadManagerScript.Saveload.saveGameInfo();
                ContentPanel.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                changeBallTexture(index);
                headerTotalPickupPoints.GetComponent<Text>().text = saveLoadManagerScript.Saveload.totalPickupPoints.ToString();
                audio1.PlayOneShot(bClip[2]);
            }
            else
            {
                audio1.PlayOneShot(bClip[0]);
            }
        }
    }
    public void backToHome()
    {
        audio1.PlayOneShot(bClip[1]);
        if (!saveLoadManagerScript.Saveload.noAds)
        {
            GoogleMobileAdsDemoScript.Instance.ShowInterstitial();
        }
        sceneManagerScript.Instance.homeSceneLoad();
    }
}
