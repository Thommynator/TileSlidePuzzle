using UnityEngine;

public class MoveUpCommand : Command
{
    public override bool Execute()
    {
        return RasterScript.current.MoveUp();
    }

    protected override System.Type GetInverseCommandType()
    {
        return typeof(MoveDownCommand);
    }
}