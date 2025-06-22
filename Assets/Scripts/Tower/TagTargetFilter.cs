using UnityEngine;

public class TagTargetFilter : ITargetFilter
{
    private readonly string tag;

    public TagTargetFilter(string tag)
    {
        this.tag = tag;
    }

    public bool IsTargetValid(GameObject target)
    {
        return target.CompareTag(tag);
    }
}
