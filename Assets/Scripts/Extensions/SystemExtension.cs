using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemExtension
{ 
   public static T As<T>(this object obj) where T:class
    {
        return obj as T;
    }
}
