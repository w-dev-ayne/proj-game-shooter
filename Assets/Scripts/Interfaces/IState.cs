public interface IState<T>
{
    void Enter(T cc);
    void Exit(T cc);
}