using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public static BuffHandler Instance;

    public List<Buff> allBuffs;
    public Buff[] t1Buffs; 
    public Buff[] t2Buffs;

    //public TextMeshProUGUI buffText;
    //public Sprite buffImage;
    public GameObject t1buffPopup;
    public GameObject t2buffPopup;


    public GameObject buffPrefab;

    internal int ApplyScoreBuff(int rawAmount, bool isTeam1)
    { 
        if (isTeam1)
        {
            foreach (var buff in t1Buffs)
            {
                if (buff == null)
                    continue;
                if (buff.type == BuffType.SUPERCHARGE)
                {
                    return rawAmount * 2;
                }
            }
        }
        else
        {
            foreach (var buff in t2Buffs)
            {
                if (buff == null)
                    continue;
                if (buff.type == BuffType.SUPERCHARGE)
                {
                    return rawAmount * 2;
                }
            }
        }
        return rawAmount;
    }

    public void ApplyOtherBuffTypeToTeam(BuffType type, bool isTeam1)
    {
        if (isTeam1)
        {
            if (type == BuffType.DIMINISHED) //Diminish enemy team
            {
                foreach(var buff in t2Buffs)
                {
                    if(buff!=null && buff.type == BuffType.PROTECTED)
                    {
                        Destroy(buff.gameObject);
                        return;
                    }
                }
                //small all color bubbles
                int teamColor = GameManager.instance.T1Color;
                GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");

                foreach (GameObject bubble in bubbles)
                {
                    if(teamColor == bubble.GetComponent<Bubble>().bubbleColor)
                    {
                        //bubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
            else if(type == BuffType.BUBBLESTORM)
            {
                //must spawn more bubbles
            }
            else if(type == BuffType.PROTECTED)
            {
                //nothing
            }
        }
        else
        {
            if (type == BuffType.DIMINISHED) //Diminish enemy team
            {
                foreach (var buff in t2Buffs)
                {
                    if (buff.type == BuffType.PROTECTED)
                    {
                        Destroy(buff.gameObject);
                        return;
                    }
                }
                //small all color bubbles
                int teamColor = GameManager.instance.T2Color;
                GameObject[] bubbles = GameObject.FindGameObjectsWithTag("Bubble");

                foreach (GameObject bubble in bubbles)
                {
                    if (teamColor == bubble.GetComponent<Bubble>().bubbleColor)
                    {
                        //bubble.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                }
            }
            else if (type == BuffType.BUBBLESTORM)
            {
                //must spawn more bubbles
            }
            else if (type == BuffType.PROTECTED)
            {
                //nothing
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddRandomBuffToTeam(bool isTeam1)
    {
        if (isTeam1)
        {
            Buff newBuff = null;
            while (newBuff == null || t1Buffs.Contains(newBuff))
            {
                newBuff = GetBuffOfType(GetRandomBuffType());
            }
            for (int i = 0; i < t1Buffs.Length; i++)
            {
                if (t1Buffs[i] == null)
                {
                    //GameObject newObject = Instantiate(buffPrefab);
                    t1buffPopup.GetComponent<Buff>().SetUpBuff(newBuff);
                    t1Buffs[i] = t1buffPopup.GetComponent<Buff>();
                    StartCoroutine(dissaapera(i, isTeam1));

                    t1buffPopup.SetActive(true);

                    ApplyOtherBuffTypeToTeam(newBuff.type, true);
                }
            }
        }
        else
        {
            Buff newBuff = null;
            while (newBuff == null || t2Buffs.Contains(newBuff))
            {
                newBuff = GetBuffOfType(GetRandomBuffType());
            }
            for (int i = 0; i < t2Buffs.Length; i++)
            {
                if (t2Buffs[i] == null)
                {
                    //GameObject newObject = Instantiate(buffPrefab);
                    t2buffPopup.GetComponent<Buff>().SetUpBuff(newBuff);
                    t2Buffs[i] = t2buffPopup.GetComponent<Buff>();
                    StartCoroutine(dissaapera(i,isTeam1));
                    t2buffPopup.SetActive(true);

                    ApplyOtherBuffTypeToTeam(newBuff.type, true);
                }
            }
        }
    }
    IEnumerator dissaapera(int referBur,bool isTeam1)
    {
        yield return new WaitForSeconds(5f);
        if (isTeam1)
        {
            t1Buffs[referBur] = null;
        }
        else t2Buffs[referBur] = null;
    }
    public bool CheckIfTeamHasDiminished(bool isTeam1)
    {
        if (isTeam1)
        {
            for (int i = 0; i < t1Buffs.Length; i++)
            {
                if (t1Buffs[i] != null && t1Buffs[i].type == BuffType.DIMINISHED)
                {
                    return true;
                }
            }
        }
        else
        {
            for (int i = 0; i < t2Buffs.Length; i++)
            {
                if (t2Buffs[i] != null && t2Buffs[i].type == BuffType.DIMINISHED)
                {
                    return false;
                }
            }
        }
        return false;
    }

    public BuffType GetRandomBuffType()
    {
        return (BuffType) Random.Range(0, allBuffs.Count);
    }

    public Buff GetBuffOfType(BuffType type)
    {
        foreach (var buff in allBuffs)
        {
            if (buff.type == type)
            {
                return buff;
            }
        }
        return null;
    }
}


public enum BuffType
{
    SUPERCHARGE,
    DIMINISHED,
    BUBBLESTORM,
    PROTECTED
}
