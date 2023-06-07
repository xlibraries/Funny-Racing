using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource carAudioSource;
    public float minSpeed = 0f;
    public float maxSpeed = 10f;
    public float minVolume = 0.2f;
    public float maxVolume = 1f;

    private bool isCarMoving = false;
    private Rigidbody2D carRigidbody;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // Set the Instance reference when the script is initialized
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    private void Start()
    {
        // Get the Rigidbody2D component of the car
        carRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the car is moving
        if (isCarMoving)
        {
            // Calculate the current speed of the car
            float currentSpeed = carRigidbody.velocity.magnitude;

            // Map the speed to a volume range
            float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, currentSpeed);
            float targetVolume = Mathf.Lerp(minVolume, maxVolume, normalizedSpeed);

            // Set the volume of the audio source
            carAudioSource.volume = targetVolume;

            // Check if the audio source is not already playing
            if (!carAudioSource.isPlaying)
            {
                // Start playing the audio source
                carAudioSource.Play();
            }
        }
        else
        {
            // Stop playing the audio source
            carAudioSource.Stop();
        }
    }

    public void SetCarMoving(bool moving)
    {
        isCarMoving = moving;
    }
}
