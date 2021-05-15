﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class MonsterFiniteStateMachine : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerInfo player;
    private Animator animator;

    public AmbushPoints ambushPoints;


    public enum State { ChasePlayer, PrepareForNextAmbush, WaitForAmbush, Ambushing, HeardSound, ChaseSound, ResumeInterruptedAction };
    public State state;
    public State interruptedState;

    public Vector3 soundDestination;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        state = State.PrepareForNextAmbush;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();


    }

    public void SoundTriggered(Vector3 soundPos)
    {
        soundDestination = soundPos;
        interruptedState = new State();
        interruptedState = state;

        UpdateState(State.HeardSound);

        agent.isStopped = true;
        animator.SetBool("SoundAlert", true);
        animator.SetBool("Running", false);
        StartCoroutine(TimeSoundAlert());

    }

    private void Update()
    {

        switch (state)
        {
            case State.ChasePlayer:

                agent.SetDestination(player.transform.position);
                break;


            //Currently not moving, waiting for the player to trigger a reaction
            case State.WaitForAmbush:



                if (ambushPoints.points[ambushPoints.currentID].triggered)
                {
                    AmbushPoint ambush = ambushPoints.points[ambushPoints.currentID];

                    agent.isStopped = false;

                    if (ambush.type == AmbushPoint.Type.Run)
                    {
                        animator.SetBool("Running", true);

                        agent.SetDestination(ambush.secondRunPoint.transform.position);

                        UpdateState(State.Ambushing);
                    }
                    else if (ambush.type == AmbushPoint.Type.Ambush)
                    {


                        print("TriggerAmbush");
                    }

                    if (ambushPoints.points.Count > ambushPoints.currentID) ambushPoints.currentID++;
                    else print("Reach all possible");

                }

                break;


            //Currently walking to the predefined ambush point
            case State.Ambushing:

                if (agent.remainingDistance < 0.1f)
                {
                    agent.SetDestination(ambushPoints.points[ambushPoints.currentID].transform.position);

                    UpdateState(State.PrepareForNextAmbush);
                }

                break;

            //Currently walking to the next point to ambush
            case State.PrepareForNextAmbush:

                if (agent.remainingDistance < 0.1f)
                {
                    agent.isStopped = true;
                    animator.SetBool("Running", false);
                    UpdateState(State.WaitForAmbush);
                }

                break;

            case State.HeardSound:

                //Waiting for animation to finish

                break;

            case State.ChaseSound:

                StopAllCoroutines();

                if (agent.remainingDistance < 0.1f)
                {
                    UpdateState(State.ResumeInterruptedAction);

                }

                break;

            case State.ResumeInterruptedAction:

                if (interruptedState == State.WaitForAmbush)
                {
                    UpdateState(State.Ambushing);
                }
                else
                {
                    UpdateState(interruptedState);
                }

                break;
        }

    }

    public IEnumerator TimeSoundAlert()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SoundAlert"))
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            print(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return new WaitForSeconds(0.3f);
        }

        animator.SetBool("SoundAlert", false);
        animator.SetBool("Running", true);
        UpdateState(State.ChaseSound);
        agent.isStopped = false;
        agent.SetDestination(soundDestination);


    }





    public void UpdateState(State _state)
    {
        state = _state;
    }


}
