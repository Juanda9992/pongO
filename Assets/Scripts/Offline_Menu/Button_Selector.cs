using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Button_Selector : MonoBehaviour
{
    [SerializeField] private Button currentButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void UpdateButton(Button desiredButton)
    {
        if(currentButton != null)
        {
            currentButton.image.color = Color.white;
            buttonText.color = Color.black;
        }
        currentButton = desiredButton;
        currentButton.image.color = Color.red;
        buttonText = currentButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = Color.white;

    }
}
