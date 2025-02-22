using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> Front;

    public Transform BehindText;
    public List<GameObject> Camera;
    public List<GameObject> Prison;

    public Transform frontDescription;

    public Transform CameraDescrription;

    public Transform PrisonDescription;

    public Transform LeftBig;
    public Transform RightBig;

    public Transform LeftSmall;
    public Transform RightSmall;

    public Transform Arrows1;
    public Transform Arrows2;

    public Transform swap;

    public Transform powerUps;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ActivateFront()
    {
        BehindText.gameObject.SetActive(true);
        LeftBig.gameObject.SetActive(false);
        RightBig.gameObject.SetActive(false);

        LeftSmall.gameObject.SetActive(true);
        RightSmall.gameObject.SetActive(true);

        foreach(GameObject obj in Camera)
        {
            obj.GetComponent<Animator>().enabled=false;
        }

        foreach(GameObject obj in Prison)
        {
            obj.GetComponent<Animator>().enabled=false;
        }

        foreach(GameObject obj in Front)
        {
            obj.GetComponent<Animator>().enabled=true;
        }

        frontDescription.gameObject.SetActive(true);
        
    }

    public void ActivateCamera()
    {
        foreach(GameObject obj in Camera)
        {
            obj.GetComponent<Animator>().enabled=false;
        }

        foreach(GameObject obj in Prison)
        {
            obj.GetComponent<Animator>().enabled=true;
        }

        foreach(GameObject obj in Front)
        {
            obj.GetComponent<Animator>().enabled=false;
        }
        frontDescription.gameObject.SetActive(false);
        CameraDescrription.gameObject.SetActive(true);        
    }

    public void ActivatePrison()
    {
        foreach(GameObject obj in Camera)
        {
            obj.GetComponent<Animator>().enabled=true;
        }

        foreach(GameObject obj in Prison)
        {
            obj.GetComponent<Animator>().enabled=false;
        }

        foreach(GameObject obj in Front)
        {
            obj.GetComponent<Animator>().enabled=false;
        }
        frontDescription.gameObject.SetActive(false);
        CameraDescrription.gameObject.SetActive(false); 
        PrisonDescription.gameObject.SetActive(true);     

        StartCoroutine(activateArrows1());   
    }


    IEnumerator activateArrows1()
    {
        
        yield return new WaitForSeconds(5.5f);

        swap.gameObject.SetActive(true);
        frontDescription.gameObject.SetActive(false);
        CameraDescrription.gameObject.SetActive(false); 
        PrisonDescription.gameObject.SetActive(false);
        LeftSmall.gameObject.SetActive(false);
        RightSmall.gameObject.SetActive(false);

        Arrows1.gameObject.SetActive(true);
        Arrows2.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.5f);

        swap.gameObject.SetActive(false);
        Arrows1.gameObject.SetActive(false);
        Arrows2.gameObject.SetActive(false);

        powerUps.gameObject.SetActive(true);

        this.gameObject.SetActive(false);
    }

}

