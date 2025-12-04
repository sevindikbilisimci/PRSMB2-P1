using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Animator animator;
    public Transform cameraTransform;

    float rotationVelocity;
    public float rotationSpeed = 10f;

    // Gravity & Jump
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    float verticalVelocity;
    bool isGrounded;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 inputDirection = new Vector3(h, 0f, v).normalized;

        // --- Ground Check ---
        isGrounded = cc.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // yere yapýþýk kalsýn
        }

        // --- Jump ---
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravity ---
        verticalVelocity += gravity * Time.deltaTime;

        // --- Movement ---
        Vector3 moveDir = Vector3.zero;

        if (inputDirection.magnitude >= 0.1f)
        {
            animator.SetBool("isWalking", true);

            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            moveDir = camForward * inputDirection.z + camRight * inputDirection.x;

            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Dikey hareketi ekle
        moveDir.y = verticalVelocity;

        // Gerçek hareket
        cc.Move(moveDir * Time.deltaTime);
    }
}
