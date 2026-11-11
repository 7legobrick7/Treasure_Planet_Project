
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void OnMouseDown()
    {
        // Load the third scene in Build Settings.
        SceneManager.LoadScene(0);
    }
}
