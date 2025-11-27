using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startPanel; // Panel Start Menu
    public GameObject player;     // Player / AI

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // หยุดเกมและปิด Player
        Time.timeScale = 0f;
        if(player != null) player.SetActive(false);

        if(startPanel != null) startPanel.SetActive(true);
    }

    public void StartGame()
    {
        if(startPanel != null) startPanel.SetActive(false);
        if(player != null) player.SetActive(true);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
