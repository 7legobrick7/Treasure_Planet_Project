
using UnityEngine;

public class ExitObservatory : MonoBehaviour
{
    void OnMouseDown()
    {
        // Close the application.
        Application.Quit();

        // Display message in Unity Editor for testing.
        Debug.Log("Game is closing...");
    }
}
