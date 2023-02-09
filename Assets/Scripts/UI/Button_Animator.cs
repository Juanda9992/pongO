using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Button_Animator : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private bool OpenMenu = true;
    [SerializeField] private bool CanAnimate = true;
    private void Awake() 
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            Animator_Manager.instance.MakeButtonNormal(this.transform);
            if(!OpenMenu)
            {
                Audio_Manager.instance.PlaySound("UI_Click01");
            }
            else
            {
                Audio_Manager.instance.PlaySound("UI_Click");
            }
            });    
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(CanAnimate)
        {
            Animator_Manager.instance.MakeButtonBigger(this.transform);
        }
        Audio_Manager.instance.PlaySound("UI_Select");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Animator_Manager.instance.MakeButtonNormal(this.transform);
    }   
}
