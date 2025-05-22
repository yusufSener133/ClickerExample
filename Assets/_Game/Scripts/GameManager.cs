using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private TMP_Text _incomeText;
    [SerializeField] private StoreUpdate[] storeUpdates;
    [SerializeField] private RotateAround rotateAround;
    [SerializeField] private int _updatePerSecond = 5;

    [SerializeField] private GameObject _stripes;

    [HideInInspector] public float count = 0;
    private float _nextTimeCheck = 1;
    private float _lastIncomeValue = 0;

    private void Start()
    {
        count = PlayerPrefs.GetFloat("Count", 0);
        _stripes.SetActive(false);
        UpdateUI();
    }
    private void Update()
    {
        if (_nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            _nextTimeCheck = Time.timeSinceLevelLoad + (1f / _updatePerSecond);
        }
        if (_lastIncomeValue > 20f)
        {
            _stripes.SetActive(true);
            for (int i = 1; i < 10; i++)
            {
                if (_lastIncomeValue > 50f * i)
                    rotateAround.Speed = i;
            }
        }

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
        PlayerPrefs.SetFloat("Count", count);
        _countText.text = Mathf.RoundToInt(count).ToString();
        _incomeText.text = _lastIncomeValue.ToString() + "/s";

    }


}/**/
