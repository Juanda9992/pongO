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


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ballSpeed);
        sensor.AddObservation(ballRb.transform.position);
        sensor.AddObservation(transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<int> discreteActions = actions.DiscreteActions;
        Debug.Log(discreteActions);
        ProcessActions(discreteActions);
    }

    public void ProcessActions(ActionSegment<int> ActionsToProcess)
    {
    }
}
