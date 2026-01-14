using UnityEngine;

public class TimeLifeDestroyer : MonoBehaviour
{
    public float LifeTime = 2f;

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
    }
}

