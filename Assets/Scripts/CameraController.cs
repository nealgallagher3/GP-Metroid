﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

 public class CameraController : MonoBehaviour
{
    private Transform player; 
    private Vector3 cameraPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPos = player.position - transform.position;
    }

    void Update()
    {
        if (player.localPosition.x < 10000.0f && player.localPosition.x > -100.0f)
        {
            transform.position = player.position - cameraPos;
        }
        if (Input.GetKey(KeyCode.U))
        {
            SceneManager.LoadScene("Level 1");
        }
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}