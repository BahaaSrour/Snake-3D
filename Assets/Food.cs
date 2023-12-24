using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Food object
public class Food : MonoBehaviour
{

    public int points = 10;
    public int lifetime = 30;
    float time;
    public void Update()
    {
        time = time + Time.deltaTime;
        if (time > lifetime)
        {
            Destroy(gameObject);
        }
    }

    public int growthAmount = 1;
    public void randomize()
    {
        float randomx, randomy, randomz;
        randomx = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomy = UnityEngine.Random.Range(-10.0f, 10.0f);
        randomz = UnityEngine.Random.Range(0.5f, 10.0f);
        transform.position = new Vector3(randomx, randomz, randomy);
    }

    public void DeActivateFood()
    {
        if (time > lifetime)
        {
            Destroy(gameObject);
        }
    }
}