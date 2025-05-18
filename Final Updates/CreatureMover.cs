using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class CreatureMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 2f;
        public float runSpeed = 5f;
        public float rotationSmoothTime = 0.2f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;
        public Transform cameraTransform;

        [Header("Animation Parameters")]
        public string verticalParam = "Vert"; // Forward/backward
        public string stateParam = "State";   // Walk/Run blend
        public string jumpParam = "Jump";    // Jump trigger
        public float animSmoothTime = 0.1f;

        private CharacterController controller;
        private Animator animator;
        private Vector2 inputAxis;
        private Vector3 lookTarget;
        private bool isRunning;
        private bool shouldJump;
        private float turnSmoothVelocity;
        private float currentAnimSpeed;
        private Vector3 verticalVelocity;
        private bool isGrounded;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            if (cameraTransform == null) cameraTransform = Camera.main?.transform;
        }

        private void Update()
        {
            if (cameraTransform == null) return;

            HandleGroundCheck();
            HandleJump();
            HandleMovement();
            HandleAnimation();
        }

        private void HandleGroundCheck()
        {
            isGrounded = controller.isGrounded;
            if (isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -0.5f;
            }
        }

        private void HandleJump()
        {
            if (shouldJump && isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                animator.SetTrigger(jumpParam);
                shouldJump = false;
            }
            
            // Apply gravity
            verticalVelocity.y += gravity * Time.deltaTime;
            controller.Move(verticalVelocity * Time.deltaTime);
        }

        private void HandleMovement()
        {
            if (inputAxis.magnitude >= 0.1f)
            {
                // Camera-relative movement
                float targetAngle = Mathf.Atan2(inputAxis.x, inputAxis.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float smoothedAngle = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    targetAngle,
                    ref turnSmoothVelocity,
                    rotationSmoothTime
                );

                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float speed = (isRunning ? runSpeed : walkSpeed) * inputAxis.magnitude;
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }

        private void HandleAnimation()
        {
            // Blend between idle/walk/run smoothly
            float targetSpeed = inputAxis.magnitude;
            currentAnimSpeed = Mathf.Lerp(
                currentAnimSpeed,
                targetSpeed,
                animSmoothTime * 10f * Time.deltaTime
            );

            animator.SetFloat(verticalParam, currentAnimSpeed);
            animator.SetFloat(stateParam, isRunning ? 1f : 0f);
            animator.SetBool("Grounded", isGrounded);
        }

        public void SetInput(in Vector2 axis, in Vector3 target, in bool isRun, in bool isJump)
        {
            inputAxis = Vector2.ClampMagnitude(axis, 1f);
            lookTarget = target;
            isRunning = isRun;
            shouldJump = isJump;
        }

        private void OnAnimatorIK()
        {
            if (lookTarget != Vector3.zero)
            {
                animator.SetLookAtPosition(lookTarget);
                animator.SetLookAtWeight(1f, 0.5f, 0.8f, 1f);
            }
        }
    }
}
