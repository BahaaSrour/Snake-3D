using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

internal class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CanvasManager canvasManager;
    public int score;
    public GameObject winText;
    public Snake snake;
    public FoodContainer foodContainer;
    internal bool Stop;
    int sceneIndex;
    internal readonly int foodSeleceted = 0;


    [SerializeField] float playerMinSpeed = 5;
    [SerializeField] float playerMaxspeed = 15;
    [SerializeField] float SpeedRatio;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SpeedRatio = playerMaxspeed - playerMinSpeed;
    }
    public void PLayerLost()
    {
        snake.gameObject.SetActive(false);
        canvasManager.GameLost();
    }
    internal void AddPoints(int points)
    {
        score += points;
        winText.GetComponent<TextMeshProUGUI>().text = score + "";
    }
    public void CheckWinGame()
    {
        if (score >= 100)
        {
            canvasManager.GameLost();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void SetPLayerSpeed(float value)
    {
#if UNITY_EDITOR
        Debug.Log($"Speed = {value}");
# endif
        var speed = playerMinSpeed + SpeedRatio * value;

        snake.SetPLayerSpeed(speed);
    }
    public void SetFoodRespawnSpeed(float value)
    {
#if UNITY_EDITOR
        //Debug.Log($"food  Speed = {value}");
# endif

        foodContainer.SetGeneratingSpeed(value);
    }
    internal void Exit()
    {
        Application.Quit();
    }
}