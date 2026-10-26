using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.TryGetRestorePosition(gameObject.scene.name, out Vector3 position))
        {
            transform.position = position;
            var body = GetComponent<Rigidbody2D>();
            if (body != null) body.position = position;
        }
        else if (PlayerSpawn.Instance != null)
        {
            transform.position = PlayerSpawn.Instance.GetSpawnPosition();
        }
    }
}
