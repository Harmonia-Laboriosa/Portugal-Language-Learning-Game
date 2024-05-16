/*using UnityEngine;
using UnityEngine.UI;

public class RotateWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Button spacealter;
    [SerializeField] public float snapAngle;
    private float currentRotation = 0f;
    private bool spacePressed = false;

    void Start()
    {
        // Add listener for button click event
        spacealter.onClick.AddListener(OnSpaceButtonClick);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || spacePressed)
        {
            RotateImage(rotationSpeed * Time.deltaTime);
        }
    }

    void RotateImage(float rotationAmount)
    {
        transform.Rotate(0f, 0f, rotationAmount);
    }

    void OnSpaceButtonClick()
    {
        spacePressed = true;
    }
}*/
using UnityEngine;
using UnityEngine.UI;

public class RotateWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Button spacealter;
    [SerializeField] public float snapAngle;
    private float currentRotation = 0f;
    private bool isRotating = false;
    public AudioSource grindSound;

    void Start()
    {
        // Add listener for button click event
        spacealter.onClick.AddListener(OnSpaceButtonClick);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || isRotating)
        {
            RotateImage(rotationSpeed * Time.deltaTime);
        }
    }

    void RotateImage(float rotationAmount)
    {
        //grindSound.Play();
        transform.Rotate(0f, 0f, rotationAmount);
    }

    void OnSpaceButtonClick()
    {
        grindSound.Stop();
        isRotating = !isRotating; // Toggle the rotation state
    }   
}