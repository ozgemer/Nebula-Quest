using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDInteract : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    public void UpdateText(string message)
    {
        promptText.text = message;
    }
}
