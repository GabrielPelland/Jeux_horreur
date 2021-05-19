using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class MonsterFiniteStateMachine : MonoBehaviour
{
    private NavMeshAgent agent;
    public PlayerInfo player;
    private Animator animator;

    public AmbushPoints ambushPoints;


    public enum State { ChasePlayer, PrepareForNextAmbush, WaitForAmbush, Ambushing, HeardSound, ChaseSound, ResumeInterruptedAction };
    public State state;
    public State interruptedState;

    public Vector3 soundDestination;

    private FieldOfView fov;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        state = State.PrepareForNextAmbush;
    }

    private void Start()
    {
  


    }

    bool firstTimeDetected = true;

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

    public bool StartChasing = false;

    public IEnumerator StopChasing()
    {
        yield return new WaitForSeconds(Random.Range(8, 12));
        StartChasing = false;

        if (firstTimeDetected)
        {
            GM.i.DiludeStress();
            agent.speed = 4.5f;
            firstTimeDetected = false;
        }
        

        if (state == State.ChasePlayer)
        {
           
            AmbushPoint ambush = ambushPoints.points[ambushPoints.currentID];
            agent.SetDestination(ambush.secondRunPoint.transform.position);
            UpdateState(State.Ambushing);
          

        }

        StopAllCoroutines();
    }

    private void Update()
    {

        switch (state)
        {
            case State.ChasePlayer:
                agent.SetDestination(player.transform.position);
                if (!StartChasing)
                {
                    if (firstTimeDetected)
                    {
                        GM.i.SetStress();
                        agent.speed = 3.7f;
                    } else
                    {
                        AudioManager.i.scaryENcouterFinal.Play();
                        agent.speed = 9f;
                    }

                    
                    animator.SetBool("Running", true);        
                    StartChasing = true;
                    StartCoroutine(StopChasing());
                    agent.enabled = false;
                } else
                {
                    agent.enabled = true;
                }

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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("wutt");
            agent.isStopped = true;
            agent.enabled = false;
            animator.SetBool("Running", false);
            animator.SetBool("SoundAlert", true);
            animator.Play("SoundAlert");
            GM.i.EndScreen();


            this.enabled = false;
        }
    }

    public void CheckForVisible()
    {
        if (!(state == State.ChasePlayer))
        {
            if (fov.visibleTargets.Count != 0)
            {
                if (state != State.ChasePlayer) interruptedState = state;
                UpdateState(State.ChasePlayer);
                agent.SetDestination(player.transform.position);
            }

            if (state == State.ChasePlayer)
            {
                if (fov.visibleTargets.Count == 0)
                {
                    UpdateState(State.ResumeInterruptedAction);
                }
            }
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
