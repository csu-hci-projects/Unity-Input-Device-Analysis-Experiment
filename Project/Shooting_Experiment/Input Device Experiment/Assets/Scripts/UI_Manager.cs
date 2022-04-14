using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_Manager : MonoBehaviour
{

    public enum Scene {
        UI_Scene,
        Shooting_Experiment,
    }
    public static bool using_gamepad = false;

    public GameObject startPanel;
    private Mouse mouse;
    private Vector2 move_mouse;

    private PlayerInputControls controls;
    // Start is called before the first frame update

    void Awake()
    {   
        startPanel.SetActive(true);
        var gamepad = Gamepad.current;
        if (gamepad == null) {
            mouse = Mouse.current;
            if (mouse == null) { Application.Quit();}
        } else { 
            using_gamepad = true;
            controls = new PlayerInputControls();
            controls.Gameplay.Jump.performed += ctx => startExperiment();
            controls.Gameplay.Quit.performed += ctx => quit();
            controls.Gameplay.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void startExperiment() {
        startPanel.SetActive(false);
        SceneManager.LoadScene(UI_Manager.Scene.Shooting_Experiment.ToString());
    }

    public void quit() {
        Application.Quit();
    }
}
