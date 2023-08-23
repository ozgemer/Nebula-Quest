using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToTargetAgent : Agent
{
    [SerializeField]
    private GameObject target, ground, env ,indicator;
    public Material closer, further, fail, success;
    public float moveSpeed = 5f;
    private float MAX_DISTANCE = 5;
    private float last_distance;
    private float time;
    // agent "Start" function for each epoch
    public override void OnEpisodeBegin()
    {
        time = 100;
        last_distance = MAX_DISTANCE;
        transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 0, Random.Range(-3.5f, 3.5f));
        target.transform.localPosition = new Vector3(Random.Range(-3.5f, 3.5f), 0, Random.Range(-3.5f, 3.5f));

        //makes so AI learns to go to Target instead of going generally right
        //env.transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
        //transform.rotation = Quaternion.identity;
    }

    // what the agent can "see" / "know" every Update
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(time);
    }

    // when agent takes an action
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //use local position so we can duplicate env and have AI train faster
        //(if we use normal position all agent will go to the same position)
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        float current_distance = Vector3.Distance(transform.position, target.transform.position);
        float multi = 1;
        if (current_distance >= last_distance)
        {
            ground.GetComponent<MeshRenderer>().material = further;
            multi = -2f;
        }/*
        else if(current_distance == last_distance)
        {
            multi = 0;
        }*/
        else
            ground.GetComponent<MeshRenderer>().material = closer;
        AddReward(multi / (current_distance / MAX_DISTANCE));
        time--;
        time = Mathf.Clamp(time, 0, 100);
        last_distance = current_distance;
    }

    // makes us able to interact
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            AddReward(250 + time);
            indicator.GetComponent<MeshRenderer>().material = success;
            EndEpisode();            
        }        
        else if (other.TryGetComponent(out Wall wall))
        {
            AddReward(-5);
            indicator.GetComponent<MeshRenderer>().material = fail;
            EndEpisode();
        }
    }
}
