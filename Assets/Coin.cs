using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its Y axis (you can change the axis and values)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
