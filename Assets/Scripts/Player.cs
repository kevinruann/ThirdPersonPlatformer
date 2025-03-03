using UnityEngine;
using Unity.Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Coin coin;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    private bool isCollidingWithGround = false;
    private bool doubleJump = false;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCollidingWithGround == true)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isCollidingWithGround = false;
                doubleJump = true;
                Debug.Log("Jump");
            }
            else if (doubleJump == true)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isCollidingWithGround = false;
                doubleJump = false;
                Debug.Log("Double Jump");
            } 
        }
        if (Input.GetKeyDown(KeyCode.W) && isCollidingWithGround == false)
        {
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            Debug.Log("Dash");
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = freeLookCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * direction.y + cameraRight * direction.x;
        rb.AddForce(speed * moveDirection);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Surface")) {
            isCollidingWithGround = true;
            doubleJump = false;
        }
        
        if (collision.gameObject.CompareTag("Coin")) {
            Destroy(collision.gameObject);
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.incrementScore();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Surface")) {
            isCollidingWithGround = false;
        }
    }
}
