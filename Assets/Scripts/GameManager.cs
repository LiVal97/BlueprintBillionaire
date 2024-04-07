using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("   MoneyManager")]
    [HideInInspector] public float money;
    [HideInInspector] public float incomePerSecond = 0f;
    private float timer;
    private float second = 1.0f;
    public TMP_Text moneyText;
    public TMP_Text incomePerSecondText;
    
    
    [Header("Workers")]
    public int workersNo;
    public TMP_Text workerNoText;
    private float remuneration;
    [HideInInspector] public float hirePrice;
    
    
    // Start is called before the first frame update
    void Start()
    {
        money = 10000f;
        workersNo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        hirePrice = 100 * MathF.Pow(2, (workersNo - 1));
        workerNoText.text = workersNo.ToString("#,###");
        if (money == 0)
        {
            moneyText.text = "0";
        }

        if (money > 0 )
        {
            moneyText.text = money.ToString("#,###");
        }

        if (incomePerSecond == 0)
        {
            incomePerSecondText.text = "0/Sec";
        }

        if (incomePerSecond > 0)
        {
            incomePerSecondText.text = incomePerSecond.ToString("#,###.##") + "/Sec";    
        }
        
        AddMoneyOverTime(incomePerSecond);
    }

    public void AddMoneyOverTime(float amount)
    {
        timer += Time.deltaTime;
        if (timer >= second)
        {
            timer = 0f;
            money += amount;
        }
    }

    public void AddMoneyInstant(float amount)
    {
        money += amount;
    }

    public void RemoveMoney(float amount)
    {
        money -= amount;
    }

    public void HireWorker()
    {
        RemoveMoney(hirePrice);
        
    }
    
}
