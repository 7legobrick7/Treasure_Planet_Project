using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpLog : MonoBehaviour
{
    void OnMouseDown()
    {
        // Load the third scene in Build Settings.
        SceneManager.LoadScene(2);
    }
}
