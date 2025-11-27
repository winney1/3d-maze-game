using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.WinGame();
        }
    }
}
