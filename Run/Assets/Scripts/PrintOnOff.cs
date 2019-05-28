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
            GameObject.Find("textresult1").GetComponent<TextMeshProUGUI>().text = 0.ToString();
            return;
        }


       GameObject.Find("textresult1").GetComponent<TextMeshProUGUI>().text = results[0].ToString();

    }
    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        ShowStatistics();
    }

    public List<int> ReadTopStatisticsFromFile()
    {
        List<int> results = new List<int>(0);
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