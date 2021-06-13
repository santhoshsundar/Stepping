using UnityEngine;
using System.Collections;

public class platformMovingScript : MonoBehaviour {

    public Transform platform;
    public Transform startPlatform;
    public Transform endPlatform;
    public float speed;
    public bool Switch = false;
    private static platformMovingScript instance;

    public static platformMovingScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<platformMovingScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void FixedUpdate()
    {
        if (platform.position == endPlatform.position)
        {
            StartCoroutine(stopPlatform1());
        }
        if (platform.position == startPlatform.position)
        {
            StartCoroutine(stopPlatform());
        }
        if (Switch)
        {
            platform.position = Vector3.MoveTowards(platform.position, startPlatform.position, speed * Time.fixedDeltaTime);
        }
        else
        {
            platform.position = Vector3.MoveTowards(platform.position, endPlatform.position, speed * Time.fixedDeltaTime);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPlatform.position, platform.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPlatform.position, platform.localScale);
    }
    IEnumerator stopPlatform()
    {
        yield return new WaitForSeconds(1.0f);
        Switch = false;
    }
    IEnumerator stopPlatform1()
    {
        yield return new WaitForSeconds(1.0f);
        Switch = true;
    }

}
