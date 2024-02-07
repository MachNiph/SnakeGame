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

        [SerializeField] private Vector2 gridVector;
        private SnakeController controller;


        private void Start()
        {
            controller = FindObjectOfType<SnakeController>();
            StartCoroutine(SpawnCoroutine());

        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {

                yield return new WaitForSeconds(5);
                int gridValueX = Random.Range(-5, 5 + 1);
                int gridValueY = Random.Range(-4, 4 + 1);

                gridVector.x = gridValueX + 0.46f;
                gridVector.y = gridValueY + 0.46f;

                bool isOnSnakePos = false;

                do
                {
                    Destroy(spawnedFood);
                    spawnedFood = Instantiate(food, gridVector, Quaternion.identity);
                    isOnSnakePos = IsOnSnakePosition(gridVector);
                }
                while (isOnSnakePos);
            }
        }

        private bool IsOnSnakePosition(Vector2 position)
        {
            foreach (var body in controller.bodies)
            {
                if ((Vector2)body.transform.position == position)
                {
                    return true;
                }
            }
            return false;
        }
    }
}