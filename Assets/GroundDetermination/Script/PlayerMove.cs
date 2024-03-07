using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _grav;
    
    private float _maxAngleToTreatAsGround = 45;
    private bool _isGround;
    private Rigidbody _rb;
    private float _veloY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _veloY = _jumpPower;
            }
        }
            
        float input = Input.GetAxis("Horizontal");
        _rb.velocity = 5 * input * Vector3.right + Vector3.up * _veloY;
    }

    private void FixedUpdate()
    {
        if (_isGround && _veloY <= 0)
        {
            _veloY = 0;
        }
        else
        {
            _veloY -= _grav;
        }
        
        //_isGround = false;
    }

    private void OnCollisionStay(Collision other)
    {
        for (int i = 0; i < other.contactCount; i++)
        {
            if (Vector3.Angle(Vector3.up, other.GetContact(i).normal) < _maxAngleToTreatAsGround)
            {
                _isGround = true;
                CancelInvoke(nameof(CancelGround));
                Invoke(nameof(CancelGround), Time.fixedDeltaTime * 2);
            }
        }
    }

    void CancelGround()
    {
        _isGround = false;
    }
}
