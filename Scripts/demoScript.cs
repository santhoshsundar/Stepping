using UnityEngine;
using System.Collections;

public class demoScript : MonoBehaviour
{
    private Rigidbody rbody;
    public float moveSpeed;
    public float jumpSpeed;
    private int stop;
    public bool doubleJump;
    public bool nextdoubleTap;
    public bool onGround;
    public Animator singleTouch;
    public AudioSource audioSourceHelp;
    public AudioClip hClip;
    void Awake()
    {
        audioSourceHelp = GetComponent<AudioSource>();

        if (!saveLoadManagerScript.Saveload.noAds)
        {
            //GoogleMobileAdsDemoScript.Instance.RequestBanner();
           GoogleMobileAdsDemoScript.Instance.RequestInterstitial();
        }
    }
    void Start()
    {
        if (saveLoadManagerScript.Saveload.audioOnOff == 0)
        {
            audioSourceHelp.volume = 0;
        }
        else if (saveLoadManagerScript.Saveload.audioOnOff == 1)
        {
            audioSourceHelp.volume = 1;
        }
        rbody = GetComponent<Rigidbody>();
        stop = 27;
        doubleJump = false;
        nextdoubleTap = false;
    }

    void Update()
    {
        rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, moveSpeed);

        if (transform.position.z >= stop)
        {
            Time.timeScale = 0;
            singleTouch.SetBool("isTouch",true);
            stop = 100;
        }

        if (Input.GetMouseButtonUp(0))
        {
            singleTouch.SetBool("isTouch", false);
            if (onGround || !doubleJump)
            {
                Time.timeScale = 1;
                rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed, rbody.velocity.z);
                doubleJump = true;
                if (nextdoubleTap)
                {
                    StartCoroutine(Stop1());
                }
                else
                {
                    StartCoroutine(Stop());
                    nextdoubleTap = true;
                }
            }

            else
            {
                if (doubleJump)
                {
                    Time.timeScale = 1;
                    rbody.velocity = new Vector3(rbody.velocity.x, jumpSpeed, rbody.velocity.z);
                    doubleJump = false;
                    nextdoubleTap = false;
                    StartCoroutine(Stop());
                }
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Demo")
        {
            onGround = true;
            Debug.Log("ground");
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.collider.tag == "Demo")
        {
            onGround = true;
            Debug.Log("ground");
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Demo")
        {
            onGround = false;
            Debug.Log("no ground");
        }
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1.02f);
        Time.timeScale = 0;
        //yield return new WaitForSeconds(.2f);
        singleTouch.SetBool("isTouch", true);
    }
    IEnumerator Stop1()
    {
        yield return new WaitForSeconds(.52f);
        Time.timeScale = 0;
        //yield return new WaitForSeconds(.2f);
        singleTouch.SetBool("isTouch", true);
    }
    public void  mainMenuFunction()
    {
        audioSourceHelp.PlayOneShot(hClip);
        if (!saveLoadManagerScript.Saveload.noAds) { GoogleMobileAdsDemoScript.Instance.ShowInterstitial(); }
        sceneManagerScript.Instance.homeSceneLoad();
    }
}