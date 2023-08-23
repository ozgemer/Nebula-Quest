using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private static int health = 3;
    private int maxHealth = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        UpdateHealth(0);
    }

    public void UpdateHealth(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 3);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)            
                hearts[i].sprite = fullHeart;            
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public int getCurrentHealth()
    {
        return health;
    }

    public void TakeDamage()
    {
        UpdateHealth(-1);
    }

    public void AddLife()
    {
        UpdateHealth(1);
    }
}
