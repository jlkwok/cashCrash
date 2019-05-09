using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    private static string leaderboardPath;
    private SortedList sl = new SortedList();

    // Start is called before the first frame update
    void Start()
    {
        leaderboardPath = Application.persistentDataPath + "/leaderboard.dat";
        if (File.Exists(leaderboardPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(leaderboardPath, FileMode.Open);
            sl = (SortedList)bf.Deserialize(file);
            String t = "";
            for (int i = sl.Count - 1; i >= 0; i--)
            {
                t += (sl.Count-i) + ". " + sl.GetKey(i) + " by " + sl.GetByIndex(i) +"\n";
            }
            GameObject.Find("Scores").GetComponent<Text>().text = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
