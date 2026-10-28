using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FreeRoamCamera : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float sprintSpeed = 30f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public bool invertMouseY = false;

    [Header("View Distance")]
    public float viewDistance = 10000f;

    private float pitch;
    private float yaw;
    private bool mouseLocked = true;

    void Start()
    {
        // Set the initial rotation.
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        // Increase how far the camera can see.
        GetComponent<Camera>().farClipPlane = viewDistance;

        LockMouse();
    }

    void Update()
    {
        // Unlock the mouse with Escape.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockMouse();
        }

        // Click to capture the mouse again.
        if (!mouseLocked && Input.GetMouseButtonDown(0))
        {
            LockMouse();
        }

        // Only look around when the mouse is locked.
        if (mouseLocked)
        {
            float mouseX = Input.GetAxis("Mouse X")
                           * mouseSensitivity;

            float mouseY = Input.GetAxis("Mouse Y")
                           * mouseSensitivity;

            yaw += mouseX;

            pitch += invertMouseY ? mouseY : -mouseY;
            pitch = Mathf.Clamp(pitch, -89f, 89f);

            transform.rotation =
                Quaternion.Euler(pitch, yaw, 0f);
        }

        MoveCamera();
    }

    void MoveCamera()
    {
        float speed = Input.GetKey(KeyCode.LeftShift)
            ? sprintSpeed
            : moveSpeed;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        if (Input.GetKey(KeyCode.W)) vertical += 1f;

        // Move relative to the direction the camera faces.
        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        // Fly up and down independently of the viewing angle.
        if (Input.GetKey(KeyCode.Space))
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.LeftControl))
            movement += Vector3.down;

        transform.position +=
            movement.normalized * speed * Time.deltaTime;
    }

    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseLocked = true;
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseLocked = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}