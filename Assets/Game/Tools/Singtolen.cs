using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singtolen <T>  where T : class,new()
{
   private static T instance;
    private static readonly object obj = new object();
   public static T Instance
   {
       get
       {
           if (instance == null)
           {
                lock (obj)
                {
                    instance = new T();
                }             
           }
           return instance;
       }
   }
}
