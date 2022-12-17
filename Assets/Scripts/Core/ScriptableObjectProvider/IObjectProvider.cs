namespace Core.ScriptableObjectProvider
{
    using System;
    using System.Collections.Generic;

    public interface IObjectProvider<T>
    {
        T GetObject(Predicate<T> condition);
        List<T> GetObjects(Predicate<T> condition);
    }
}