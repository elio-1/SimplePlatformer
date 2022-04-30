using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region COMPONENTS
    private Rigidbody2D rb;
    [SerializeField] private GameObject graphics;
    private Animator animator;
    #endregion

    private Vector2 _moveInputs;

    #region PLAYER DATAS

    [Header("Movements")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velPower;
    [SerializeField] private float _frictionAmount;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _coyoteTime;
    [SerializeField] private float _jumpCutMultiplier;
    [SerializeField] private float _jumpBufferTime;
    [SerializeField] private float _fallGravityMultiplier;
    [SerializeField] private float _gravityScale;
    [SerializeField] private IntVariable _bonusJump;


    [Header ("Wall Jumps")]
    [SerializeField] GameObject wallJumpSensor;
    [SerializeField] IntVariable _wallJumpLeft;
    [SerializeField] private int _nbWallJump = 3;
    [SerializeField] Vector2 _wallSensorSize;

    [Header("Ground Check Box")]
    [SerializeField] GameObject jumpSensor;
    [SerializeField] private Vector2 _groundcheckSize;
    [SerializeField] private LayerMask _groundCheckLayer;
    [SerializeField] private LayerMask _movingPlatformCheckLayer;
    [SerializeField] private LayerMask _oobLayer;

    [Header("Spawn Point")]
    [SerializeField] private Transform[] checkPoint;
    [SerializeField] DataVariable currentCheckPoint;

    #endregion

    #region private


    private bool _isJumping = false;
    private bool _wantToJump = false;
    private float _lastGroundedTime;
    private float _jumpInputTimer;
    private bool _isOnMovingPlatform = false;
    private bool _isGrounded = false;
    private bool _isHittingWall = false;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _wallJumpLeft.variable = _nbWallJump;
         _wallJumpLeft.variable = _nbWallJump;
        currentCheckPoint.intnum = 0;
        transform.position = checkPoint[0].position;
    }

    
    void Update()
    {
        #region INPUT HANDLER
        _moveInputs.x = Input.GetAxisRaw("Horizontal");
        _moveInputs.y = Input.GetAxisRaw("Vertical");
        
        #endregion
        #region RUN

        float targetSpeed = _moveInputs.x * _moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;                                             
        float movement = Mathf.Pow(Mathf.Abs(speedDif)*accelRate, _velPower) *Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);

        #region comments
        // calculer la direction et la velocite voulues
        //la dif entre les deux
        //changer le taux d'acceleration en fonction de la situation
        //donc, si targetSpeed(passe a l'absolu cad sans 'signe' -) est sup a 0.01f, donc 
        //quon veut bouger le perso, si oui on passe la val d'acceleration ou de decceleration dans 
        //la valeur accel rate.
        //mathf pow retour une val f a la puisance p...
        //appliquer la force au rb
        #endregion
        #endregion
        #region FRICTION
        //check si le joueur appuis sur un bouton et si le joueur est sur le sol 
        if(Mathf.Abs(_moveInputs.x ) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        #endregion
        Flip(_moveInputs.x);
        #region MovingPlatformCheck
        Collider2D movingPlatformCheck = Physics2D.OverlapBox(jumpSensor.transform.position, _groundcheckSize, 0, _movingPlatformCheckLayer);
        _isOnMovingPlatform = movingPlatformCheck != null;

        if (_isOnMovingPlatform)
        {
            gameObject.transform.SetParent(movingPlatformCheck.gameObject.transform);
            animator.SetBool("Jump", false);
            _wallJumpLeft.variable = _nbWallJump;


        }
        else
        {
            gameObject.transform.parent = null;
        }
        #endregion
        #region CHECK GROUND
        
        Collider2D groundDetectorCollider = Physics2D.OverlapBox(jumpSensor.transform.position, _groundcheckSize, 0, _groundCheckLayer);
        _isGrounded = groundDetectorCollider != null;
        if (_isGrounded && !_isHittingWall)
        {
            RefreshJump();
        }
        if (_isGrounded && _isHittingWall && _wantToJump)
        {
            Jump();
            
        }

        #endregion
     #region Jump
        #region Wall Jump
        Collider2D wallDectorCollider = Physics2D.OverlapBox(wallJumpSensor.transform.position, _wallSensorSize, 0, _groundCheckLayer);
        _isHittingWall = wallDectorCollider != null;
        if (_isHittingWall && _wantToJump && _wallJumpLeft.variable>0 && !_isGrounded)
        {
            _wallJumpLeft.variable -= 1;
            Jump();
        }
        #endregion
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("IsHitting wall:" + _isHittingWall + ", isGrounded:" + _isGrounded);
           _jumpInputTimer = _jumpBufferTime;
            _wantToJump = true;
        }


        if (_lastGroundedTime > 0 &&!_isJumping && _wantToJump && _jumpInputTimer>0 && !_isHittingWall)
        {
            Jump();
        }
        else if ( _bonusJump.variable > 0 && _wantToJump)
        {
            Jump();
            _bonusJump.variable -= 1;
            _wallJumpLeft.variable = _nbWallJump;
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = _gravityScale * _fallGravityMultiplier;
            
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.AddForce(Vector2.down * rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);

        }
        else
        {
            rb.gravityScale = _gravityScale;
        }

        #endregion

        #region TIMERS
        _lastGroundedTime -= Time.deltaTime;
        _jumpInputTimer -= Time.deltaTime;

        

        #endregion
    }
    void Flip(float move)
    {
        if(move < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);   
        }
        else if(move>0.1f)
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

    }
    private void Jump()
    {
        animator.SetBool("Jump", true);

        float force = _jumpForce;
        if (rb.velocity.y < 0)
        {
            force -= rb.velocity.y; // si le perso tombe on compense
        }
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        _lastGroundedTime = 0;
        _isJumping = true;
        _wantToJump = false;
       
    }
    private void RefreshJump()
    {
        _lastGroundedTime = _coyoteTime;
        _isJumping = false;
        animator.SetBool("Jump", false);
        _wallJumpLeft.variable = _nbWallJump;
        _bonusJump.variable = 0;
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.DrawWireCube(wallJumpSensor.transform.position, _wallSensorSize);
        Gizmos.DrawWireCube(jumpSensor.transform.position, _groundcheckSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            gameObject.transform.position = checkPoint[currentCheckPoint.intnum].position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
           
            collision.gameObject.SetActive(false);
            currentCheckPoint.intnum++;
        }
    }
}

    

