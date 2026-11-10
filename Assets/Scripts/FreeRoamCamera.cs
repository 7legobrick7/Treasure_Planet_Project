
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
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;

        GetComponent<Camera>().farClipPlane = viewDistance;

        LockMouse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockMouse();
        }

        if (!mouseLocked && Input.GetMouseButtonDown(0))
        {
            LockMouse();
        }

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

        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        if (Input.GetKey(KeyCode.Space))
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.LeftControl))
            movement += Vector3.down;

        transform.position +=
            movement.normalized * speed * Time.deltaTime;
    }

    // Teleport camera to a waypoint.
    public void TeleportTo(Transform target)
    {
        if (target == null)
            return;

        transform.SetPositionAndRotation(
            target.position,
            target.rotation
        );

        // Update mouse-look angles to match the new rotation.
        Vector3 angles = transform.eulerAngles;

        pitch = angles.x;

        if (pitch > 180f)
            pitch -= 360f;

        yaw = angles.y;

        pitch = Mathf.Clamp(pitch, -89f, 89f);
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
