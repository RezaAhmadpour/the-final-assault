using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InputAction pressKey;
    [SerializeField] TextMeshProUGUI txtCurrentScore;
    [SerializeField] TextMeshProUGUI txtBestScore;
    Transform mainCam;

    void OnEnable()
    {
        pressKey.Enable();    
    }
    void OnDisable()
    {
        pressKey.Disable();
    }
    
    void Start()
    {
        //Set Camera Rotation
        mainCam = Camera.main.transform;
        int y = Random.Range(0, 360);
        mainCam.localRotation = Quaternion.Euler(0, y, 0);

        txtBestScore.text = $"Best: {PlayerPrefs.GetInt("BestScore", 0)}";
        txtCurrentScore.text = FindObjectOfType<GameManager>().CurrentScore.ToString();
    }

    void Update()
    {
        //Rotate Camera
        mainCam.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime, Space.Self);

        //Check if Any Key is Pressed To Load Gameplay Scene
        if (pressKey.triggered && pressKey.ReadValue<float>() >= 0.5f)
            FindObjectOfType<GameManager>().LoadGameplay();
    }
}
