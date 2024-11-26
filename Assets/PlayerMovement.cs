using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    [NonSerialized] public UnityEvent onScore = new UnityEvent();

    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private TextMeshProUGUI scoreText;

    bool goingleft = true;
    private bool canMoveHorizontally = false;

    int score = 0;

    public void EnableHorizontalMovement() {
        canMoveHorizontally = true;
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, verticalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(0, verticalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -verticalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(0, -verticalSpeed, 0) * Time.deltaTime;
        }
        
        if (canMoveHorizontally) {
            if (goingleft) {
                transform.position += new Vector3(-horizontalSpeed, 0, 0) * Time.deltaTime;
            }
            else {
                transform.position += new Vector3(horizontalSpeed, 0, 0) * Time.deltaTime;
            }
        }

        // Checks if the player is above or below the screen so that it can't leave the screen
        if (transform.position.y > 4.5f) {
            transform.position = new Vector3(transform.position.x, 4.5f, 0);
        }
        if (transform.position.y < -4.5f) {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            goingleft = false;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            goingleft = true;
        }
        else if (collision.gameObject.tag == "Goal")
        {
            transform.position = new Vector3(6, 0, 0); 
            goingleft = true;
            score++;
            scoreText.text = "Score: " + score;
            onScore.Invoke();
        }
    }
}
