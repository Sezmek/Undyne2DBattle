using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class NewMonoBehaviourScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text textMeshPro;
    public Color32 rightBottomColor; 
    public Color32 leftBottomColor; 
    private VertexGradient originalGradient;

    private void Start()
    {
        if (textMeshPro == null) textMeshPro = GetComponent<TMP_Text>();
        originalGradient = textMeshPro.colorGradient;
        rightBottomColor = new Color32(rightBottomColor.r, rightBottomColor.g, rightBottomColor.b, 255);
        leftBottomColor = new Color32(leftBottomColor.r, leftBottomColor.g, leftBottomColor.b, 255);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.colorGradient = new VertexGradient(originalGradient.topLeft, originalGradient.topRight, leftBottomColor, rightBottomColor);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.colorGradient = originalGradient;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        textMeshPro.colorGradient = originalGradient;
    }
}
