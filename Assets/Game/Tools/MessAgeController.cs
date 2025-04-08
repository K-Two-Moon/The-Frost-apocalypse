using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

public class MessAgeController<T>:Singtolen<MessAgeController<T>>
{
    Dictionary<int, Action<T>> messAgeList = new Dictionary<int, Action<T>>();
   public void AddLister(int id,Action<T> action)
   {
       if (messAgeList.ContainsKey(id))
       {
           messAgeList[id] += action;
       }
       else
       {
           messAgeList.Add(id, action);
       }
   }

   
   public void RemoveLister(int id,Action<T> action)
   {
       if (messAgeList.ContainsKey(id))
       {
           messAgeList[id] -= action;
           if (messAgeList[id] == null)
           {
               messAgeList.Remove(id);
           }
       }
   }

  
    public void SendMessAge(int id,T t)
    {
         if (messAgeList.ContainsKey(id))
         {
              messAgeList[id].Invoke(t);
         }
    }

   
}
