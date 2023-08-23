using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Image fuelBar;
    public Image backFuelBar;
    public float chipSpeed = 2f;
    public float maxFuel;
    private float fuel,
        fill , fFrac, percent, lerpTimer;

    void Awake()
    {
        fuel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
        UpdateFuel();
    }

    private void UpdateFuel()
    {
        fill = fuelBar.fillAmount;
        fFrac = fuel / maxFuel;
        if(fill < fFrac)
        {
            backFuelBar.fillAmount = fFrac;
            lerpTimer += Time.deltaTime;
            percent = lerpTimer / chipSpeed;
            percent *= percent;
            fuelBar.fillAmount = Mathf.Lerp(fill, fFrac, percent);
        }

    }

    public void AddFuel()
    {
        fuel ++;
        lerpTimer = 0f;
    }

    public bool isFull()
    {
        return fuel == maxFuel;
    }
}
