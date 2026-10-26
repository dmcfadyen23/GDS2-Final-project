using UnityEngine;

public class PlayerPositionRestore : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.TryGetRestorePosition(gameObject.scene.name, out Vector3 position))
        {
            transform.position = position;
            var body = GetComponent<Rigidbody2D>();
            if (body != null) body.position = position;
        }
    }
}
