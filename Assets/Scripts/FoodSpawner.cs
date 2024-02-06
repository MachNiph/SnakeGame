using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject food;

        [SerializeField]
        private GameObject spawnedFood;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                Destroy(spawnedFood);
                spawnedFood = Instantiate(food, new Vector2(Random.Range(-5, 6), Random.Range(-3.5f, 4.5f)), Quaternion.identity);
            }
        }
    }
}
