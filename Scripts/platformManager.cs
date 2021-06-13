using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class platformManager : MonoBehaviour {
    public GameObject[] platformPrefab;
    public GameObject currentPlatform;
    public int randomIndex;
    private static platformManager instance;
    private Stack<GameObject> backwardPlatform=new Stack<GameObject>();
    private Stack<GameObject> forwardPlatform = new Stack<GameObject>();
    private Stack<GameObject> stationaryPlatform = new Stack<GameObject>();
    private Stack<GameObject> smallBackwardPlatform = new Stack<GameObject>();
    private Stack<GameObject> smallForwardPlatform = new Stack<GameObject>();
    private Stack<GameObject> smallStationaryPlatform = new Stack<GameObject>();
   
    public static platformManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<platformManager>();
            }
            return instance;
        }
    }

    public Stack<GameObject> ForwardPlatform
    {
        get
        {
            return forwardPlatform;
        }

        set
        {
            forwardPlatform = value;
        }
    }

    public Stack<GameObject> BackwardPlatform
    {
        get
        {
            return backwardPlatform;
        }

        set
        {
            backwardPlatform = value;
        }
    }

    public Stack<GameObject> StationaryPlatform
    {
        get
        {
            return stationaryPlatform;
        }

        set
        {
            stationaryPlatform = value;
        }
    }

    public Stack<GameObject> SmallBackwardPlatform
    {
        get
        {
            return smallBackwardPlatform;
        }

        set
        {
            smallBackwardPlatform = value;
        }
    }

    public Stack<GameObject> SmallForwardPlatform
    {
        get
        {
            return smallForwardPlatform;
        }

        set
        {
            smallForwardPlatform = value;
        }
    }

    public Stack<GameObject> SmallStationaryPlatform
    {
        get
        {
            return smallStationaryPlatform;
        }

        set
        {
            smallStationaryPlatform = value;
        }
    }

    


    // Use this for initialization
    void Start () {
        createPlatform(50);
	for(int i=0;i<300;i++)
        { 
            spawnPlatform();
           
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void createPlatform(int amount)
    {
        for(int i = 0; i< amount; i++)
        {
            BackwardPlatform.Push(Instantiate(platformPrefab[0]));
            ForwardPlatform.Push(Instantiate(platformPrefab[1]));
            StationaryPlatform.Push(Instantiate(platformPrefab[2]));
            SmallBackwardPlatform.Push(Instantiate(platformPrefab[3]));
            SmallForwardPlatform.Push(Instantiate(platformPrefab[4]));
            SmallStationaryPlatform.Push(Instantiate(platformPrefab[5]));
            BackwardPlatform.Peek().name = "backwardPlatform";
            ForwardPlatform.Peek().name = "forwardPlatform";
            StationaryPlatform.Peek().name = "stationaryPlatform";
            SmallBackwardPlatform.Peek().name = "smallbackwardPlatform";
            SmallForwardPlatform.Peek().name = "smallforwardPlatform";
            SmallStationaryPlatform.Peek().name = "smallstationaryPlatform";
            BackwardPlatform.Peek().SetActive(false);
            ForwardPlatform.Peek().SetActive(false);
            StationaryPlatform.Peek().SetActive(false);
            SmallBackwardPlatform.Peek().SetActive(false);
            SmallForwardPlatform.Peek().SetActive(false);
            SmallStationaryPlatform.Peek().SetActive(false);
        }
    }
    public void spawnPlatform()
    {
        
        
        randomIndex = Random.Range(0, 7);
      
        if (randomIndex == 0)
        {
            GameObject temp = BackwardPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;
        }
        else if (randomIndex == 1)
        {
            GameObject temp = ForwardPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;

        }
        else if (randomIndex == 2)
        {
            GameObject temp = StationaryPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;

        }
        else if (randomIndex == 3)
        {
            GameObject temp = SmallStationaryPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;

        }
        else if (randomIndex == 4)
        {
            GameObject temp = SmallBackwardPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;

        }
        else if (randomIndex == 5)
        {
            GameObject temp = SmallForwardPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(0).transform.GetChild(0).position;
            currentPlatform = temp;
        }
        
        int randomSpawn = Random.Range(0, 15);
        if (randomSpawn == 0)
        {
            currentPlatform.transform.GetChild(1).gameObject.SetActive(true);
        }
        
        //currentPlatform = (GameObject)Instantiate(platformPrefab[randomIndex], currentPlatform.transform.GetChild(0).transform.GetChild(0).position, Quaternion.identity);

    }
}
