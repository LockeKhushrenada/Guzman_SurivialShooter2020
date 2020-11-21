using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public GameObject slider;
    public GameObject heart;
    public GameObject player;
    public bool multiPlayer = false;
    public int playerCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && multiPlayer == false)
        {
            multiPlayer = true;
            playerCount = 2;
            cam1.rect = new Rect(0, 0, 0.5f, 1);
            cam2.rect = new Rect(0.5f, 0, 0.5f, 1);
            player.SetActive(true);
            slider.SetActive(true);
            heart.SetActive(true);
        }
    }
}
