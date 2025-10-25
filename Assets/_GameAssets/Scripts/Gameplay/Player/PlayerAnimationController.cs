using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _PlayerAnimator;

    private PlayerController _PlayerController;
    private StateController _StateController;

    private void Awake()
    {
        _PlayerController = GetComponent<PlayerController>();
        _StateController = GetComponent<StateController>();
    }

    private void Start()
    {
        _PlayerController.OnplayerJumped += PlayerController_OnplayerJumped;
    }
    void Update()
    {
        SetPlayerAnimations();
    }
    private void PlayerController_OnplayerJumped()
    {
        _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }
    private void ResetJumping()
    {
        _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    private void SetPlayerAnimations()
    {
        var currentState = _StateController.GetCurrentPlayerState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;
            
            case PlayerState.Move:
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            
            case PlayerState.SlideIdle:
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
            
            case PlayerState.Slide:
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _PlayerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;


        }

    }
}
