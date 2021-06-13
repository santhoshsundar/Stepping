using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textManager : MonoBehaviour {
    private static textManager instance;

    public GameObject textPrefab;
    public RectTransform canvasTransform;
    public float speed;
    public Vector3 direction;
    public float fadeTime;
    public static textManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<textManager>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void createText(Vector3 position,string text)
    {
       GameObject st=(GameObject) Instantiate(textPrefab, position, Quaternion.identity);
        st.transform.SetParent(canvasTransform);
        //st.GetComponent<text>().localScale = new Vector3(1, 1, 1);
        st.transform.localScale = new Vector3(.1f, .1f, .1f);
        st.GetComponent<textScript>().Initialize(speed, direction,fadeTime);
        st.GetComponent<Text>().text = text;
    }
}
