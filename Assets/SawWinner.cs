using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawWinner : MonoBehaviour
{
    public static SawWinner instance;
    public Transform LeftWinner;
    public Transform RightWinner;

    public AudioClip thankYou;

    public AudioSource audiosource;
    // Start is called before the first frame update

    void Awake()
    {
        instance=this;               
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThankYou()
    {
        audiosource.PlayOneShot(thankYou);
    }


    public void WinnerIS()
    {
        LeftWinner.transform.parent.Find(GameManager.instance.WinnerIs).gameObject.SetActive(true);
    }
}
