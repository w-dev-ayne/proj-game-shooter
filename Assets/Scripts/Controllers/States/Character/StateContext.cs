public class StateContext<T>
{
    public IState<T> CurrentState { get; private set; }
    private IState<T> OverlayState { get; set; }

    private readonly T controller;

    // 생성자
    public StateContext(T controller)
    {
        this.controller = controller;
    }

    // 상태 전환
    public void Transition(IState<T> state)
    {
        CurrentState?.Exit(controller);
        CurrentState = state;
        CurrentState.Enter(controller);
    }

    public void Overlay(IState<T> state)
    {
        OverlayState?.Exit(controller);
        OverlayState = state;
        OverlayState.Enter(controller);
    }
}