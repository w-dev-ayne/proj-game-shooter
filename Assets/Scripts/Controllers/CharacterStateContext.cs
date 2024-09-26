public class CharacterStateContext
{
    public ICharacterState CurrentState { get; set; }

    private readonly CharacterController characterController;

    // 생성자
    public CharacterStateContext(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    // 상태 전환
    public void Transition()
    {
        CurrentState.Handle(characterController);
    }

    // 상태 전환
    public void Transition(ICharacterState state)
    {
        CurrentState = state;
        CurrentState.Handle(characterController);
    }
}