using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private ICharacterState movingState;
    private CharacterStateContext characterStateContext;

    private void Start()
    {
        this.characterStateContext = new CharacterStateContext(this);

        movingState = this.gameObject.AddComponent<CharacterMovingState>();
    }
}