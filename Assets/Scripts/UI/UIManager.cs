using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [Header("References")]
    [SerializeField] private SelectionDisplay selectionDisplayComponent = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void AddAgentToDisplay(AgentHandler _targetAgent)
    {
        if (selectionDisplayComponent == null)
        {
            return;
        }

        selectionDisplayComponent.EnableDisplay(_targetAgent);
    }

    public void RemoveAgentFromDisplay()
    {
        if (selectionDisplayComponent == null)
        {
            return;
        }

        selectionDisplayComponent.DisableDisplay();
    }
}
