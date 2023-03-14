using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DailyProgrammerChallenge392IntermediatePancakesort
{
    public static class Flipfront
    {

        public static IEnumerable<T> FlipfrontExecute<T>(this IEnumerable<T> array, int indexOfValues) where T : IComparable<T>
        {
            

            if (indexOfValues > array.Count()) { throw new ArgumentException(nameof(indexOfValues)); }
            var newArray = array.ToArray();

            var indexValue = indexOfValues;

            for(var i = 0; i < indexOfValues; i++)
            {
                (newArray[indexValue], newArray[i]) = (newArray[i], newArray[indexValue]);
                indexValue--;
            }

            TrackerClass.Flipfrontiteration++;
            return newArray.AsEnumerable();   
        }

        public static void FlipfrontExecuteLongArray(this long[] array, int indexOfValues)
        {
            if (indexOfValues > array.Length) { throw new ArgumentException(nameof(indexOfValues)); }

            var indexValue = indexOfValues;

            for (var i = 0; i < indexOfValues; i++)
            {
                (array[indexValue], array[i]) = (array[i], array[indexValue]);
                indexValue--;
            }
            TrackerClass.Flipfrontiteration++;
        }

        public static void FlipfrontExecuteArray<T>(this T[] array, int indexOfValues) where T: IComparable<T>    
        {
            if (indexOfValues > array.Length) { throw new ArgumentException(nameof(indexOfValues)); }

            var indexValue = indexOfValues;

            for (var i = 0; i < indexOfValues; i++)
            {
                (array[indexValue], array[i]) = (array[i], array[indexValue]);
                indexValue--;
            }
            TrackerClass.Flipfrontiteration++;
        }

        /// <summary>
        /// def pancake_sort(arr) -> None:
        ///         n = len(arr)
        ///     while n > 1:
        ///         maxdex = max_index(arr, n)
        ///         flip(arr, maxdex)
        ///         flip(arr, n - 1)
        ///         n -= 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name=""></param>
        /// <returns></returns>

        public static IEnumerable<T> PancakeSort<T>(this IEnumerable<T> list) where T : IComparable<T>
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            var returnList = list.ToArray();
            var n = list.Count();

            while (n > 1)
            {
                var value = returnList.Take(n).Max();
                var maxIndexValue = Array.FindLastIndex(returnList.Take(n).ToArray(), item => item.Equals(value));
                returnList = returnList.FlipfrontExecute(maxIndexValue).ToArray();
                returnList = returnList.FlipfrontExecute(n - 1).ToArray(); 
                n--;
            }


            return returnList;
        }

        public static T[] PancakeSortArray<T>(this T[] list) where T : IComparable<T>
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            var returnList = list;
            var lengthOfList = list.Length;

            while (lengthOfList > 1)
            {
                var value = returnList.Take(lengthOfList).Max();
                var maxIndexValue = Array.FindLastIndex(returnList.Take(lengthOfList).ToArray(), item => item.Equals(value));
                returnList.FlipfrontExecuteArray(maxIndexValue);
                returnList.FlipfrontExecuteArray(lengthOfList - 1);
                lengthOfList--;
            }


            return returnList;
        }

        public static long[] PancakeSortLongArray(this long[] list)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            var returnList = list;
            var lengthOfList = list.Length;

            while (lengthOfList > 1)
            {
               
                (var maxIndexValue, var maxValue) = returnList.MaxValueIndex(lengthOfList);
                returnList.FlipfrontExecuteLongArray(maxIndexValue);
                returnList.FlipfrontExecuteLongArray(lengthOfList - 1);
                lengthOfList--;
            }


            return returnList;
        }

        /// <summary>
        /// def max_index(arr, k: int) -> int:
        ///          index = 0
        ///      for i in range(k) :
        ///          if arr[i] > arr[index]:
        ///              index = i
        ///      return index
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        public static (int, T?) MaxValueIndex<T>(this IEnumerable<T> list, int k) where T:IComparable<T>
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            var index = 0;
            T? maxValue = default;
            for (var i = 0; i < k ; i++)
            {
                var valueIndex = list.ElementAt(index);
                var valueAtI = list.ElementAt(i);

                if (valueAtI.CompareTo(valueIndex) > 0)
                {
                    index = i;
                    maxValue = (T)Convert.ChangeType(valueAtI, typeof(T));
                }
            }
            return (index, maxValue);
        }

        public static (bool, List<(int, T?)>?) CheckToSeeIfArrayIsInOrder<T>(this T[] array) where T: IComparable<T>  
        {
            var isInOrder = true;
            var outOfOrderList = new List<(int, T?)>();
            if (!array.Any()) { return (false, null); }
            T? tempValue = default;
            for (var i = 0; i < array.Length; i++)
            {
                if (i == 0) { tempValue = array[i]; continue; }
                var currentValue = array[i];
                if (currentValue.CompareTo(tempValue) >= 0) { tempValue = currentValue; continue; }
                else if (currentValue.CompareTo(tempValue) < 0)
                {

                    isInOrder = false;
                    outOfOrderList.Add((i, currentValue));
                }

            }
            return (isInOrder, outOfOrderList);
        }

        public static (bool, List<(int, long)>?) CheckToSeeIfLongArrayIsInOrder(this long[] array)
        {
            var isInOrder = true;
            var outOfOrderList = new List<(int, long)>();
            if (!array.Any()) { return (false, null); }
            long tempValue = 0;
            for (var i = 0; i < array.Length;i++)
            {
                if (i == 0) { tempValue = array[i]; continue; }
                var currentValue = array[i];
                
                if (currentValue.CompareTo(tempValue) >= 0) { tempValue = currentValue; continue; }
                else if (currentValue.CompareTo(tempValue) < 0) 
                { 
                    
                    isInOrder = false; 
                    outOfOrderList.Add((i, currentValue));
                }

            }
            return (isInOrder, outOfOrderList);
        }


    }
}

