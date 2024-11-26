using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;

    [SerializeField] private CircleCollider2D circleCollider2D;

    bool goingleft = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, horizontalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += new Vector3(0, horizontalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -horizontalSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += new Vector3(0, -horizontalSpeed, 0) * Time.deltaTime;
        }
        if (goingleft) {
            transform.position += new Vector3(-verticalSpeed, 0, 0) * Time.deltaTime;
        }
        else {
            transform.position += new Vector3(verticalSpeed, 0, 0) * Time.deltaTime;
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
    }
}
