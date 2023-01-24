using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{   
    [SerializeField] private float easySpeed,hardSpeed;
    private float currentBoost;
    [SerializeField] private Transform ballPos;
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    private void Start() 
    {
        if(OfflineStatsManager.instance.isHard)
        {
            currentBoost = hardSpeed;
        }
        else
        {
            currentBoost = easySpeed;
        }
    }

    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x,ballPos.position.y),currentBoost* Time.deltaTime);    
    }
}
