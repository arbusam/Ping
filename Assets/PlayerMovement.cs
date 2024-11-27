using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    [NonSerialized] public UnityEvent onScore = new UnityEvent();
    [NonSerialized] public int difficulty = 0; // 1 = easy, 2 = medium, 3 = hard

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SpriteRenderer rightWall;

    [Header("Easy Settings")]
    [SerializeField] private float easyVerticalSpeed = 5f;
    [SerializeField] private float easyHorizontalSpeed = 5f;

    [Header("Medium Settings")]
    [SerializeField] private float mediumVerticalSpeed = 4f;
    [SerializeField] private float mediumHorizontalSpeed = 4f;

    [Header("Hard Settings")]
    [SerializeField] private float hardVerticalSpeed = 3f;
    [SerializeField] private float hardHorizontalSpeed = 3f;

    bool goingleft = true;

    int score = 0;

    public void SetDifficulty(int difficulty) {
        this.difficulty = difficulty;
        if (difficulty == 1) {
            rightWall.color = new Color(0, 1, 0);
            rightWall.tag = "Goal";
        }
        else if (difficulty == 3) {
            rightWall.color = new Color(1, 0, 0);
            rightWall.tag = "Death";
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    float GetVerticalSpeed() {
        if (difficulty == 1) {
            return easyVerticalSpeed;
        }
        else if (difficulty == 2) {
            return mediumVerticalSpeed;
        }
        else if (difficulty == 3) {
            return hardVerticalSpeed;
        }
        return easyVerticalSpeed;
    }

    float GetHorizontalSpeed() {
        if (difficulty == 1) {
            return easyHorizontalSpeed;
        }
        else if (difficulty == 2) {
            return mediumHorizontalSpeed;
        }
        else if (difficulty == 3) {
            return hardHorizontalSpeed;
        }
        return 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, GetVerticalSpeed(), 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(0, GetVerticalSpeed(), 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -GetVerticalSpeed(), 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(0, -GetVerticalSpeed(), 0) * Time.deltaTime;
        }

        if (goingleft) {
            transform.position += new Vector3(-GetHorizontalSpeed(), 0, 0) * Time.deltaTime;
        }
        else {
            transform.position += new Vector3(GetHorizontalSpeed(), 0, 0) * Time.deltaTime;
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
            goingleft = !goingleft;
        }
        else if (collision.gameObject.tag == "Wall")
        {
            goingleft = !goingleft;
        }
        else if (collision.gameObject.tag == "Goal")
        {
            transform.position = new Vector3(5, 0, 0); 
            goingleft = true;
            if (difficulty == 3) {
                score += 5;
            }
            else {
                score++;
            }
            scoreText.text = "Score: " + score;
            onScore.Invoke();
        }
        else if (collision.gameObject.tag == "Death") {
            goingleft = !goingleft;
            score--;
            scoreText.text = "Score: " + score;
        }
    }
}
