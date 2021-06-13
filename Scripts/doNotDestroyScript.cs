using UnityEngine;
using System.Collections;

public class doNotDestroyScript : MonoBehaviour {

    public Texture[] backgroundImage;
    private static int i;
    public Material backGroundImage;
	void Awake()
    {
        DontDestroyOnLoad(this);
    }
	void Start()
    {
        if (FindObjectsOfType<doNotDestroyScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        i = Random.Range(0, 6);
        backGroundImage.SetTexture("_MainTex", backgroundImage[i]);
    }
}
