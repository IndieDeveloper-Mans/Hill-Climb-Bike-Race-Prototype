using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] WheelJoint2D _backWheel;
    [Space]
    [SerializeField] WheelJoint2D _frontWheel;
    [Space]
    [SerializeField] float _xMovement;
    [Space]
    [SerializeField] float _speed;
    [Space]
    [SerializeField] float _screenWidth;

    [Header("Distance")]
    [SerializeField] float _distanceTravelled;
    [Space]
    [SerializeField] FloatVariable _distanceSO;
    [Space]
    [SerializeField] Vector3 _startPosition;

    [Header("Wheelie")]
    [SerializeField] BoolVariable _boolSO;
    [Space]
    [SerializeField] float _wheelieTimer;
    [Space]
    [SerializeField] float _wheelieDetectDelay;
    [Space]
    [SerializeField] CircleCollider2D _frontWheelCollider;
    [Space]
    [SerializeField] EdgeCollider2D _ground;

    void Start()
    {
        _startPosition = transform.position;

        _wheelieTimer = _wheelieDetectDelay;

        _screenWidth = Screen.width;
    }

    public void Move(float horizontalAxis)
    {
        _xMovement = horizontalAxis * _speed;
    }

    private void Update()
    {
        #region  Directives
#if UNITY_EDITOR
        Move(-Input.GetAxisRaw("Horizontal"));
#endif

#if UNITY_STANDALONE
                 Move(-Input.GetAxisRaw("Horizontal"));
#endif
        #endregion

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;

            float accelerate = 0;

            if (mousePos.x > _screenWidth / 2)
            {
                accelerate = Mathf.Lerp(accelerate, -1, _speed * Time.deltaTime);

                Move(accelerate);
            }

            if (mousePos.x < _screenWidth / 2)
            {           
                accelerate = Mathf.Lerp(accelerate, 1, _speed * Time.deltaTime);

                Move(accelerate);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Move(0);
        }
    }

    void FixedUpdate()
    {
        if (_xMovement == 0)
        {
            _backWheel.useMotor = false;

            _frontWheel.useMotor = false;
        }
        else
        {
            _backWheel.useMotor = true;

            _frontWheel.useMotor = true;

            JointMotor2D motor = new JointMotor2D { motorSpeed = _xMovement, maxMotorTorque = _backWheel.motor.maxMotorTorque };

            _backWheel.motor = motor;

            _frontWheel.motor = motor;
        }

        CalculateTraveledDistance();

        DetectWheeling();
    }

    void CalculateTraveledDistance()
    {
        _distanceTravelled = Vector3.Distance(transform.position, _startPosition);

        _distanceSO.Value = _distanceTravelled;
    }

    void DetectWheeling()
    {
        if (_frontWheelCollider.IsTouching(_ground))
        {
            _boolSO.Value = false;

            _wheelieTimer = _wheelieDetectDelay;
        }
        else
        {
            _wheelieTimer -= Time.deltaTime;

            if (_wheelieTimer < 0)
            {
                _boolSO.Value = true;
            }
        }
    }
}