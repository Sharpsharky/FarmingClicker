namespace Utils
{
    using UnityEngine;

    public static class TransformExtensions
    {
        public static void Clear(this Transform source)
        {
            for(int i = source.childCount - 1; i >= 0; i--)
            {
                var child = source.GetChild(i);
                child.SetParent(null);
                Object.Destroy(child.gameObject);
            }
        }
    }
}