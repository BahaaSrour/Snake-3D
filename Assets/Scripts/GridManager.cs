using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    public static GridManager Instance => instance;
    public List<FoodBehaviour>[,,] arr3d;
    private List<FoodBehaviour> close;
    public int foodCount = 0;
    Vector3Int vector3intOf3 = Vector3Int.one * 3;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        arr3d = new List<FoodBehaviour>[4, 4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    arr3d[i, j, k] = new List<FoodBehaviour>();
                }
            }
        }

        close = new List<FoodBehaviour>();
    }
    public void AddFoodIntoGrid(FoodBehaviour _value)
    {
        var gridPosition = PostionToGridPosition(_value.transform.position);
        arr3d[gridPosition.x, gridPosition.y, gridPosition.z].Add(_value);
        foodCount++;
    }
    public void RemoveMarbleFromGrid(FoodBehaviour _value)
    {
        var gridPosition = PostionToGridPosition(_value.transform.position);
        arr3d[gridPosition.x, gridPosition.y, gridPosition.z].Remove(_value);
        foodCount--;
    }
    public static Vector3Int PostionToGridPosition(Vector3 position)
    {
        return new Vector3Int((int)(position.x + 100) / 50, (int)(position.y + 100) / 50, (int)(position.z + 100) / 50);
    }

    public FoodBehaviour GetTheNearestFood(Vector3 snakePosition)
    {
        Vector3Int foodsListIndex = PostionToGridPosition(snakePosition);
        foodsListIndex.Clamp(Vector3Int.zero, vector3intOf3);
        if (arr3d[foodsListIndex.x, foodsListIndex.y, foodsListIndex.z].Count == 0)
        {
            Vector3Int tmp = new Vector3Int(Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4));
            if (arr3d[tmp.x, tmp.y, tmp.z].Count > 0)
                return arr3d[tmp.x, tmp.y, tmp.z][0];
            return null;
        }
        else
        {
            List<FoodBehaviour> tmp = arr3d[foodsListIndex.x, foodsListIndex.y, foodsListIndex.z];
            FoodBehaviour closest = tmp[0];
            for (int i = 0; i < tmp.Count; i++)
            {
                if ((snakePosition - closest.mytransform.position).sqrMagnitude >
                    (snakePosition - tmp[i].mytransform.position).sqrMagnitude)
                    closest = tmp[i];
            }
            return closest;
        }
    }
}