using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Buff : MonoBehaviour
{
    public int id;
    public Sprite sprite;
    private float activeTimer = 5f;
    public AudioClip voiceoverClip;

    public AudioSource audiosource;

    public Sprite ico;

    void Awake()
    {
        audiosource=transform.parent.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        audiosource.PlayOneShot(voiceoverClip);
        StartCoroutine(dissaapera());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator dissaapera()
    {
        yield return new WaitForSeconds(5f);

        this.gameObject.SetActive(false);
    }
        
    /*private void Update()
    {
        if(activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
        }
        else
        {

            Destroy(gameObject);
        }
    }*/
}
