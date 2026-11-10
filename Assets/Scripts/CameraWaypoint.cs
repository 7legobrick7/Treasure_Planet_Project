
using UnityEngine;

public class CameraWaypoint : MonoBehaviour
{
    [Header("Waypoint Settings")]
    public string locationName = "New Location";

    [Header("Teleport Key")]
    public KeyCode teleportKey = KeyCode.Alpha1;

    public bool IsTeleportKeyPressed()
    {
        return Input.GetKeyDown(teleportKey);
    }
}
