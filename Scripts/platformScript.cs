using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {
    public Transform platform;
    public Transform startPlatform;
    public Transform endPlatform;
    public float speed;
    public bool Switch=false;
    private static platformScript instance;

    public static platformScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<platformScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void FixedUpdate ()
    {
        if (platform.position == endPlatform.position)
        {
            Switch = true;
        }
        if (platform.position == startPlatform.position)
        {
            Switch = false;
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
}
