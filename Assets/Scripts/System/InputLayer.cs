namespace NoodleEater.Caravan.System
{
    public enum InputLayer
    {
        None,

        //for main menu
        ButtonNewGame,
        ButtonContinue,
        ButtonOptions,
        ButtonExitGame,

        //for options
        ButtonVideo,
        ButtonAudio,
        ButtonControl,
        ButtonExitOptions,

        //for panel video
        DropDownDisplayMode,
        DropDownResolution,
        DropDownDisplayPlayerUI,

        //for panel audio
        SliderSfxUI,
        SliderBgmUI,

        //for panel control
        ButtonMoveLeft,
        ButtonMoveRight,
        ButtonJumo,
        ButtonDash,
        ButtonSpellAttack,
        ButtonMeleeAttack,
        ButtonSelfHeal,
        ButtonInteraction,
        ButtonOpenMap,
        ButtonResetDefault,
    }
}