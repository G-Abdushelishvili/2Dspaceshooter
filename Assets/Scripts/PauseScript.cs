using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pause;
    bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                pause.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;
            }
            else if(isPaused == true)
            {
                pause.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
