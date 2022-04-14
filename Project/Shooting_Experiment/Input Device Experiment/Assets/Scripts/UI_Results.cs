using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using System;

public class UI_Results : MonoBehaviour
{
    public static GameObject resultsPanel;
    public Text stats_one, stats_two, stats_three, stats_total;
    private PlayerInputControls controls;
    // static string text_one, text_two, text_three, text_total = "null";


    // Start is called before the first frame update
    void Awake()
    {   
        List<List<string>> results = StartExp.getResults();
        setResults(results[0], results[1], results[2], results[3]);
        if (UI_Manager.using_gamepad) {
            controls = new PlayerInputControls();
            controls.Gameplay.Quit.performed += ctx => quit();
            controls.Gameplay.Enable();
        }
        resultsPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // stats_one.text = text_one;
        // stats_two.text = text_two;
        // stats_three.text = text_three;
        // stats_total.text = text_total;
        Debug.Log("One = " + stats_one.text);
        Debug.Log("Two = " + stats_two.text);
        Debug.Log("Three = " + stats_three.text);
        Debug.Log("Total = " + stats_total.text);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        resultsPanel.SetActive(true);
    }

    public void setResults(List<string> s_one, List<string> s_two, List<string> s_three, List<string> s_total) {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // resultsPanel.SetActive(true);
        Debug.Log("One = " + s_one.ToString());
        Debug.Log("Two = " + s_two.ToString());
        Debug.Log("Three = " + s_three.ToString());
        Debug.Log("Total = " + s_total.ToString());
        stats_one.text = s_one[0] + "\n" + s_one[1] + "\n" + s_one[2] + "\n" + s_one[3];
        stats_two.text = s_two[0] + "\n" + s_two[1] + "\n" + s_two[2] + "\n" + s_two[3];
        stats_three.text = s_three[0] + "\n" + s_three[1] + "\n" + s_three[2] + "\n" + s_three[3];
        stats_total.text = s_total[0] + "\n" + s_total[1] + "\n" + s_total[2] + "\n" + s_total[3];

        // string path = @"C:\Users\mrewr\CS464\Unity Input Device Experiment\Log_Files";
        string localPath = Directory.GetCurrentDirectory();
        string specifiedPath = Path.Combine(localPath,"Experiment_Data");
        if (!Directory.Exists(specifiedPath)) {
            Directory.CreateDirectory(specifiedPath);
        }
        string fileName;
        if (UI_Manager.using_gamepad) {
            fileName = "gamepad" + Time.time.ToString() + "shooting_exp.csv";
        } else {
            fileName = "mouse" + Time.time.ToString() + "shooting_exp.csv";
        }
        string finalPath = Path.Combine(specifiedPath,fileName);
        StreamWriter writer = new StreamWriter(finalPath, true);

        writer.WriteLine("Participant " + Time.time.ToString());
        writer.WriteLine("hits,misses,accuracy,time(secs)");
        writer.WriteLine(s_one[0] + "," + s_one[1] + "," + s_one[2] + "," + s_one[3]);
        writer.WriteLine(s_two[0] + "," + s_two[1] + "," + s_two[2] + "," + s_two[3]);
        writer.WriteLine(s_three[0] + "," + s_three[1] + "," + s_three[2] + "," + s_three[3]);
        writer.WriteLine(s_total[0] + "," + s_total[1] + "," + s_total[2] + "," + s_total[3]);
        writer.Close();
        // Debug.Log(stats_one.text);
        // Debug.Log(stats_two.text);
        // Debug.Log(stats_three.text);
        // Debug.Log(stats_total.text);
    }

    public void quit() {
        Application.Quit();
    }

    // static void Main(string[] args)  {
    //     var s_one = new List<string> {"hits", "misses", "accuracy", "time"};
    //     var s_two = new List<string> {"hits", "misses", "accuracy", "time"};
    //     var s_three = new List<string> {"hits", "misses", "accuracy", "time"};
    //     var s_total = new List<string> {"hits", "misses", "accuracy", "time"};
    //     UI_Results ui = new UI_Results();
    //     ui.setResults(s_one, s_two, s_three, s_total);
    // }
}
