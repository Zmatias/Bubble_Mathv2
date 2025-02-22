using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public static BuffHandler Instance;
    public Buff[] t1Buffs; 
    public Buff[] t2Buffs;




    //public GameObject buffPrefab;

    
    public void ApplyOtherBuffTypeToTeam(string type, bool isTeam1)
    {
        if (isTeam1)
        {
            if (type == "Diminished") //Diminish enemy team
            {
                BubbleManager.Instance.shrinkBubbles("T1");

            }
            else if(type == "BubbleStorm")
            {
                BubbleManager.Instance.ColorBubbles("T1",GameManager.instance.T1Color);

            }
            else
            {
                GameManager.instance.multipleScoreT1=2;
                Debug.Log("SuperCharge");
                StartCoroutine(restartMultiplier("T1"));
            }

            Debug.Log("SuperCharge");

            var obj = returnBuffT1(type);
            if(obj!=null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.Log("IS NULL");
            }
            
        }
        else
        {
            if (type == "Diminished") //Diminish enemy team
            {
                BubbleManager.Instance.shrinkBubbles("T2");
            }
            else if (type == "BubbleStorm")
            {
                BubbleManager.Instance.ColorBubbles("T2",GameManager.instance.T2Color);
            }
            else
            {
                GameManager.instance.multipleScoreT2=2;
                StartCoroutine(restartMultiplier("T2"));
            }   

            var obj = returnBuffT2(type);
            if(obj!=null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.Log("IS NULL");
            }
            
        }
    }

    IEnumerator restartMultiplier(string T1orT2)
    {
        yield return new WaitForSeconds(5);

        if(T1orT2=="T1")
        {
            GameManager.instance.multipleScoreT1=1;
        }
        else
        {
            GameManager.instance.multipleScoreT2=1;
        }
    }

    public GameObject returnBuffT1(string nameB)
    {
        foreach(Buff bf in t1Buffs)
        {
            if(bf.gameObject.name==nameB)
            {
                return bf.gameObject;
            }
        }

        return null;
    }

     public GameObject returnBuffT2(string nameB)
    {
        foreach(Buff bf in t2Buffs)
        {
            if(bf.gameObject.name==nameB)
            {
                return bf.gameObject;
            }
        }

        return null;
    }

    private void Awake()
    {
        Instance = this;
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

}

/*     public void AddRandomBuffToTeam(bool isTeam1)
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
    } */

/*     public bool CheckIfTeamHasDiminished(bool isTeam1)
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
    } */

/*     public BuffType GetRandomBuffType()
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
    Supercharge,
    Diminished,
    BubbleStorm
}
 */