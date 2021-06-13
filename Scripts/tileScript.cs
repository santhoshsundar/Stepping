using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour
{
    private float fallDelay = 2f;

   void OnTriggerEnter(Collider other2)
    {
        if (other2.tag == "Player")
        {
           if (gameObject.transform.childCount == 7)
            {
                if (gameObject.transform.GetChild(5).name == "movingPlatform")
                {
                    gameObject.transform.GetChild(5).GetComponent<platformScript>().speed = 0.0f;
                }
            }        
        }      
    }
  void OnTriggerExit(Collider other3)
    {
        if (other3.tag == "Player")
        {
            if (gameObject.transform.childCount == 7)
            {
                if (gameObject.transform.GetChild(5).name == "movingPlatform")
                {
                    platformManagerScript.Instance.spawnPlatform();
                    StartCoroutine(fallDown());
                }
            }
            else
            {
                platformManagerScript.Instance.spawnPlatform();
                StartCoroutine(fallDown());
            }
        }
    }
    IEnumerator fallDown()
    {
        yield return new WaitForSeconds(fallDelay);
        switch (gameObject.name)
        {
            case "forwardPlatform":
                platformManagerScript.Instance.ForwardPlatform.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.transform.GetChild(5).GetComponent<platformScript>().speed = 2.3f;
                gameObject.SetActive(false);
                break;
            case "stationaryPlatform":
                platformManagerScript.Instance.StationaryPlatform.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
            case "mediumstationaryPlatform":
                platformManagerScript.Instance.MediumstationaryPlatform.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
            case "scalingPlatform":
                platformManagerScript.Instance.ScalingPlatform.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
            case "longstationaryPlatform":
                platformManagerScript.Instance.LongstationaryPlatform.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
