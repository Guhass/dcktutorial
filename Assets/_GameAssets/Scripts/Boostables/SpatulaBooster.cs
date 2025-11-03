using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostables
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;

    [Header("Settings")]
    [SerializeField] private float _jumpForce;

    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if (_isActivated) { return; }
        PlayBoostAnimation();
        Rigidbody _playerRigidbody = playerController.GetPlayerRigidbody();

        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetActivation), 0.2f);
    }

    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
    }

    private void ResetActivation()
    {
        _isActivated = false;
    }
}
