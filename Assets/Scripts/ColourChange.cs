using UnityEngine;

public class ColourChange : MonoBehaviour
{
    private Renderer rend;
    private MaterialPropertyBlock block;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
    }

    public void SetColour(Color c)
    {
        rend.GetPropertyBlock(block);
        block.SetColor("_BaseColor", c);   // URP/HDRP base colour
        block.SetColor("_Color", c);       // Standard shader fallback
        rend.SetPropertyBlock(block);
    }

    void Start()
    {
        SetColour(Color.cyan);
    }
}