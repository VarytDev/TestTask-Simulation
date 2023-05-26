using UnityEngine;

public class AgentSelectionHandler : MonoBehaviour, IHoverable, ISelectable
{
    public delegate void AgentSelectionDelegate(bool _isSelected);
    public AgentSelectionDelegate OnAgentSelectionChanged;

    [Header("References")]
    [SerializeField] private Outline outlineComponent = null;

    [Header("Selection Settings")]
    [SerializeField] private Color hoverColor = Color.red;
    [SerializeField] private Color selectedColor = Color.yellow;

    private bool isSelected = false;

    public void OnSelected()
    {
        isSelected = true;
        outlineComponent.OutlineColor = selectedColor;

        OnAgentSelectionChanged?.Invoke(isSelected);
    }

    public void OnDeselected()
    {
        isSelected = false;
        outlineComponent.OutlineColor = hoverColor;
        outlineComponent.enabled = false;

        OnAgentSelectionChanged?.Invoke(isSelected);
    }

    public void OnPointerEnter()
    {
        outlineComponent.enabled = true;
    }

    public void OnPointerExit()
    {
        if (isSelected == true)
        {
            return;
        }

        outlineComponent.enabled = false;
    }
}
