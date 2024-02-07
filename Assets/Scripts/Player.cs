using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 moveVector;
        [SerializeField]
        private Vector3 rotationVector;
        

        [SerializeField]
        private float time;
        [SerializeField]
        private float maxTime = 1;

        [SerializeField]
        private bool forceMove;
        [SerializeField]
        private Vector2 maxLimit;

        [SerializeField]
        private float radius;
        [SerializeField]
        private LayerMask foodLayerMask;
        [SerializeField]
        private LayerMask bodyLayerMask;

        [SerializeField]
        private int foodCount;

        [SerializeField]
        private GameObject bodyPrefab;
        [SerializeField]
        public List<GameObject> bodies;

        public GameObject PauseMenuUI;
        private bool isDead;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClip;
        

        private void Start()
        {
            moveVector = Vector3.right;
            rotationVector = Vector3.forward * -90;
            audioSource = GetComponent<AudioSource>();
            
          

        }

        private void Update()
        {
            Move();
            Eat();
            BodyHit();
        }

        private void Move()
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame && !(moveVector.x > 0) && !(moveVector.x < 0))
            {
                forceMove = true;
                moveVector = Vector3.left;
                rotationVector = Vector3.forward * 90;
            }
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame && !(moveVector.x < 0) && !(moveVector.x > 0))
            {
                forceMove = true;
                moveVector = Vector3.right;
                rotationVector = Vector3.forward * -90;
            }
            if (Keyboard.current.upArrowKey.wasPressedThisFrame && !(moveVector.y < 0) && !(moveVector.y > 0))
            {
                forceMove = true;
                moveVector = Vector3.up;
                rotationVector = Vector3.forward * 0;
            }
            if (Keyboard.current.downArrowKey.wasPressedThisFrame && !(moveVector.y > 0) && !(moveVector.y < 0))
            {
                forceMove = true;
                moveVector = Vector3.down;
                rotationVector = Vector3.forward * -180;
            }

            time -= Time.deltaTime;
            if (time <= 0 || forceMove)
            {
                moveVector = moveVector.normalized;
                
                if (ShouldMove())
                {
                    for (int i = bodies.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            bodies[i].transform.position += moveVector;
                            bodies[i].transform.rotation = Quaternion.Euler(rotationVector);
                        }
                        else
                        {
                            bodies[i].transform.position = bodies[i - 1].transform.position;
                            bodies[i].transform.rotation = bodies[i - 1].transform.rotation;
                        }
                    }
                }
                time = maxTime;
                forceMove = false;
            }
        }

        private void Eat()
        {
            RaycastHit2D hitInfoFood = Physics2D.CircleCast(bodies[0].transform.position, radius, bodies[0].transform.right, 0, foodLayerMask);
            if (hitInfoFood.collider != null)
            {
                if(audioSource != null)
                {
                    audioSource.clip = audioClip[0];
                    audioSource.enabled = true;
                    audioSource.PlayOneShot(audioSource.clip);
                }
                Destroy(hitInfoFood.collider.gameObject);
                foodCount++;
                GameObject body = Instantiate(bodyPrefab, bodies[bodies.Count - 1].transform.position - bodies[bodies.Count - 1].transform.up, Quaternion.identity, transform);
                bodies.Add(body);
            }
        }

        private void BodyHit()
        { 
            RaycastHit2D hitInfoBody= Physics2D.CircleCast(bodies[0].transform.position, radius, bodies[0].transform.right, 0, bodyLayerMask);
            if (hitInfoBody.collider != null )
            {
                PlayerDeath();
            }
        }

        private bool ShouldMove()
        {
            bool shouldMove = (moveVector.x > 0 && bodies[0].transform.position.x < maxLimit.x) ||
                    (moveVector.x < 0 && bodies[0].transform.position.x > -maxLimit.x) ||
                    (moveVector.y > 0 && bodies[0].transform.position.y < maxLimit.y) ||
                    (moveVector.y < 0 && bodies[0].transform.position.y > -maxLimit.y);

            if (!shouldMove)
            {
                PlayerDeath();
            }

            return shouldMove;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(bodies[0].transform.position, radius);
        }

        private void PlayerDeath()
        {
            Debug.Log("Die");
            if(audioSource != null)
            {
                audioSource.clip = audioClip[1];
                audioSource.PlayOneShot(audioSource.clip);
            }
            Time.timeScale = 0;
            PauseMenuUI.SetActive(true);
            
        }
    }
}
