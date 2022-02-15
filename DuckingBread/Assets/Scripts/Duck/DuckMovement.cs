using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DuckMovement : MonoBehaviour
{
    [Header("References:")]
    public GameObject waterSurface = null;
    [Header("Movement parameters:")]
    [SerializeField] private Vector2 minXZdestinationPoint;
    [SerializeField] private Vector2 maxXZdestinationPoint;
    [SerializeField] private float minDistanceToReachPoint = 0.5f;
    [SerializeField] private float minDistanceBetweenNextPoint = 10.0f;
    [SerializeField] private float brakeDistance = 2.0f;
    [SerializeField] private float maxTimeToReachDestination = 10.0f;
    [SerializeField] private float dashCenterDirectionOffset = 5.0f;
    [SerializeField] private LayerMask dashObstacleLayers;
    [Space(10)]
    [SerializeField] private float minSpeed = 0.8f;
    [SerializeField] private float maxSpeed = 1.1f;
    [SerializeField] private float acceleration = 3.0f;
    [SerializeField] private float splashAcceleration = 9.0f;
    [SerializeField] private float dashAcceleration = 27.0f;

    private Animator animator = null;
    private Vector3 destinationPoint = Vector3.zero;
    private NavMeshAgent navMeshAgent = null;
    private float speed = 3.0f;
    private float currentTimeToReachDestination = 0.0f;
    private DuckBrain brain = null;

    private GameObject foodTarget = null;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        brain = GetComponent<DuckBrain>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetDestinationPoint();

        navMeshAgent.acceleration = acceleration;

        animator?.SetBool("isMoving", !navMeshAgent.isStopped);
    }

    private void Update()
    {
        if (dashForced)
        {
            navMeshAgent.speed = speed * 4.5f;
            navMeshAgent.acceleration = dashAcceleration;

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                dashForced = false;
                ResetDash();
                ResetMovement();
            }
        }
        else if (customPathForced)
        {
            customPathCurrentTime += Time.deltaTime;

            navMeshAgent.speed = speed * 3.0f;
            navMeshAgent.acceleration = splashAcceleration;
            navMeshAgent.SetDestination(this.transform.position + customPathDirection);

            if (customPathCurrentTime >= customPathDuration)
            {
                customPathForced = false;
                ResetMovement();
            }
        }
        else
        {
            navMeshAgent.speed = speed;
            navMeshAgent.acceleration = acceleration;

            if (hasFoodAsDestinationPoint && foodTarget)
            {
                return;
            }

            float distanceToDestPoint = Vector3.Distance(destinationPoint, this.transform.position);

            if (distanceToDestPoint < minDistanceToReachPoint)
            {
                SetDestinationPoint();
            }
        }

        if (!dashForced)
        {
            currentTimeToReachDestination += Time.deltaTime;

            if (currentTimeToReachDestination > maxTimeToReachDestination)
            {
                destinationPoint = this.transform.position;
                //SetDestinationPoint();
                ForceDash();
            }
        }
    }

    private void SetDestinationPoint()
    {
        Vector3 previousDestinationPoint = destinationPoint;

        speed = Random.Range(minSpeed, maxSpeed);
        navMeshAgent.speed = speed;

        do
        {
            destinationPoint = GetDestinationPoint();

        } while (Vector3.Distance(previousDestinationPoint, destinationPoint) < minDistanceBetweenNextPoint || Physics.OverlapSphere(destinationPoint, 2.5f, dashObstacleLayers).Length != 0);

        if (navMeshAgent)
        {
            navMeshAgent.SetDestination(destinationPoint);
        }

        currentTimeToReachDestination = 0.0f;
    }

    private void ResetMovement()
    {
        if (foodTarget)
            navMeshAgent?.SetDestination(foodTarget.transform.position); 
        else
            navMeshAgent?.SetDestination(destinationPoint);
    }

    private Vector3 GetDestinationPoint()
    {
        Vector3 point = Vector3.zero;
        point.x = Random.Range(minXZdestinationPoint.x, maxXZdestinationPoint.x);
        point.z = Random.Range(minXZdestinationPoint.y, maxXZdestinationPoint.y);
        point.y = this.transform.position.y;

        return point;
    }

    private bool customPathForced = false;
    private float customPathCurrentTime = 0.0f;
    private float customPathDuration = 0.0f;
    private Vector3 customPathDirection = Vector3.zero;

    public void ForcePathChange(Vector3 direction, float time)
    {
        if (dashForced)
            return;

        EnableMovement();
        brain?.InterruptEating();

        direction.y = 0.0f;

        customPathForced = true;

        customPathCurrentTime = 0.0f;
        customPathDuration = time;
        customPathDirection = direction.normalized;

        navMeshAgent.SetDestination(this.transform.position + customPathDirection);
    }

    private bool hasFoodAsDestinationPoint = false;

    public bool CustomPathForced { get => customPathForced; }
    public bool DashForced { get => dashForced; }

    public void SetFoodAsDestinationPoint(GameObject food)
    {
        hasFoodAsDestinationPoint = true;
        foodTarget = food;

        if (!dashForced)
            navMeshAgent.SetDestination(foodTarget.transform.position);
    }

    public void StopFollowingToFood()
    {
        hasFoodAsDestinationPoint = false;
        foodTarget = null;
        ResetMovement();
    }

    public void DisableMovement()
    {
        animator?.SetBool("isMoving", false);
        navMeshAgent.isStopped = true;
    }

    public void EnableMovement()
    {
        animator?.SetBool("isMoving", true);
        navMeshAgent.isStopped = false;
    }

    private bool dashForced = false;
    private Vector3 dashDirection = Vector3.zero;

    private void ForceDash()
    {
        dashForced = true;
        customPathForced = false;
        brain.PlayDiveSound();
        EnableMovement();

        animator.SetTrigger("dashStartTrigger");

        dashDirection = (waterSurface.transform.position + new Vector3(Random.Range(-dashCenterDirectionOffset, dashCenterDirectionOffset), 0.0f, Random.Range(-dashCenterDirectionOffset, dashCenterDirectionOffset)) - this.transform.position);
        dashDirection.Normalize();
        dashDirection.y = 0.0f;

        navMeshAgent.SetDestination(dashDirection * 4.0f);
    }

    private void ResetDash()
    {
        animator?.SetTrigger("dashStopTrigger");

        currentTimeToReachDestination = 0.0f;

        ResetMovement();
    }
}

