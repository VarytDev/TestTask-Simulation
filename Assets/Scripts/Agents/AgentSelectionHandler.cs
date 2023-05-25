using UnityEngine;

public class AgentSelectionHandler : MonoBehaviour, IHoverable, ISelectable
{
    [SerializeField] private Outline outlineComponent = null;

    private bool isSelected = false;

    public void OnSelected()
    {
        isSelected = true;
        outlineComponent.OutlineColor = Color.yellow;
    }

    public void OnDeselected()
    {
        isSelected = false;
        outlineComponent.OutlineColor = Color.red;
        outlineComponent.enabled = false;
    }

    public void OnPointerEnter()
    {
        Debug.Log("ENTER");
        outlineComponent.enabled = true;
    }

    public void OnPointerExit()
    {
        Debug.Log("Exit");

        if (isSelected == true)
        {
            return;
        }

        outlineComponent.enabled = false;
    }
}
