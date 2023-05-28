using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkoPoloGenerator : MonoBehaviour
{
    private const string MARKO_TEXT = "Marko";
    private const string POLO_TEXT = "Polo";

    [SerializeField] private TextMeshProUGUI displayText = null;
    [SerializeField] private Scrollbar scrollbarHandler = null;
    [SerializeField] private bool resetScrollPositionAfterGeneration = true;

    private int numbersCount = 100;

    public void SetNumbersCount(string _numberCountToSet)
    {
        if (int.TryParse(_numberCountToSet, out int _parsedResult) == false)
        {
            Debug.LogError("MarkoPoloGenerator :: Int parsing failed! Check input...");
            return;
        }

        numbersCount = _parsedResult;
    }

    public void GenerateNumbers()
    {
        string _displayText = "";

        for(int i = 0; i < numbersCount; i++)
        {
            int _currentNumber = i + 1;
            string _markoText = isNumberMarko(_currentNumber) == true ? MARKO_TEXT : "";
            string _poloText = isNumberPolo(_currentNumber) == true ? POLO_TEXT : "";
            _displayText += $"{_currentNumber} {_markoText}{_poloText}\n";
        }

        if (displayText == null)
        {
            return;
        }

        displayText.text = _displayText;

        if (scrollbarHandler == null || resetScrollPositionAfterGeneration == false)
        {
            return;
        }

        scrollbarHandler.value = 1f;
    }

    private bool isNumberMarko(int _numberToCheck)
    {
        return _numberToCheck % 3 == 0;
    }

    private bool isNumberPolo(int _numberToCheck)
    {
        return _numberToCheck % 5 == 0;
    }
}
