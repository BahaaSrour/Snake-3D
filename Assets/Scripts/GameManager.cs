using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

internal class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject food;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(this);
    }
    public void Start()
    {
        score = 0;
        StartCoroutine("generateFood");
    }


    //TODO:: UpdateScore
    internal void AddPoints(int points)
    {
        score += points;
        winText.GetComponent<TextMeshProUGUI>().text = score + "";
    }
    public int score;
    public GameObject winText;

    void Update()
    {
        Debug.Log(score);
        if (score >= 100)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winText.SetActive(true);
    }
    IEnumerator generateFood()
    {
        yield return new WaitForSeconds(0.2f);
        float randomx, randomy, randomz;
        randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomz = UnityEngine.Random.Range(0.5f, 10.0f);
        Instantiate(food, new Vector3(randomx, randomz, randomy), Quaternion.identity);
        StartCoroutine(generateFood());
    }

    public void UpdateScore(int foodScore)
    {

    }
}