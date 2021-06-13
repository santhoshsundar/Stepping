using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class platformManagerScript : MonoBehaviour {
    public int randomIndex;
    public int longSpawn;
    public int currentIndex;

    public GameObject[] platformPrefab;
    public GameObject currentPlatform;
    //private Stack<GameObject> backwardPlatform=new Stack<GameObject>();
    private Stack<GameObject> forwardPlatform = new Stack<GameObject>();
    private Stack<GameObject> stationaryPlatform = new Stack<GameObject>();
    private Stack<GameObject> mediumstationaryPlatform = new Stack<GameObject>();
    private Stack<GameObject> scalingPlatform = new Stack<GameObject>();
    private Stack<GameObject> longstationaryPlatform = new Stack<GameObject>();
   

    private static platformManagerScript instance;

    public static platformManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<platformManagerScript>();
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

  /*  public Stack<GameObject> BackwardPlatform
    {
        get
        {
            return backwardPlatform;
        }

        set
        {
            backwardPlatform = value;
        }
    }*/

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

    public Stack<GameObject> ScalingPlatform
    {
        get
        {
            return scalingPlatform;
        }

        set
        {
            scalingPlatform = value;
        }
    }

    public Stack<GameObject> LongstationaryPlatform
    {
        get
        {
            return longstationaryPlatform;
        }

        set
        {
            longstationaryPlatform = value;
        }
    }

    public Stack<GameObject> MediumstationaryPlatform
    {
        get
        {
            return mediumstationaryPlatform;
        }

        set
        {
            mediumstationaryPlatform = value;
        }
    }

    void Start () {
        longSpawn = 25;
        currentIndex = 0;
        createPlatform(5);
        LongPlatformSpawn();

        for (int i=0;i<30;i++)
        { 
            spawnPlatform();    
        }
	}
	
	void Update () {
	
	}
    public void LongPlatformSpawn()
    {
        LongstationaryPlatform.Push(Instantiate(platformPrefab[3]));
        LongstationaryPlatform.Peek().name = "longstationaryPlatform";
        LongstationaryPlatform.Peek().SetActive(false);
    }
    public void createPlatform(int amount)
    {
        for(int i = 0; i< amount; i++)
        {
            //BackwardPlatform.Push(Instantiate(platformPrefab[0]));
            ForwardPlatform.Push(Instantiate(platformPrefab[0]));
            StationaryPlatform.Push(Instantiate(platformPrefab[1]));
            MediumstationaryPlatform.Push(Instantiate(platformPrefab[4]));
            ScalingPlatform.Push(Instantiate(platformPrefab[2]));
           
            //BackwardPlatform.Peek().name = "backwardPlatform";
            ForwardPlatform.Peek().name = "forwardPlatform";
            StationaryPlatform.Peek().name = "stationaryPlatform";
            ScalingPlatform.Peek().name = "scalingPlatform";
            MediumstationaryPlatform.Peek().name = "mediumstationaryPlatform";
            
            //BackwardPlatform.Peek().SetActive(false);
            ForwardPlatform.Peek().SetActive(false);
            StationaryPlatform.Peek().SetActive(false);
            ScalingPlatform.Peek().SetActive(false);
            MediumstationaryPlatform.Peek().SetActive(false);
            
           
        }
    }

    public void spawnPlatform()
    {
       
        if(forwardPlatform.Count==0||
           scalingPlatform.Count==0||
            stationaryPlatform.Count == 0 || MediumstationaryPlatform.Count==0)
        {
            createPlatform(2);
        }

        if (longstationaryPlatform.Count == 0)
        {
            LongPlatformSpawn();
        }

          if (currentIndex == longSpawn)
        {
            GameObject temp = LongstationaryPlatform.Pop();
            temp.SetActive(true);
            temp.transform.position = currentPlatform.transform.GetChild(2).position + new Vector3(0,0,1.85f);
            currentPlatform = temp;
            longSpawn += 25;
            currentIndex += 1;
        }
        else
        {
            randomIndex = UnityEngine.Random.Range(0, 4);
             if (randomIndex == 0)
            {
                GameObject temp = ForwardPlatform.Pop();
                temp.SetActive(true);
                temp.transform.position = currentPlatform.transform.GetChild(2).position;
                currentPlatform = temp;
                currentIndex += 1;
            }
            else if (randomIndex == 1)
            {
                GameObject temp = StationaryPlatform.Pop();
                temp.SetActive(true);
                temp.transform.position = currentPlatform.transform.GetChild(2).position;
                currentPlatform = temp;
                currentIndex += 1;
            }
            else if (randomIndex == 2)
            {
                GameObject temp = ScalingPlatform.Pop();
                temp.SetActive(true);
                temp.transform.position = currentPlatform.transform.GetChild(2).position;
                currentPlatform = temp;
                currentIndex += 1;
            }
            else if (randomIndex == 3)
            {
                GameObject temp = MediumstationaryPlatform.Pop();
                temp.SetActive(true);
                temp.transform.position = currentPlatform.transform.GetChild(2).position;
                currentPlatform = temp;
                currentIndex += 1;
            }
            int randomSpawn = UnityEngine.Random.Range(0, 10);
            if (randomSpawn == 0)
            {
                currentPlatform.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
}
