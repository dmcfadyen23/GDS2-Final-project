using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        if (PlayerSpawn.Instance != null)
        {
            transform.position = PlayerSpawn.Instance.GetSpawnPosition();
        }
    }
}
