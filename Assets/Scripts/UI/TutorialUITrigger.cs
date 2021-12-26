using UnityEngine;

public enum TutorialType
{
    Movement,
    Dash,
    Jump,
    DoubleJump,
    AttackMelee,
    AttackThrow,
    SelfHeal,
    Interaction,
    WallJump,
    None
}

public class TutorialUITrigger : MonoBehaviour
{
    public TutorialType tutorialType;

    void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);

            if (transform.GetChild(0).gameObject.activeSelf)
            {
                if (tutorialType == TutorialType.AttackMelee) TutorialManager.Instance.hasShowAttackMeleeKey = true;
                if (tutorialType == TutorialType.AttackThrow) TutorialManager.Instance.hasShowAttackThrowKey = true;
                if (tutorialType == TutorialType.Dash)        TutorialManager.Instance.hasShowDashKey        = true;
                if (tutorialType == TutorialType.DoubleJump)  TutorialManager.Instance.hasShowDoubleJumpKey  = true;
                if (tutorialType == TutorialType.Interaction) TutorialManager.Instance.hasShowInteractionKey = true;
                if (tutorialType == TutorialType.Jump)        TutorialManager.Instance.hasShowJumpKey        = true;
                if (tutorialType == TutorialType.Movement)    TutorialManager.Instance.hasShowMoveKey        = true;
                if (tutorialType == TutorialType.SelfHeal)    TutorialManager.Instance.hasShowSelfHealKey    = true;
                if (tutorialType == TutorialType.WallJump)    TutorialManager.Instance.hasShowWallJumpsKey   = true;

            }
        }
    }
}
