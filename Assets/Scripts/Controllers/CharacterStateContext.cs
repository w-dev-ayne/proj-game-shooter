public class CharacterStateContext
{
    private ICharacterState CurrentState { get; set; }
    private ICharacterState OverlayState { get; set; }

    private readonly CharacterController characterController;

    // 생성자
    public CharacterStateContext(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    // 상태 전환
    public void Transition(ICharacterState state)
    {
        CurrentState?.Exit(characterController);
        CurrentState = state;
        CurrentState.Enter(characterController);
    }

    public void Overlay(ICharacterState state)
    {
        OverlayState?.Exit(characterController);
        OverlayState = state;
        OverlayState.Enter(characterController);
    }
}