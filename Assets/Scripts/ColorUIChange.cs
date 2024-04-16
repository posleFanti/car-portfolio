using UnityEngine;

public class ColorUIChange : MonoBehaviour
{
    [SerializeField] private CanvasRenderer panel;
    
    private void Start()
    {
        panel.SetColor(new Color(255, 255, 255, 0));
    }

    public void ChangeColor()
    {
        panel.SetColor(new Color(255,255,255,100));
    }

    public void ResetAlpha()
    {
        panel.SetColor(new Color(255,255,255,0));
    }
}
