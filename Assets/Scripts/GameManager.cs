using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    internal int selecetedFood = 0;


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

        SetPLayerSpeed(CurrentSettingSingleton.Instance.GetSnakeSpeed());
        SetFoodRespawnSpeed(CurrentSettingSingleton.Instance.GetGeneratingFoodSpeed());
        SetPrefabIndex(CurrentSettingSingleton.Instance._selecetedFood);
    }
    public void PLayerLost()
    {
        snake.gameObject.SetActive(false);
        canvasManager.GameLost(score);
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
            canvasManager.GameLost(score);
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
        CurrentSettingSingleton.Instance.SetSnakeSpeed(value);
    }
    public void SetFoodRespawnSpeed(float value)
    {
#if UNITY_EDITOR
        //Debug.Log($"food  Speed = {value}");
# endif

        foodContainer.SetGeneratingSpeedFromRatio(value);
        CurrentSettingSingleton.Instance.SetGeneratingFoodSpeed(value);
    }
    internal void Exit()
    {
        Application.Quit();
    }

    internal void SetPrefabIndex(int index)
    {
        selecetedFood = index;
        CurrentSettingSingleton.Instance._selecetedFood = index;
    }
}