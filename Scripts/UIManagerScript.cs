using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManagerScript : MonoBehaviour {
   
    public Animator RetryPanelAnim;

    private static UIManagerScript instance;

    public static UIManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManagerScript>();
                
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }


    public void retryAnimPlay()
    {
        RetryPanelAnim.SetBool("isOver", true);
        
    }

    public void retryAnimStop()
    {
        RetryPanelAnim.SetBool("isOver", false);
    }
}
