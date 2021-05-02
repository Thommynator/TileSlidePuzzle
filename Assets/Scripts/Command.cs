using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public abstract bool Execute();
    protected abstract System.Type GetInverseCommandType();

    public bool IsInverseOf(Command other)
    {
        return other.GetType() == GetInverseCommandType();
    }

}