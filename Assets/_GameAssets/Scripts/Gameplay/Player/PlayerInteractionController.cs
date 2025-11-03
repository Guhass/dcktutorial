using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<IBoostables>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }

}
