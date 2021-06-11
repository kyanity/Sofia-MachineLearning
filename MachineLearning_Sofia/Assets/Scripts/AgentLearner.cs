using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentLearner : Agent
{
    private BufferSensorComponent buffer;
    private ProjectileManager projectileManager;

    public float agentSpeed;

    private void Awake()
    {
        buffer = GetComponent<BufferSensorComponent>();
        projectileManager = FindObjectOfType<ProjectileManager>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);

        foreach(GameObject projectile in projectileManager.projectiles)
        {
            if (projectile.activeInHierarchy)
            {
                float[] overseer = { projectile.transform.position.x, projectile.transform.position.y};
                buffer.AppendObservation(overseer);
            }
        }    
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
        SetReward(1f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float X = actions.ContinuousActions[0];
        transform.position += new Vector3(X, 0) * agentSpeed * Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ProjectileSphere"))
        {
            collision.gameObject.SetActive(false);
            SetReward(-1f);
            EndEpisode();
            transform.localPosition = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-.2f);
        }
    }

    private void Update()
    {
        AddReward(0.05f);
    }
}
