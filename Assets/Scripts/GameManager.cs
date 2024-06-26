using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;
using static UnityEngine.ParticleSystem;
using System;

public class GameManager : MonoBehaviour
{
    private static GameObject arrowLeft;
    private static GameObject arrowRight;
    private static GameObject plane;
    private static GameObject go;
    private static GameObject light;
    public static GameObject ryan;
    private static GameObject transition;
    private static GameObject hand1;
    private static GameObject hand2;
    private static GameObject leg1;
    private static GameObject leg2;
    private static GameObject capsule;
    private static GameObject conductor;
    private static GameObject train;
    private static GameObject train2;
    private static Camera camera;

    public static float TopBorder = 2.6f;
    public static float BottomBorder = -2.6f;


    public static bool visableFlag = false;
    public static bool CameraMove = false;
    public static bool RyanMove = false;
    public static bool TakeSomething = false;
    public static bool isItGameOver = false;

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
    private const int KEYEVENTF_KEYUP = 0x0002;


    public void SimulateSpaceKeyPress()
    {
        keybd_event(0x20, 0, 0, 0);
        keybd_event(0x20, 0, KEYEVENTF_KEYUP, 0);
    }

    void Start()
    {
        arrowLeft = GameObject.Find("ArrowLeft");
        arrowRight = GameObject.Find("ArrowRight");
        plane = GameObject.Find("back");
        go = GameObject.Find("LetsGo");
        light = GameObject.Find("Light");
        ryan = GameObject.Find("Ryan");
        transition = GameObject.Find("Square");
        hand1 = GameObject.Find("Hand1");
        hand2 = GameObject.Find("Hand2");
        leg1 = GameObject.Find("Leg1");
        leg2 = GameObject.Find("Leg2");
        capsule = GameObject.Find("Capsule");
        conductor = GameObject.Find("Conductor");
        train = GameObject.Find("Train");
        train2 = GameObject.Find("Train2");
        camera = Camera.main;
        
        arrowLeft.transform.position = new Vector3(-4.96f, 0.02f, 0);
        arrowRight.transform.position = new Vector3(5.15f, 0.08f, 0);
        light.transform.position = new Vector3(-0.2200007f, -2.38f, 0);
        transition.transform.position = new Vector3(0, 0, 0);
        ryan.transform.position = new Vector3(0, 0, 0);
        plane.transform.position = new Vector3(0, 0, 0);
        go.transform.position = new Vector3(5.79f, 3.69f, 0);
        hand1.transform.position = new Vector3(0, 0, 0);
        hand2.transform.position = new Vector3(0, 0, 0);
        leg1.transform.position = new Vector3(0, 0, 0);
        leg2.transform.position = new Vector3(0, 0, 0);
        capsule.GetComponent<Renderer>().enabled = false;

        CameraMove = true;
        conductor.GetComponent<Renderer>().enabled = false;
    }

    private IEnumerator ExampleCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        visableFlag = true;
    }

    private IEnumerator ShowConductorAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Renderer conductorRender = conductor.GetComponent<Renderer>();
        conductorRender.enabled = true;
        conductor.GetComponent<Conductor>().target = ryan;
    }

    private void ShowRyan()
    {
        ryan.transform.localScale = new Vector3(.3f, .3f, 0);
        Renderer ryanRender = ryan.GetComponent<Renderer>();
        ryanRender.enabled = true;
        RyanMove = true;
        for (var i = 0; i < 6; i++)
            SimulateSpaceKeyPress();
    }

    private void ChangeSceneLook()
    {
        Renderer[] allRenderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in allRenderers)
        {
            if (renderer.gameObject.name is not ("Square" or "Capsule" or "TrainNose"))
                renderer.enabled = false;
        }
        train.GetComponent<Renderer>().enabled = true;
        train2.GetComponent<Renderer>().enabled = true;
        camera.orthographicSize = 5.5f;
    }

    private void MakeGameOver()
    {
        RyanMove = false;
        conductor.GetComponent<Renderer>().enabled = false;
        ryan.transform.position = new Vector3(ryan.transform.position.x, 0, 0);
        var gameOver = GameObject.Find("GameOver");
        gameOver.transform.position = ryan.transform.position;
        gameOver.GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        TakeSomething = batScript.TakeBat || CakeScript.TakeCake;

        if (StartButton.IsItLetsGo)
        {
            Music.TimeToPlay = true;
            StartCoroutine(ExampleCoroutine(2.5f));
            Camera.FindObjectOfType<VideoPlayer>().enabled = false;
        }

        if (visableFlag)
            ChangeSceneLook();

        if (Train.arrived && visableFlag)
        {
            ShowRyan();
            visableFlag = false;
            StartCoroutine(ShowConductorAfterDelay(2f));
        }

        if (isItGameOver)
            MakeGameOver();
    }
}
