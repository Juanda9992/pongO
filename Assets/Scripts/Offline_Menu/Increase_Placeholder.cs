using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Increase_Placeholder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPlaceHolder;
    [HideInInspector] public int textValue = 0;
    public void DecreaseValue()
    {
        if(textValue > 0)
        {
            textValue--;
        }
        textPlaceHolder.text = textValue.ToString();
    }

    public void IncreaseValue()
    {
        if(textValue < 15)
        {
            textValue++;
        }
        textPlaceHolder.text = textValue.ToString();
    }
}
