using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class playerScript : MonoBehaviour {
    private Rigidbody rbody;

    public float moveSpeed;
    public float jumpSpeed;
    public bool onGround;
    public bool startGround;
    public bool isDead;
    public bool canDoubleJump;
    public bool inoutCollision;
    public bool OnlyOnce;
    private bool isTouchDevice=false;
    public int score;
    private int pickupScore;
    public int nextChangePlatformColor;
    public bool doubleTap;
    public int doubleJumped;
    public int totalDoubleJumped;
    private int bestScore;
    private int gamesPlayed;
    public int totalPickupPoints;
    public int totalSteps;
    public int showReward;
    public float checkDownFall;
    public Vector3 lastPosition;

    public ParticleSystem ps;
    public ParticleSystem ps1;
    public ParticleSystem bestParticleSystem;
    public ParticleSystem retryParticleSystem;

    public GameObject retryPanel;
    public GameObject bestText;
    public Text scoreText;
    public Text scoreTextRetryMenu;
    public Text pickupText;
    public Text pickUpTextRetryMenu;
    public Text bestScoreRetryMenu;
    public GameObject bestShowPanel;
    public Text labelScoreText;
    public Image pickUpImage;

    public Camera mainCam;
    public Light directionlLight;

    private AudioSource audioSource;
    public AudioClip[] audioClip;

    private static playerScript instance;

    public static playerScript Instance
    {
        get
        {   if (instance == null)
                instance = FindObjectOfType<playerScript>();
            return instance;
        }

        set
        {
            instance = value;
        }
    }


    void Awake()
    {
        #if UNITY_ANDROID
                isTouchDevice = true;
#elif UNITY_IOS
                isTouchDevice=true;
                debug.log("IOS DEVICE");
#else
                isTouchDevice=false;
#endif
        if (!saveLoadManagerScript.Saveload.noAds)
        {
           // GoogleMobileAdsDemoScript.Instance.RequestBanner();
            GoogleMobileAdsDemoScript.Instance.RequestInterstitial();     
        }
        saveLoadManagerScript.Saveload.loadGameInfo();
        bestScore = saveLoadManagerScript.Saveload.bestScore;
        totalPickupPoints = saveLoadManagerScript.Saveload.totalPickupPoints;
        totalDoubleJumped = saveLoadManagerScript.Saveload.doubleJumped;
        totalSteps = saveLoadManagerScript.Saveload.totalSteps;
        gamesPlayed = saveLoadManagerScript.Saveload.gamesPlayed;
        gamesPlayed += 1;
        saveLoadManagerScript.Saveload.gamesPlayed = gamesPlayed;
        saveLoadManagerScript.Saveload.isFirst = false;
        saveLoadManagerScript.Saveload.saveGameInfo();    
    }
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        mainCam.GetComponent<cameraScript>().enabled = true;
        directionlLight.GetComponent<directionalLightScript>().enabled = true;
        isDead = false;
        startGround = true;
        canDoubleJump = false;
        doubleTap = false;
        inoutCollision = false;
        OnlyOnce = true;
        score =0;
        nextChangePlatformColor = 15;
        pickupScore = 0;
        bestText.SetActive(false);
        scoreText.enabled = true;
        pickupText.enabled = true;
        pickUpImage.enabled = true;
        bestShowPanel.SetActive(false);
        retryPanel.SetActive(false);
        retryParticleSystem.gameObject.SetActive(false);
        bestParticleSystem.gameObject.SetActive(false);
        scoreText.text = score.ToString();
        pickupText.text = pickupScore.ToString();
        audioSource = GetComponent<AudioSource>();
       
       
        showReward = UnityEngine.Random.Range(0, 5);    

        if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            audioSource.volume = 0;
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            audioSource.volume = 1;
        }
        
    }

    void Update()
    {
        if (isTouchDevice)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, moveSpeed);
            if (Input.GetMouseButtonUp(0) && !isDead)
            {
                if (onGround || startGround)
                {
                    rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed,rbody.velocity.z);
                    canDoubleJump = true;
                    startGround = false;
                    doubleTap = false;
                    inoutCollision = true;
                    audioSource.PlayOneShot(audioClip[2]);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed,rbody.velocity.z/1.16f);
                        canDoubleJump = false;
                        doubleTap = true;
                        inoutCollision = true;
                        audioSource.PlayOneShot(audioClip[2]);
                    }
                }
            }
        }
        /*else
        {
            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, moveSpeed);
            if (Input.GetKeyDown(KeyCode.Space) && !isDead)
            {
                if (onGround || startGround)
                {
                    rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed,rbody.velocity.z);
                    canDoubleJump = true;
                    startGround = false;
                    doubleTap = false;
                    inoutCollision = true;
                    audioSource.PlayOneShot(audioClip[2]);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed, rbody.velocity.z/1.175f);
                        audioSource.PlayOneShot(audioClip[2]);
                        canDoubleJump = false;
                        doubleTap = true;
                        inoutCollision = true;
                        audioSource.PlayOneShot(audioClip[2]);
                    }
                }
            }
        }*/
    }
    void FixedUpdate()
    {
        
        if (score >= nextChangePlatformColor)
        {
            changePlatformMaterialColorScript.Instance.changeMaterial();
            nextChangePlatformColor += 20;
        }
        if ((checkDownFall - 5.0f) > transform.position.y && OnlyOnce)
        {
            isDead = true;
            OnlyOnce = false;
            audioSource.PlayOneShot(audioClip[1]);
            gameObject.transform.DetachChildren();
            mainCam.GetComponent<cameraScript>().enabled = false;
            directionlLight.GetComponent<directionalLightScript>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(gameObjectDisable());
            retryFunction();
        }
        if (lastPosition == transform.position && OnlyOnce)
        {
            isDead = true;
            OnlyOnce = false;
            audioSource.PlayOneShot(audioClip[0]);
            gameObject.transform.DetachChildren();
            mainCam.GetComponent<cameraScript>().enabled = false;
            directionlLight.GetComponent<directionalLightScript>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(gameObjectDisable());
            Instantiate(ps1, transform.position, Quaternion.identity);
            retryFunction();
        }
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
            foreach (ContactPoint contact in collision.contacts)
              {
                  if (contact.normal == -Vector3.forward && OnlyOnce)
                  {
                       isDead = true;
                      OnlyOnce = false;
                      audioSource.PlayOneShot(audioClip[0]);
                      gameObject.transform.DetachChildren();
                      mainCam.GetComponent<cameraScript>().enabled = false;
                      directionlLight.GetComponent<directionalLightScript>().enabled = false;
                      gameObject.GetComponent<MeshRenderer>().enabled = false;
                      StartCoroutine(gameObjectDisable());
                      Instantiate(ps1, transform.position, Quaternion.identity);
                  }
            }
            if (!isDead)
            {
                if (inoutCollision)
                {
                    if (!doubleTap)
                    {
                        score = score + 1;
                        totalSteps += 1;
                        inoutCollision = false;
                    }
                    else
                    {
                        score = score + 2;
                        totalSteps += 2;
                        totalDoubleJumped += 1;
                        doubleTap = false;
                        inoutCollision = false;
                    }
                    scoreText.text = score.ToString();
                }
            }
            else
            {
                StartCoroutine(gameObjectDisable());
                retryFunction();
            }

        }
    }

    void OnCollisionStay(Collision col)
    { 
        if (col.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision col1)
    {
        if (col1.collider.tag == "Ground")
        {
            onGround = false;
            checkDownFall = transform.position.y;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            if (!isDead)
            {
                other.gameObject.SetActive(false);
                audioSource.PlayOneShot(audioClip[3]);
                Instantiate(ps, other.transform.position, Quaternion.identity);
                pickupScore += 1;
                totalPickupPoints += 1;
                pickupText.text = pickupScore.ToString();
            }
        }
    }

   void OnTriggerExit(Collider other1)
    {
        if (other1.tag == "GameOverFall" && OnlyOnce)
        {
            isDead = true;
            OnlyOnce = false;
            audioSource.PlayOneShot(audioClip[1]);
            gameObject.transform.DetachChildren();
            mainCam.GetComponent<cameraScript>().enabled = false;
            directionlLight.GetComponent<directionalLightScript>().enabled = false;
            StartCoroutine(gameObjectDisable());
            retryFunction();
        }
    }
    private void retryFunction()
    {
        
        scoreText.enabled = false;
        pickupText.enabled = false;
        pickUpImage.enabled = false;
        bestShowPanel.SetActive(false);
        labelScoreText.enabled = false;
        retryPanel.SetActive(true);
        retryParticleSystem.gameObject.SetActive(true);
        retryParticleSystem.Play();
        scoreTextRetryMenu.text = score.ToString();
        pickUpTextRetryMenu.text = pickupScore.ToString();
        UIManagerScript.Instance.retryAnimPlay();

        saveLoadManagerScript.Saveload.totalPickupPoints = totalPickupPoints;
        saveLoadManagerScript.Saveload.doubleJumped = totalDoubleJumped;
        saveLoadManagerScript.Saveload.totalSteps = totalSteps;
        saveLoadManagerScript.Saveload.saveGameInfo();

            if (showReward == 0 || showReward == 1)
            {
                gameRetryScript.Instance.rewardPanel.SetActive(true);
            }
       
        if (score >bestScore)
        {
            bestScoreRetryMenu.text = score.ToString();
            saveLoadManagerScript.Saveload.bestScore = score;
            saveLoadManagerScript.Saveload.saveGameInfo();
            bestText.SetActive(true);
            bestParticleSystem.gameObject.SetActive(true);
            bestParticleSystem.Play();
        }
        else
        {
            bestScoreRetryMenu.text = bestScore.ToString();
            saveLoadManagerScript.Saveload.saveGameInfo();
        }
        gameRetryScript.Instance.CheckAchievementsLeaderboard();
        saveLoadManagerScript.Saveload.loadGameInfo();
        saveLoadManagerScript.Saveload.saveGameInfo();
    }

    IEnumerator gameObjectDisable()
        {
            yield return new WaitForSeconds(.76f);
            gameObject.SetActive(false);
        }
    IEnumerator bestShowPanelDisable()
    {
        yield return new WaitForSeconds(2f);
        bestShowPanel.SetActive(false);
    }

    /*public void labelChangeScoreText()
    {
        if (score == 25)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "WELL DONE!! KEEP GOING";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 50)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "SHOWING PROWESS";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 75)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "EXCELLENT!! DOMINATING!!";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 100)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "MASTERED!!";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 150)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "ONE WORD: AWESOME!!";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 200)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "PEERLESS$$$$";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 250)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "UNPRECEDENTED$$$$";
            StartCoroutine(bestShowPanelDisable());
        }
        else if (score == 300)
        {
            bestShowPanel.SetActive(true);
            labelScoreText.text = "TAKE A BOW";
            StartCoroutine(bestShowPanelDisable());
        }
    }*/
}
