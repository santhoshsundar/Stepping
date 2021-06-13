using UnityEngine;
using System.Collections;

public class particleScript : MonoBehaviour {
    private ParticleSystem ps0;
	// Use this for initialization
	void Start () {
        ps0 = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ps0.isPlaying)
        {
            Destroy(gameObject);
        }
	
	}
}
