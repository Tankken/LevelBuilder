public class S_Wall : obj { }

public class Wall : EditorObjectBase
{
    public override obj GetDataToSerialize()
    {
        return new S_Wall();
    }
}
