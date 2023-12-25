using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class CurrentSetting : ScriptableObject
{

}

public class CurrentSettingSingleton
{
    static CurrentSettingSingleton _instance;
    float _snakeSpeed = 0;
    float _generatingFoodSpeed = 0;
    public int _selecetedFood = 0;
    CurrentSettingSingleton()
    {
        _instance = this;
    }
    public static CurrentSettingSingleton Instance
    {
        get
        {
            if (_instance == null) _instance = new CurrentSettingSingleton();
            return _instance;
        }
    }

    public void SetSnakeSpeed(float Value)
    {
        _snakeSpeed = Value;
    }
    public void SetGeneratingFoodSpeed(float Value)
    {
        _generatingFoodSpeed = Value;
    }
    public float GetSnakeSpeed()
    {
        return _snakeSpeed;
    }
    public float GetGeneratingFoodSpeed()
    {
        return _generatingFoodSpeed ;
    }
}

