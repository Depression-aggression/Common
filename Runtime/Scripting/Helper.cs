using System.Collections.Generic;
using UnityEngine;

namespace FD.Scripting
{
    public class Helper
    {
        public static List<int> GenerateIndexList(int lenght)
        {
            var indexList = new List<int>();

            int i = 0;
            while (i < lenght)
            {
                indexList.Add(i);
                i++;
            }

            return indexList;
        }

        public static void ShuffleList<T>(List<T> inputList)
        {
            if (inputList.Count < 1) 
                return;
            
            for (var i = 0; i < inputList.Count; i++)
            {
                var swapId = Random.Range(0, inputList.Count);

                if (swapId == i) 
                    continue;
                
                var tempElement = inputList[swapId];
                inputList[swapId] = inputList[i];
                inputList[i] = tempElement;
            }
        }
    }
}
