using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("   MoneyManager")]
    private float timer;
    private float second = 1.0f;
    
    [Header("Workers")]
    private float remuneration;
    [HideInInspector] public float hirePrice;

    private GlobalManager _globalManager;

    
    // Start is called before the first frame update
    void Start()
    {
        _globalManager = FindObjectOfType<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hirePrice = 100 * MathF.Pow(2, (_globalManager.currentData.totalWorkers - 1));
        AddMoneyOverTime(_globalManager.currentData.revenuePerSecond);
    }

    public void AddMoneyOverTime(float amount)
    {
        timer += Time.deltaTime;
        if (timer >= second)
        {
            timer = 0f;
            _globalManager.currentData.money += amount;
        }
    }
    

    public void AddMoneyInstant(float amount)
    {
        _globalManager.currentData.money += amount;
    }

    public void RemoveMoney(float amount)
    {
        _globalManager.currentData.money -= amount;
    }

    public void HireWorker()
    {
        RemoveMoney(hirePrice);
        _globalManager.currentData.totalWorkers++;
        _globalManager.currentData.availableWorkers++;
    }
}
