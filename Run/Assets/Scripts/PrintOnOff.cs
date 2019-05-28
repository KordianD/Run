// Implement OnDisable and OnEnable script functions.
// These functions will be called when the attached GameObject
// is toggled.
// This example also supports the Editor.  The Update function
// will be called, for example, when the position of the
// GameObject is changed.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PrintOnOff : MonoBehaviour
{
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }


    void ShowStatistics()
    {
        List<int> results = ReadTopStatisticsFromFile();
        Debug.Log(string.Join(";", results.Select(x => x.ToString()).ToArray()));

        if (results.Count == 0)
        {
            GameObject.Find("textresult1").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("textresult2").GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("textresult3").GetComponent<TextMeshProUGUI>().text = "";
            return;
        }

        for (int i = 0; i < results.Count && i < 3; i++)
        {
            GameObject.Find("textresult" + (i + 1).ToString()).GetComponent<TextMeshProUGUI>().text = results[i].ToString();
        }
       
    }
    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        ShowStatistics();
    }

    public List<int> ReadTopStatisticsFromFile()
    {
        List<int> results = new List<int>();
        string destination = "save.txt";

         if (!File.Exists(destination))
        {
            return results;
         }

        using (StreamReader sr = File.OpenText(destination))
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                results.Add(Int32.Parse(s));
            }
        }

        results.Reverse();
        return results;
    }


    void Update()
    {
#if UNITY_EDITOR
        Debug.Log("Editor causes this Update");
        ShowStatistics();
#endif
    }


}