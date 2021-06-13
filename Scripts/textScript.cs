using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textScript : MonoBehaviour {
    private float speed;
    private Vector3 dir;
    private float fadeTime;
    private static textScript instance;

    public static textScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<textScript>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float translation = speed * Time.deltaTime;
        transform.Translate(dir * translation);
	}
    public void Initialize(float speed,Vector3 directior,float fadeTime)
    {
        this.speed = speed;
        this.dir = directior;
        this.fadeTime = fadeTime;
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float alpha = GetComponent<Text>().color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;
        while(progress<1.0)
        {
            Color tmpColor = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(alpha, 0, progress));
            progress += rate * Time.deltaTime;

            yield return null;
        }
        Destroy(gameObject);
        
    }
}
