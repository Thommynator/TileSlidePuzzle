using UnityEngine;

public class MoveDownCommand : Command
{
    public override bool Execute()
    {
        return RasterScript.current.MoveDown();
    }

    protected override System.Type GetInverseCommandType()
    {
        return typeof(MoveUpCommand);
    }


}