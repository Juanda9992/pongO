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
    [SerializeField] private Vector2 ballSpeed;
    [SerializeField] private Player_Control player_Control;

    private void Start() 
    {
        player_Control = GetComponent<Player_Control>();   
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ballSpeed);
        sensor.AddObservation(ballRb.transform.position);
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<float> discreteActions = actions.ContinuousActions;
        Debug.Log(discreteActions);
        ProcessActions(discreteActions);
    }

    public void ProcessActions(ActionSegment<float> ActionsToProcess)
    {
        float yAxis = ActionsToProcess[0];
        Debug.Log(yAxis);
        rb.velocity = transform.up * player_Control.speed * yAxis;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> discreteActions = actionsOut.ContinuousActions;
        discreteActions[0] = Input.GetAxisRaw("Vertical");
        ProcessActions(discreteActions);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log(ballRb);
        ballRb.transform.position = Vector2.zero;
        ballRb.velocity = Vector2.zero;
        transform.position = new Vector2(transform.position.x, Random.Range(-3.6f,3.6f));
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
            SetReward(1.0f);
        }    
    }
}
