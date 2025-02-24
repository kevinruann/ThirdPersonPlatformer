using UnityEngine;
using Unity.Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 1f;
    private bool isCollidingWithGround = false;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space) && isCollidingWithGround == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
        isCollidingWithGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isCollidingWithGround = false;
    }
}
