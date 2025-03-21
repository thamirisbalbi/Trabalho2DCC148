using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AgentController : MonoBehaviour
{
    [SerializeField] private Transform target1; // Primeiro alvo
    [SerializeField] private Transform target2; // Segundo alvo
    
    private Transform currentTarget;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        currentTarget = target1;
        agent.destination = currentTarget.position;
    }

    void Update()
    {
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        { //se agente chegou perto do destino
            // fico alternando entre os dois alvos
            currentTarget = currentTarget == target1 ? target2 : target1;
            agent.destination = currentTarget.position;
        }
    }

    // colisão com player (bola)
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //reinicia a cena atual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
