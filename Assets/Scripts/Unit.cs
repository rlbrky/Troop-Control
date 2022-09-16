using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    GameObject selectedUnit;
    [SerializeField] float speed;
    [SerializeField] float distanceToDestination;
    NavMeshAgent agent;
    UnitAnimationController animationController;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float DistanceToDestination
    {
        get { return distanceToDestination; }
        set { distanceToDestination = value; }
    }
    private void Awake()
    {
        animationController = GetComponent<UnitAnimationController>();
        agent = GetComponent<NavMeshAgent>();
        selectedUnit = transform.Find("Selected").gameObject;
        SetSelectedVisibility(false);
    }
    private void Update()
    {
        agent.speed = speed;
        agent.stoppingDistance = distanceToDestination;
        animationController.SetAnimationSpeed(agent.velocity.magnitude);
    }
    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void SetSelectedVisibility(bool visible)
    {
        selectedUnit.SetActive(visible);
    }
}
