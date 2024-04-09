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
    
    [Header("Workers")]
    public int workersNo;
    private float remuneration;
    [HideInInspector] public float hirePrice;

    private GlobalManager _globalManager;
    // Start is called before the first frame update
    void Start()
    {
        money = 2000f;
        workersNo = 1;
        _globalManager = FindObjectOfType<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hirePrice = 100 * MathF.Pow(2, (workersNo - 1));
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
        workersNo++;
        _globalManager.playersData.workers = workersNo;
    }
    
}
