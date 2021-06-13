using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour {
    
    
    private static sceneManagerScript instance;

    public static sceneManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<sceneManagerScript>();

            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    public void homeSceneLoad()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void MainSceneLoad()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void MenuSceneLoad()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void HelpSceneLoad()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
