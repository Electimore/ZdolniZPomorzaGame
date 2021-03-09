using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public void GoToGameObject(GameObject goal)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;
    }
}
