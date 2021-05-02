using UnityEngine;

public class MoveLeftCommand : Command
{
    public override bool Execute()
    {
        return RasterScript.current.MoveLeft();
    }

    protected override System.Type GetInverseCommandType()
    {
        return typeof(MoveRightCommand);
    }

}