
using UnityEngine;

public class CameraWaypointManager : MonoBehaviour
{
    [Header("Waypoint Configuration")]
    [SerializeField] private CameraWaypoint[] waypoints;

    [Header("Camera Configuration")]
    [SerializeField] private FreeRoamCamera freeRoamCamera;

    void Start()
    {
        // Automatically find waypoints if none were assigned.
        if (waypoints == null || waypoints.Length == 0)
        {
            waypoints = FindObjectsByType<CameraWaypoint>(
                FindObjectsSortMode.None
            );
        }

        // Automatically find the camera controller if not assigned.
        if (freeRoamCamera == null)
        {
            freeRoamCamera = GetComponent<FreeRoamCamera>();
        }

        Debug.Log("Camera Waypoints Found: " + waypoints.Length);
    }

    void Update()
    {
        foreach (CameraWaypoint waypoint in waypoints)
        {
            if (waypoint != null && waypoint.IsTeleportKeyPressed())
            {
                TeleportCamera(waypoint);
                break;
            }
        }
    }

    private void TeleportCamera(CameraWaypoint waypoint)
    {
        if (freeRoamCamera != null)
        {
            freeRoamCamera.TeleportTo(waypoint.transform);
        }
        else
        {
            transform.SetPositionAndRotation(
                waypoint.transform.position,
                waypoint.transform.rotation
            );
        }

        Debug.Log("Teleported to: " + waypoint.locationName);
    }
}
