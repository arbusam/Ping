using UnityEngine;

class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float sensitivity = 1.5f;

    [Header("Easy Settings")]
    [SerializeField] private float speed = 5.5f;
    [SerializeField] private float timeBetweenDirectionChanges = 1f;

    [Header("Medium Settings")]
    [SerializeField] private float mediumSpeed = 5.5f;
    [SerializeField] private float mediumTimeBetweenDirectionChanges = 0.75f;

    [Header("Hard Settings")]
    [SerializeField] private float hardSpeed = 6f;
    [SerializeField] private float hardTimeBetweenDirectionChanges = 0.5f;

    private float timeSinceLastDirectionChange = Mathf.Infinity;
    private bool goingUp = false;
    private bool goingDown = false;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        player.onScore.AddListener(OnScore);
    }

    void OnScore()
    {
        transform.position = initialPosition;
        goingUp = false;
        goingDown = false;
        timeSinceLastDirectionChange = Mathf.Infinity;
    }
    
    void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange < timeBetweenDirectionChanges)
        {
            if (goingUp)
            {
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
            }
            else if (goingDown)
            {
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
            }
        }
        else {
            if (player.transform.position.y > transform.position.y + sensitivity)
            {
                goingUp = true;
                goingDown = false;
                timeSinceLastDirectionChange = 0f;
            }
            else if (player.transform.position.y < transform.position.y - sensitivity)
            {
                goingDown = true;
                goingUp = false;
                timeSinceLastDirectionChange = 0f;
            }
        }
    }
}