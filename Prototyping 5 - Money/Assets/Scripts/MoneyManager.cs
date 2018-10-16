using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int bank;
    public Text bankText;
    public int addmoney;
    public float moneyTimer;
    public bool loopTimer;
    public int reserves;
    public Text reserveText;
    public int cost;
    public Text costText;

    public Button hire;
    public Button coffeeButton;
    public Button sleepButton;
    public Button cursusButton;
    public Button toiletButton;

    public bool coffee;
    public bool sleep;

    public List<BuildingMangerScript> Buildings = new List<BuildingMangerScript>();

	void Start ()
    {
        addmoney = 100;
        bank = 0;
        reserves = 3;
        loopTimer = true;
        hire.onClick.AddListener(Hire);
        coffeeButton.onClick.AddListener(delegate { GainUpgrade(1); });
        sleepButton.onClick.AddListener(delegate { GainUpgrade(2); });
        cursusButton.onClick.AddListener(delegate { GainUpgrade(3); });
        toiletButton.onClick.AddListener(delegate { GainUpgrade(4); });
        reserveText.text = "Reserves: " + reserves.ToString();
        bankText.text = "Bank: " + bank.ToString();
        costText.text = "Hire - $" + cost.ToString();
    }
	
	void Update ()
    {
        if (loopTimer == true)
        {
            StartCoroutine(AddmoneyTimer(moneyTimer));
            loopTimer = false;
        }
	}

    IEnumerator AddmoneyTimer(float timer)
    {
        if (coffee == true)
        {
            timer /= 2;
        }
        if (sleep == true)
        {
            timer *= 1.5f;
        }
        yield return new WaitForSeconds(timer);
        ChangeMoney(reserves);
        Debug.Log("hoi");
        loopTimer = true;
    }

    public void ChangeMoney(float gain)
    {
        if(gain > 0)
        {
            if(sleep == true)
            {
                gain *= 2;
            }
        }
        bank += Mathf.FloorToInt(gain); ;
        bankText.text = "Bank: " + bank.ToString();
    }

    public void ChangeReserves(int i)
    {
        reserves += i;
        reserveText.text = "Reserves: " + reserves.ToString();
    }

    void GainUpgrade(int i)
    {
        if(i == 1)
        {
            if(bank > 50)
            {
                coffee = true;
                coffeeButton.gameObject.SetActive(false);
                bank -= 50;
            }
        }
        if(i == 2)
        {
            if(bank > 25)
            {
                sleep = true;
                sleepButton.gameObject.SetActive(false);
                bank -= 25;
            }
        }
        if(i == 3)
        {
            if(bank > 50)
            {
                foreach (BuildingMangerScript b in Buildings)
                {
                    b.cursus = true;
                }
                cursusButton.gameObject.SetActive(false);
                bank -= 50;
            }
        }
        if(i == 4)
        {
            if(bank > 75)
            {
                foreach (BuildingMangerScript b in Buildings)
                {
                    b.toilet = true;
                }
                toiletButton.gameObject.SetActive(false);
                bank -= 75;
            }
        }
    }

    void Hire()
    {
        if(bank < cost)
        {
            print("Not enough cash");
        }
        else
        {
            reserves += 1;
            reserveText.text = "Reserves: " + reserves.ToString();
            ChangeMoney(-cost);
            cost *= 3;
            costText.text = "Hire - $" + cost.ToString();
        }
    }

}
