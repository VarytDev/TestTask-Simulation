using TMPro;
using UnityEngine;

public class TextSequencer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText = null;
    [SerializeField] string[] stringSequence = new string[0];

    private int currentString = 0;

    private void Start()
    {
        setButtonText();
    }

    public void CycleButtonText()
    {
        if (stringSequence.Length <= 0)
        {
            return;
        }

        currentString++;

        if(currentString >= stringSequence.Length)
        {
            currentString = 0;
        }

        setButtonText();
    }

    private void setButtonText()
    {
        if (stringSequence.Length <= 0 || buttonText == null)
        {
            return;
        }

        buttonText.text = stringSequence[currentString];
    }
}
