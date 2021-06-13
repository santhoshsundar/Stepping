using UnityEngine;
using System.Collections;

public class changePlatformMaterialColorScript : MonoBehaviour {
    public Texture[] texture;
    private static int i;
    private Renderer rend;
    private static changePlatformMaterialColorScript instance;

    public static changePlatformMaterialColorScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<changePlatformMaterialColorScript>();

            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        i = Random.Range(0, 8);
        rend.sharedMaterial.SetTexture("_MainTex",texture[i]);
        
    }
    public void changeMaterial()
    {
        i++;
        Debug.Log(i);
        rend.sharedMaterial.SetTexture("_MainTex", texture[i]);
        //i++;
        Debug.Log(i);
        if (i >= texture.Length-1)
            i = 0;
    }
}
