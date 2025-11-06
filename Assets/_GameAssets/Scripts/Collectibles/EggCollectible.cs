using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        Debug.Log("Egg Collected!");
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
