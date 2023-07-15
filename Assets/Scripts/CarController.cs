using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] private SuspensionManager suspensionManager;
    [SerializeField] private EngineManager engineManager;

    public float speed;
    public float rotationSpeed = 10f;
    public float rotationDampening = 10f; // Adjust this value to control the dampening effect
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    public Rigidbody2D rb;
    public static bool isGrounded;

    #region Movement Variables
    private float movement = 0f;
    private float currentRotationVelocity = 0f; // Tracks the current rotational velocity
    private float rotation = 0f;
    private float rotationInput = 0f;
    private float rotationInputThreshold;
    private float clampedRotation;
    private float targetRotation;
    #endregion


    private GameManager gameManager;
    private Quaternion initialRotation;
    private bool isBackWheelTouchingGround = false;
    private bool isFrontWheelTouchingGround = false;
    private const float MaxRotation = 10f;
    private bool previousInputState = false;
    private bool isAudioPlaying = false;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }

        // Store the initial rotation of the car
        initialRotation = this.transform.rotation;
        isGrounded = false;
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    private void Update()
    {
        HandleInput();
        gameManager.DistanceCovered();
    }

    private void HandleInput()
    {
        bool currentInputState = Input.touchCount > 0;

        if (currentInputState)
        {
            Touch touch = Input.GetTouch(0);
            rotationInputThreshold = 0.8f;

            if (touch.position.x < Screen.width * 0.25f)
            {
                movement = speed; // Move backward
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
            }
            else if (touch.position.x > Screen.width * 0.75f)
            {
                movement = -speed; // Move forward
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
            }
            else
            {
                movement = 0f; // No movement
                rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
            }
        }
        else
        {
            rotationInputThreshold = 0.5f;
            rotationInput = Input.gyro.rotationRate.y * rotationInputThreshold;
            movement = 0f; // No movement
        }

        clampedRotation = Mathf.Clamp(rotationInput, -MaxRotation, MaxRotation);
        targetRotation = clampedRotation * rotationSpeed;
        rotation = Mathf.SmoothDamp(rotation, targetRotation, ref currentRotationVelocity, 1f / rotationDampening);

        // Check if input state has changed since the last frame
        if (currentInputState != previousInputState)
        {
            if (currentInputState)
            {
                AudioManager.Instance.PlaySound("WheelMovement");
                isAudioPlaying = true;
            }
            else if (isAudioPlaying && movement == 0f)
            {
                AudioManager.Instance.StopSound("WheelMovement");
                isAudioPlaying = false;
            }
            else if (!currentInputState && isAudioPlaying && movement != 0f)
            {
                AudioManager.Instance.PlaySound("WheelMovement");
            }
        }

        previousInputState = currentInputState;
    }



    private void FixedUpdate()
    {
        if (movement == 0 || GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            gameManager.FuelManagement();

            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = backWheel.motor.maxMotorTorque };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }

        if (GameManager.fuelPresent <= 0)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
            CurrencyManager.Instance.AddBaseCurrency(GameManager.distanceCovered);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        rb.AddTorque(-rotation * Time.fixedDeltaTime);

        // Check if both back and front wheels are not touching the ground
        if (!isBackWheelTouchingGround && !isFrontWheelTouchingGround)
        {
            //Debug.Log("CheckFlip()");
            CheckFlip();
            // Handle the flip result as needed
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == backWheel.GetComponentInChildren<CircleCollider2D>().gameObject)
        {
            isBackWheelTouchingGround = true;
        }
        else if (collider.gameObject == frontWheel.GetComponentInChildren<CircleCollider2D>().gameObject)
        {
            isFrontWheelTouchingGround = true;
        }

        if (collider.CompareTag("Ground"))
        {
            isGrounded = true;
            CurrencyManager.Instance.AddBaseCurrency(GameManager.distanceCovered); // Add base currency as per every 100m distance covered rule
            Debug.Log("Currency: " + CurrencyManager.Instance.GetTotalCurrency());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collider.CompareTag("Fuel"))
        {
            GameManager.fuelPresent = GameManager.fuelCapacity;
            Debug.Log("Fuel Refilled " + GameManager.fuelPresent);
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == backWheel.GetComponentInChildren<CircleCollider2D>().gameObject)
        {
            isBackWheelTouchingGround = false;
        }
        if (collider.gameObject == frontWheel.GetComponentInChildren<CircleCollider2D>().gameObject)
        {
            isFrontWheelTouchingGround = false;
        }

    }

    public int CheckFlip()
    {
        // Calculate the rotation difference between the current rotation and the initial rotation
        Quaternion currentRotation = this.transform.rotation;
        Quaternion rotationDifference = Quaternion.Inverse(initialRotation) * currentRotation;

        // Calculate the angle of rotation around the forward axis (x-axis)
        float flipAngle = Quaternion.Angle(rotationDifference, Quaternion.identity);

        // Check if a backflip or front flip was made based on the flip angle threshold
        if (flipAngle > 150f)
        {
            // Backflip detected
            Debug.Log("Backflip");
            return -1;
        }
        else if (flipAngle < -150f)
        {
            // Front flip detected
            Debug.Log("Front Flip");
            return 1;
        }

        // No flip detected
        return 0;
    }
}
