using UnityEngine;

public enum SFXType {
    //player
    PlayerAttack,
    PlayerJump,
    PlayerAttackHit,
    PlayerBlock,
    PlayerFall,
    PlayerDeath,
    PlayerLandGround,
    PlayerLandWater,
    PlayerStep,
    PlayerStepWater,
    PlayerTakeDamage,
    PlayerUmbrella,

    //environment
    Balloon,
    Bounce,
    Cauldron,
    CauldronFall,
    Checkpoint,
    CandyCollected,
    PumpkinCollected,
    DoorOpen,
    VaseBreak,
    Snore,

    //UI
    MaxHpGained,
    ButtonHover,
    ButtonPress,

    //enemies
    Poof,

    GhostAttack,
    GhostLoop,
    GhostDeath,
    GhostMunch,

    WizardAttack,
    WizardAttackHit,
    WizardCharge,
    WizardTakeDamage,
}
