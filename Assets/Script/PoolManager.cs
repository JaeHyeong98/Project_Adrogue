using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++) 
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, bool isRandom)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index]) 
        { 
            if(!item.activeSelf)
            {
                select = item; 
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(prefabs[index].gameObject, transform);
            pools[index].Add(select);
        }

        if(isRandom)
        {
            float x = Random.Range(-15, 16);
            float y;
            if (Mathf.Abs(x) < 10)
            {
                y = Random.Range(-5, 6);
                y = y < 0 ? y - 10 : y + 10;
            }
            else
            {
                y = Random.Range(-15, 16);
                if (Mathf.Abs(y) < 10)
                {
                    x = Random.Range(-5, 6);
                    x = x < 0 ? x - 10 : x + 10;
                }
            }

            select.transform.localPosition = new Vector3(x, y, 0) + GameManager.instance.player.transform.position;
        }

        return select;
    }


}
