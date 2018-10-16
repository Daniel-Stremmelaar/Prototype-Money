using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMangerScript : MonoBehaviour {

    public int workers;
    public float workerMultiplier;
    public float timer;
    public float timerReset;
    public int value;
    public int minValue;
    public int maxValue;
    public int change;
    public int minChange;
    public int maxChange;
    public bool cursus;
    public bool toilet;
    public GameObject moneyManager;
    public Button neg;
    public Button pos;
    public Text wokerText;
    public Image indicator;
    public Color red;
    public Color green;
    public Color black;
    public bool reset;

	// Use this for initialization
	void Start () {
        red = new Color(255, 0, 0);
        green = new Color(0, 255, 0);
        black = new Color(0, 0, 0);
        value = Random.Range(minValue, maxValue);
        neg.onClick.AddListener(delegate { ChangeWorkers(-1); });
        pos.onClick.AddListener(delegate { ChangeWorkers(1); });
    }
	
	// Update is called once per frame
	void Update () {
        if(workers < 1)
        {
            workerMultiplier = 0;
        }
        else
        {
            workerMultiplier = 1;
            workerMultiplier += (workers - 1) / 100;
        }

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            if(reset == true)
            {
                minValue = 0;
                maxValue = 2;
                indicator.GetComponent<Image>().color = green;
                reset = false;
            }
            Revenue(value);
            value = Random.Range(minValue, maxValue);
            if(value > 0)
            {
                indicator.GetComponent<Image>().color = green;
            }
            else
            {
                indicator.GetComponent<Image>().color = red;
            }
            change = Random.Range(minChange, maxChange);
            minValue += change;
            maxValue += change;
            if(minValue < -7 && maxValue <= 0)
            {
                indicator.GetComponent<Image>().color = black;
                reset = true;
                timer = 20;
            }
            else
            {
                timer = timerReset;
            }
        }
	}

    void Revenue(float f)
    {
        f *= workerMultiplier;
        if(cursus == true)
        {
            f *= 1.20f;
        }
        if(toilet == true)
        {
            f *= 1.10f;
        }
        moneyManager.GetComponent<MoneyManager>().ChangeMoney(f);
    }

    void ChangeWorkers(int i)
    {
        if(i == 1)
        {
            if (moneyManager.GetComponent<MoneyManager>().reserves < 1)
            {
                print("not enough workers");
            }
            else
            {
                moneyManager.GetComponent<MoneyManager>().ChangeReserves(-i);
                workers += i;
                wokerText.text = workers.ToString();
            }
        }
        if(i == -1)
        {
            if(workers > 0)
            {
                workers += i;
                wokerText.text = workers.ToString();
                moneyManager.GetComponent<MoneyManager>().ChangeReserves(-i);
            }
        }
    }
}
