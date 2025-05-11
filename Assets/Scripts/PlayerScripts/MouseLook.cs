using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi ekrana kilitler
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Kameran�n yukar�-a�a�� d�n���n� s�n�rlama

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera yukar�-a�a�� bakar
        playerBody.Rotate(Vector3.up * mouseX); // Karakter sa�a-sola d�ner
    }
}
