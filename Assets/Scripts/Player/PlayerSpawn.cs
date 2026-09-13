using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public static PlayerSpawn Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetSpawnPosition()
    {
        return transform.position;
    }
}
