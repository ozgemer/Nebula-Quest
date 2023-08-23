using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GemsCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI counter;
    private static int gems = 0;

    public void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
            gems = 0;

        UpdateGemsText();
    }

    public int GemsCount()
    {
        return gems;
    }

    public void Add()
    {
        gems++;
        UpdateGemsText();
    }

    public void UpdateGemsText()
    {
        if(counter)
            counter.text = gems.ToString();
    }
}
