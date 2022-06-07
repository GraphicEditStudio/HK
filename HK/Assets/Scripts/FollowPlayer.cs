using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
	NavMeshAgent nav;

	//
	[Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 2.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float SprintSpeed = 5.335f;
	[Tooltip("How fast the character turns to face movement direction")]
	[Range(0.0f, 0.3f)]
	public float RotationSmoothTime = 0.12f;
	[Tooltip("Acceleration and deceleration")]
	public float SpeedChangeRate = 10.0f;

	[Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.50f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.28f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;

	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject CinemachineCameraTarget;
	[Tooltip("How far in degrees can you move the camera up")]
	public float TopClamp = 70.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	public float BottomClamp = -30.0f;
	[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
	public float CameraAngleOverride = 0.0f;
	[Tooltip("For locking the camera position on all axis")]
	public bool LockCameraPosition = false;
	// player
	private float _speed;
	private float _animationBlend=1;
	private float _targetRotation = 0.0f;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;

	// timeout deltatime
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;

	// animation IDs
	private int _animIDSpeed;
	private int _animIDGrounded;
	private int _animIDJump;
	private int _animIDFreeFall;
	private int _animIDMotionSpeed;

	private Animator _animator;
	private CharacterController _controller;
	private StarterAssetsInputs _input;
	private GameObject _mainCamera;

	private const float _threshold = 0.01f;
	public float moveDistance = 0.0015f;
	private bool _hasAnimator=true;
//

	// Start is called before the first frame update
	void Start()
    {
        nav = GetComponent<NavMeshAgent>();
		_animator = gameObject.GetComponent<Animator>();
		nav.SetDestination(target.position);
	}

    // Update is called once per frame
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
