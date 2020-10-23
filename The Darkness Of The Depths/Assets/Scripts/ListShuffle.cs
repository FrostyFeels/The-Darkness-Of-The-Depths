using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListShuffle : MonoBehaviour
{
    public List<int> randomList = new List<int> {1,2,3,4,5,6,7,8,9,10 };

    public void Start()
    {
        shuffle();
    }
    void shuffle()
    {
        for (int i = 0; i < randomList.Count; i++)
        {
            int temp = randomList[i];
            int randomIndex = Random.Range(i, randomList.Count);
            randomList[i] = randomList[randomIndex];
            randomList[randomIndex] = temp;
        }

        for (int i = 0; i < randomList.Count; i++)
        {
            Debug.Log(randomList[i]);
        }
    }
}
