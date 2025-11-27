using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject endPanel;         // Panel จบเกม
    public TextMeshProUGUI endText;     // Text แสดง YOU WIN / GAME OVER

    bool gameEnded = false;

    void Start()
    {
        if(endPanel != null)
            endPanel.SetActive(false); // เริ่มเกม Panel ปิด
    }

    void Update()
    {
        // ถ้าเกมจบ กดปุ่ม R เพื่อ Restart
        if(gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // เรียกเมื่อชนะ
    public void WinGame()
    {
        EndGame("YOU WIN");
    }

    // เรียกเมื่อแพ้
    public void GameOver()
    {
        EndGame("GAME OVER");
    }

    // ฟังก์ชันแสดง Panel จบเกม
    void EndGame(string message)
    {
        if (gameEnded) return;
        gameEnded = true;

        if(endPanel != null) endPanel.SetActive(true);
        if(endText != null) endText.text = message;

        Time.timeScale = 0f; // หยุดเกม

        // ปลดล็อกเมาส์
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ฟังก์ชัน Restart Game
    public void RestartGame()
    {
        Time.timeScale = 1f; // เริ่มเวลาใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // โหลด Scene ปัจจุบัน

        // รีล็อกเมาส์เมื่อเริ่มเกมใหม่
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
