using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private float _inputX;
  private float _inputZ;
  private float _verticalVel;
  
  private Vector3 _moveDirection;
  private Vector3 _moveVector;
  private bool _bloackRotation = false;
  private bool _isGrounded;
  private Animator _anim;
  private CharacterController _controller;

  [SerializeField]private float _gravity = 9.81f;
  [SerializeField]private float _allowPlayerRotation;
  [SerializeField] private float _rotationSpeed;
  [SerializeField] private float _speed;
  [SerializeField] private float _moveSpeed;
  [SerializeField] private float _jumpForce;
  [SerializeField] private Camera _mainCam;
  
  

   void Start()
   {
      _anim = this.GetComponent<Animator>();
      
      _controller = this.GetComponent<CharacterController>();

      Cursor.lockState = CursorLockMode.Locked;

   }

   void Update()
   {
     
     InputMagnitude();
     Move();
      
   }

  
    void Move()
   {
     _moveVector = new Vector3(_inputX,0, _inputZ).normalized;
      Vector3 velocity = _moveVector * _moveSpeed;

     _isGrounded = _controller.isGrounded;
     if(_isGrounded)
    {
      if(Input.GetKeyDown(KeyCode.Space))
      {
         _anim.SetTrigger("Jump");
         _verticalVel = _jumpForce;
         

      }
      else
       {
          
         _verticalVel -= _gravity;
         
       }
    }
    else
     {
      _verticalVel -= _gravity;
     }
     
      velocity.y = _verticalVel;
   
      _controller.Move(velocity * Time.deltaTime);
   }
   void PlayerMoveCalc()
    {
       _inputX = Input.GetAxis("Horizontal");
       _inputZ = Input.GetAxis("Vertical");
 
         var forward = _mainCam.transform.forward;
         var right = _mainCam.transform.right;
         forward.y = 0f;
         right.y = 0f;
         forward.Normalize();
         right.Normalize();
         
         _moveDirection = forward * _inputZ + right * _inputX;
         
         if(_bloackRotation == false)
         {
          transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection),_rotationSpeed);
         }
    
    }

   void InputMagnitude()
   {
      _inputX = Input.GetAxis("Horizontal");
      _inputZ = Input.GetAxis("Vertical");
 
     _anim.SetFloat("InputZ",_inputZ,0.0f,Time.deltaTime * 2f);
     _anim.SetFloat("InputX",_inputX,0.0f,Time.deltaTime * 2f);

     _speed = new Vector2(_inputX,_inputZ).normalized.sqrMagnitude;
    
     if(_speed > _allowPlayerRotation)
       {
        _anim.SetFloat("InputMagnitude", _speed, 0.0f , Time.deltaTime);
        PlayerMoveCalc();
       }
     else if(_speed < _allowPlayerRotation)
       {
        _anim.SetFloat("InputMagnitude", _speed, 0.0f , Time.deltaTime);
        
       }

    
   }
  

}                                                                                                                                                                                                                                                                                  