using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] GameObject restartPanal;
    [SerializeField] Button Restart1;
    [SerializeField] Button Exit;
    [SerializeField] Slider speedSlider;
    [SerializeField] Slider foodRespawnSlider;
    [SerializeField] Toggle choosePrefab;

    private void Start()
    {
        speedSlider.onValueChanged.AddListener(PlayerSpeed);
        foodRespawnSlider.onValueChanged.AddListener(RespawnFoodSpeed);
        Restart1.onClick.AddListener(RestartGame);
        Exit.onClick.AddListener(ExittGame);
    }
    void PlayerSpeed(float value)
    {
        GameManager.instance.SetPLayerSpeed(value);
    }
    void RespawnFoodSpeed(float value)
    {
        GameManager.instance.SetFoodRespawnSpeed(value);
    }

    void RestartGame()
    {
        restartPanal.SetActive(false);
        GameManager.instance.Restart();
    }
    void ExittGame()
    {
        GameManager.instance.Exit();

    }
    public void GameLost()
    {
        restartPanal.SetActive(true);

    }
}
