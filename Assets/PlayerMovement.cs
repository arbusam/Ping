using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private TextMeshProUGUI scoreText;

    bool goingleft = true;

    int score = 0;

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
        if (goingleft) {
            transform.position += new Vector3(-horizontalSpeed, 0, 0) * Time.deltaTime;
        }
        else {
            transform.position += new Vector3(horizontalSpeed, 0, 0) * Time.deltaTime;
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
            transform.position = new Vector3(0, 0, 0); 
            goingleft = true;
            score++;
            scoreText.text = "Score: " + score;
        }
    }
}
