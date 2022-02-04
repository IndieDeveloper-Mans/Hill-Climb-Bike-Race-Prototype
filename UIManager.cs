using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _distanceValue;
    [Space]
    [SerializeField] FloatVariable _distanceSO;
    [Space]
    [SerializeField] TextMeshProUGUI _wheelieText;
    [Space]
    [SerializeField] BoolVariable _isWheeling;
    
    private void Update()
    {
        UpdateDistanceText(_distanceSO.Value);

        ShowWheelieText();
    }

    public void UpdateDistanceText(float distance)
    {
        _distanceValue.text = distance.ToString("F");
    }

    public void ShowWheelieText()
    {
        _wheelieText.enabled = _isWheeling.Value;
    }

    private void OnDisable()
    {
        _isWheeling.Value = false;

        _distanceSO.Value = 0;
    }
}