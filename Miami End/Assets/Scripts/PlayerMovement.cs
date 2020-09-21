using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private float _inputX;
  private float _inputZ;
  private float _verticalVel;

  private Vector3 _moveVector;
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
     _inputX = Input.GetAxis("Horizontal");
     _inputZ = Input.GetAxis("Vertical");
 
     
     InputMagnitude();
     RegularMovement();
    
      
   }
    
   void RegularMovement()
   {
      _moveVector = new Vector3(_inputX , 0 , _inputZ);
      _moveVector = _mainCam.transform.TransformDirection(_moveVector);

      if((_inputX > 0.1f || _inputZ > 0.1f) || (_inputX < -0.1f || _inputZ < -0.1f))
      {
         var rotation = Quaternion.LookRotation(new Vector3(_moveVector.x , 0 , _moveVector.z));
         transform.rotation = Quaternion.Slerp(transform.rotation , rotation , Time.deltaTime * _rotationSpeed);
      }

      //Jump       
        
     if(_controller.isGrounded && Input.GetKeyDown(KeyCode.Space) )
     {
         _anim.SetTrigger("Jump");
         _verticalVel = _jumpForce;
              
    }
    else
     {
       _verticalVel -= _gravity;
     }

      Vector3 velocity = _moveVector * _moveSpeed;
      velocity.y = _verticalVel;

     _controller.Move(velocity * Time.deltaTime);
     
   }
   void InputMagnitude()
   {
      
     _anim.SetFloat("InputZ",_inputZ,0.0f,Time.deltaTime * 2f);
     _anim.SetFloat("InputX",_inputX,0.0f,Time.deltaTime * 2f);

     _speed = new Vector2(_inputX,_inputZ).normalized.sqrMagnitude;
    
     if(_speed > _allowPlayerRotation)
       {
        _anim.SetFloat("InputMagnitude", _speed, 0.0f , Time.deltaTime);
       }
     else if(_speed < _allowPlayerRotation)
       {
        _anim.SetFloat("InputMagnitude", _speed, 0.0f , Time.deltaTime);
        
       }

    
   }
  

}                                                                                                                                                                                                                                                                                  