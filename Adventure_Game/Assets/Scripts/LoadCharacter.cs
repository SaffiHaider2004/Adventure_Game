using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;

    public CameraFollow cameraFollow; // Reference to camera follow script
    public Camera mainCamera;         // Reference to Main Camera
    public FloatingJoystick joystick;
    public JumpButtonHandler jumpButton;
    public PunchButtonHandler punchButton;
    public SprintButtonHandler sprintButton;
    public PlayerUI playerUI; // This should stay on the Canvas in the scene

    void Start()
    {
        int characterIndex = PlayerPrefs.GetInt("Characters");

        if (characterIndex >= 0 && characterIndex < characterPrefabs.Length)
        {
            GameObject character = Instantiate(characterPrefabs[characterIndex], spawnPoint.position, Quaternion.identity);
            character.tag = "Player";

            // Set camera to follow
            cameraFollow.target = character.transform;
            mainCamera.transform.SetParent(character.transform);
            mainCamera.transform.localPosition = new Vector3(0f, 2f, -4f);
            mainCamera.transform.localRotation = Quaternion.Euler(10f, 0f, 0f);

            // Setup joystick movement
            PlayerJoystickMovement movement = character.GetComponent<PlayerJoystickMovement>();
            movement.joystick = joystick;
            movement.cameraTransform = mainCamera.transform;

            // Hook up buttons
            jumpButton.player = movement;
            punchButton.player = movement;

            // Hook up stats
            PlayerStats stats = character.GetComponent<PlayerStats>();
            sprintButton.playerStats = stats;

            // Hook up PlayerUI
            playerUI.playerStats = stats;
            playerUI.InitializeSliders(); // call custom method to initialize sliders
        }
        else
        {
            Debug.LogError("Character index out of range.");
        }
    }
}

