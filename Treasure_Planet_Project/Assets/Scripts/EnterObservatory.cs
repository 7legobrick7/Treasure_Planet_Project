
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterObservatory : MonoBehaviour
{
    void OnMouseDown()
    {
        // Load the second scene in Build Settings.
        SceneManager.LoadScene(1);
    }
}
