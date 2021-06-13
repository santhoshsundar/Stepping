using UnityEngine;
using System.Collections;

public class platformScalingScript : MonoBehaviour {
    private Vector3 scale1;
    private Vector3 scale2;
    public bool Switch;
    public float speed;
    // Use this for initialization
    void Start()
    {
        scale2 = new Vector3(5, .75f, 3.5f);
        scale1 = new Vector3(5, .75f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale == scale2)
        {
            Switch = true;

        }
        if (transform.localScale == scale1)
        {
            Switch = false;

        }
        if (!Switch)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,scale2, Time.deltaTime / speed);

        }

        if (Switch)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale,scale1, Time.deltaTime / speed);

        }
    }
}
