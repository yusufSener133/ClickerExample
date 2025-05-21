using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private StoreUpdate[] storeUpdates;
    [SerializeField] private int _updatePerSecond = 5;

    public GameObject Stripes;

    [HideInInspector] public float count = 0;
    private float _nextTimeCheck = 1;
    private float _lastIncomeValue = 0;

    private void Start()
    {
        Stripes.SetActive(false);
        UpdateUI();
    }
    private void Update()
    {
        if (_nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            _nextTimeCheck = Time.timeSinceLevelLoad + (1f / _updatePerSecond);
        }
        if (_lastIncomeValue > 100f)
            Stripes.SetActive(true);

    }
    private void IdleCalculate()
    {
        float sum = 0f;
        foreach (var storeUpgrade in storeUpdates)
        {
            sum += storeUpgrade.CalculateIncomePerSecond();
            storeUpgrade.UpdateUI();
        }
        _lastIncomeValue = sum;
        count += sum / _updatePerSecond;
        UpdateUI();
    }
    public void ClickAction()
    {
        count++;
        count += _lastIncomeValue * .02f;
        UpdateUI();
    }
    public bool PurchaseAction(int cost)
    {
        if (count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        _countText.text = Mathf.RoundToInt(count).ToString();
        _incomeText.text = _lastIncomeValue.ToString() + "/s";

    }


}/**/
