using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _Speed = 1f;
    [SerializeField] private Transform _rotation = default;

    public bool IsFacingUp { get =>_rotation.eulerAngles.z == 180; }
    public bool IsFacingLeft { get => _rotation.eulerAngles.z == 270; }
    public bool IsFacingDown { get => _rotation.eulerAngles.z == 0; }
    public bool IsFacingRight { get => _rotation.eulerAngles.z == 90; }

    private Transform _transform;
    private Vector3 _direction = Vector3.up;
    private Vector3 _newDirection = Vector3.up;
    private bool _positionReached = false;
    private Vector3 _previousPosition;
    private float timeElapsed = 0;
    private float interpolationRatio;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _previousPosition = _transform.position;
    }

    private void Update()
    {
        ChangeDirection();
        
        Move(_direction);
    }


    private void ChangeDirection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)
            && _newDirection != Vector3.down)
        {
            _newDirection = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) 
            && _newDirection != Vector3.right)
        {
            _newDirection = Vector3.right;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) 
            && _newDirection != Vector3.up)
        {
            _newDirection = Vector3.up;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) 
            && _newDirection != Vector3.left)
        {
            _newDirection = Vector3.left;
        }

        if(_positionReached)
        {
            _direction += _newDirection;
            ChangeRotation(_newDirection);
            _positionReached = false;
        }
    }

    private void ChangeRotation(Vector3 direction)
    {
        if(direction == Vector3.up && !IsFacingUp) _rotation.eulerAngles = new Vector3(0, 0 ,180);
        if(direction == Vector3.down && !IsFacingDown) _rotation.eulerAngles = new Vector3(0, 0 ,0);
        if(direction == Vector3.left && !IsFacingLeft) _rotation.eulerAngles = new Vector3(0, 0 ,270);
        if(direction == Vector3.right && !IsFacingRight) _rotation.eulerAngles = new Vector3(0, 0 ,90);
    }

    private void Move(Vector3 direction)
    {
        interpolationRatio = Mathf.Clamp(timeElapsed * _Speed, 0, 1);
        _transform.position = Vector3.Lerp(_previousPosition, direction, interpolationRatio);

        timeElapsed += Time.deltaTime;
        
        if (interpolationRatio == 1)
        {
            _previousPosition = _transform.position;
            timeElapsed = 0;
            _positionReached = true;
        }
    }
}
