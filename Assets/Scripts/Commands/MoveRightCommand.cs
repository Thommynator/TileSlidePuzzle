using UnityEngine;

public class MoveRightCommand : Command
{
    public override bool Execute()
    {
        return RasterScript.current.MoveRight();
    }

    protected override System.Type GetInverseCommandType()
    {
        return typeof(MoveLeftCommand);
    }


}