using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager instance;
    public static GridManager Instance => instance;
    public List<FoodBehaviour>[,,] arr3d;
    private List<FoodBehaviour> close;
    [SerializeField] public int foodCount = 0;
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
    public void RemoveFoodFromGrid(FoodBehaviour _value)
    {
        var gridPosition = PostionToGridPosition(_value.transform.position);
        arr3d[gridPosition.x, gridPosition.y, gridPosition.z].Remove(_value);
        foodCount--;
    }
    public static Vector3Int PostionToGridPosition(Vector3 position)
    {
        int x = Mathf.Clamp((int)(position.x + 10) / 5, 0, 3);
        int y = Mathf.Clamp((int)(position.y / 5) / 5, 0, 3);
        int z = Mathf.Clamp((int)(position.z + 10) / 5, 0, 3);
        return new Vector3Int(x, y, z);
    }
    public Transform GetTheNearestFood(Vector3 snakePosition)
    {
        Vector3Int foodsListIndex = PostionToGridPosition(snakePosition);

        #region Old
        ////foodsListIndex.Clamp(Vector3Int.zero, vector3intOf3);
        //if (arr3d[foodsListIndex.x, foodsListIndex.y, foodsListIndex.z].Count == 0)
        //{
        //    Vector3Int tmp = new Vector3Int(Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4));
        //    if (arr3d[tmp.x, tmp.y, tmp.z].Count > 0)
        //        return arr3d[tmp.x, tmp.y, tmp.z][0].mytransform;
        //    return null;
        //}
        #endregion

        foodsListIndex = GetNearestGrid(foodsListIndex);
        if (arr3d[foodsListIndex.x, foodsListIndex.y, foodsListIndex.z].Count == 0) return null;

        List<FoodBehaviour> tmp = arr3d[foodsListIndex.x, foodsListIndex.y, foodsListIndex.z];
        FoodBehaviour closest = tmp[0];
        for (int i = 0; i < tmp.Count; i++)
        {
            if ((snakePosition - closest.mytransform.position).sqrMagnitude >
                (snakePosition - tmp[i].mytransform.position).sqrMagnitude)
                closest = tmp[i];
        }
        return closest.mytransform;

    }

    public Vector3Int GetNearestGrid(Vector3Int position)
    {
        bool[,,] visited = new bool[4, 4, 4];
        Queue<Vector3Int> queue = new Queue<Vector3Int>();

        visited[position.x, position.y, position.z] = true;
        queue.Enqueue(position);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (arr3d[current.x, current.y, current.z].Count > 0)
                return current;
            else
            {
                Vector3Int[] nearby = new Vector3Int[6];
                nearby[0] = current + Vector3Int.right;
                nearby[1] = current + Vector3Int.up;
                nearby[2] = current + Vector3Int.forward;
                nearby[3] = current - Vector3Int.right;
                nearby[4] = current - Vector3Int.up;
                nearby[5] = current - Vector3Int.forward;
                for (int i = 0; i < 6; i++)
                {
                    if (nearby[i].x >= 0 && nearby[i].x < 4 && nearby[i].y >= 0 && nearby[i].y < 4 && nearby[i].z >= 0 && nearby[i].z < 4 &&
                        !visited[nearby[i].x, nearby[i].y, nearby[i].z])
                    {
                        visited[nearby[i].x, nearby[i].y, nearby[i].z] = true;
                        queue.Enqueue(nearby[i]);
                    }
                }
            }
        }
        return position;
    }
}