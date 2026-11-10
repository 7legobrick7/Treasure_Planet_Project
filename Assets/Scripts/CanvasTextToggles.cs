
using UnityEngine;
using TMPro;

public class CanvasTextToggle : MonoBehaviour
{
    [Header("Toggle Settings")]
    public KeyCode toggleKey = KeyCode.U;

    private TMP_Text[] textElements;
    private bool textVisible = true;

    void Start()
    {
        // Find all TextMeshPro elements inside this Canvas.
        textElements = GetComponentsInChildren<TMP_Text>(true);

        Debug.Log("TextMeshPro elements found: " + textElements.Length);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleText();
        }
    }

    void ToggleText()
    {
        textVisible = !textVisible;

        foreach (TMP_Text text in textElements)
        {
            if (text != null)
            {
                text.enabled = textVisible;
            }
        }

        Debug.Log("Canvas Text Visibility: " + textVisible);
    }
}
