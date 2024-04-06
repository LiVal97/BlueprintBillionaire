using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float money = 0f;
    [HideInInspector] public float incomePerSecond = 0f;
    private float timer;
    private float second = 1.0f;
    public TMP_Text moneyText;
    public TMP_Text incomePerSecondText;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        
        timer += Time.deltaTime;
        if (timer >= second)
        {
            timer = 0f;
            money += incomePerSecond;
        }
        
       
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
}
