using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject spawnedFood;

    void Start()
    {
        StartCoroutine(SpawnFood());    
    }

    IEnumerator SpawnFood()
    {
        yield return new WaitForSeconds(3);
        Destroy(spawnedFood);
        spawnedFood = Instantiate(food,new Vector2(Random.Range(-8,8), Random.Range(-5,5)),Quaternion.identity);

    }
}
