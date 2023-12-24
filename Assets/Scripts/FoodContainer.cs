using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public FoodBehaviour foddPrefab;
    GridManager gridManager;
    public Transform foodParent;
    List<FoodBehaviour> inactiveFoodBehaviours;
    void Start()
    {
        gridManager = GridManager.Instance;
        inactiveFoodBehaviours = new List<FoodBehaviour>();
        StartCoroutine(SpawnMarbles());
    }
    IEnumerator SpawnMarbles()
    {
        for (int i = 0; i < 300; i++)
        {

            GenerateFood();
        }
        while (true)
        {
            GenerateFood();
            yield return new WaitForSeconds(2f);
        }
    }
    private void GenerateFood()
    {
        FoodBehaviour newFood;
        if (inactiveFoodBehaviours.Count == 0)
        {
            newFood = Instantiate(foddPrefab, Random.insideUnitSphere * 10f + Vector3.up * 10, Quaternion.identity, foodParent);
        }
        else
        {
            newFood = inactiveFoodBehaviours[0];
            newFood.mytransform.position = Random.insideUnitSphere * 10f + Vector3.up * 10;
            newFood.gameObject.SetActive(true);
            inactiveFoodBehaviours.RemoveAt(0);
        }
        newFood.gameObject.name = $"ball {gridManager.foodCount}";
        gridManager.AddFoodIntoGrid(newFood);
    }
    public void DeactivateFood(FoodBehaviour food)
    {
        gridManager.RemoveFoodFromGrid(food);
        food.gameObject.SetActive(false);
        inactiveFoodBehaviours.Add(food);

    }

    //public FoodBehaviour GetCloseFoodToPosition(Vector3 position)
    //{
    //    return gridManager.GetTheNearestFood(position);
    //}
}
