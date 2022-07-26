
using UnityEngine;
using UnityEngine.AI;


public class FollowPlayer : MonoBehaviour
{
    public Transform target;
	NavMeshAgent nav;
	private Animator _animator;
	
	void Start()
    {
        nav = GetComponent<NavMeshAgent>();
		_animator = gameObject.GetComponent<Animator>();
		nav.SetDestination(target.position);
	}

    void Update()
    {
		//Debug.Log("remaing distance"+nav.remainingDistance);

		//Debug.Log(Vector3.Distance(new Vector3(nav.destination.x,0,nav.destination.z), new Vector3(target.position.x,0,target.position.z)));
		nav.SetDestination(target.position);
		
		//if (nav.remainingDistance>nav.stoppingDistance-0.002f)
		if (nav.velocity.magnitude > 0.01f)
        {
			_animator.SetFloat("Speed", 2);
		}
		else
        {

			_animator.SetFloat("Speed", 0);
		}
	}
	

}
