using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Buff : MonoBehaviour
{
    public BuffType type;
    public int id;
    public Sprite sprite;
    public string name;
    private float activeTimer = 5f;
    public AudioClip voiceoverClip;

    private void OnEnable()
    {
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

    public void SetUpBuff(Buff buff)
    {
        /*this.type = buff.type;
        this.id = buff.id;
        this.sprite = buff.sprite;
        this.name = buff.name;
        this.voiceoverClip = buff.voiceoverClip;

        GetComponent<AudioSource>().clip = voiceoverClip;
        GetComponent<AudioSource>().Play();*/
    }
}
