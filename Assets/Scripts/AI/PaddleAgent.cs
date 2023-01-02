using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Demonstrations;
using Unity.MLAgents.Sensors;

public class PaddleAgent : Agent
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D ballRb;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private Player_Control player;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ballRb.velocity);
        sensor.AddObservation(ballTransform);
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<float> continuousActions = actions.ContinuousActions;
        rb.velocity = transform.up * Mathf.Ceil(continuousActions[0]);
        Debug.Log(continuousActions[0]);
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector2(transform.position.x,Random.Range(-6,6));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Heuristics");
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Vertical");
    }

    public void OnGameWin()
    {
        SetReward(5.0f);
        EndEpisode();
    }
    public void OnGameLosed()
    {
        SetReward(-3.0f);
        EndEpisode();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Ball"))
        {
            AddReward(1.0f);
        }  
    }
}
