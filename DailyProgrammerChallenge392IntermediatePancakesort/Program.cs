using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace DailyProgrammerChallenge392IntermediatePancakesort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                PancakeSort10000(args[i]);
            }
        }


        private static void TestMethod()
        {
            var testArray = new int[] { 0, 1, 2, 3, 4 };
            var testArray2 = new int[25];


            testArray = testArray.FlipfrontExecute(4).ToArray();

            for (int i = 0; i < testArray.Length; i++)
            {
                Console.Write($"{testArray[i]}   ");

            }
            Console.WriteLine();
            var random = new Random();
            for (int i = 0; i < testArray2.Length; i++)
            {
                testArray2[i] = random.Next(0, 101);

            }

            Console.WriteLine("Before Sorting: ");
            for (int i = 0; i < testArray2.Length; i++)
            {
                Console.Write($"{testArray2[i]}   ");
            }

            var testArray3 = testArray2.PancakeSort().ToArray();

            Console.WriteLine("After Pancake Sorting: ");
            for (int i = 0; i < testArray3.Length; i++)
            {
                Console.Write($"{testArray3[i]}   ");
            }
        }

        private static void PancakeSort10000(string arguments)
        {

            var timer = new Stopwatch();
            timer.Start();
            var stringPath = string.IsNullOrWhiteSpace(arguments) ? null : arguments;

            if (stringPath == null) { Console.WriteLine("Please Provide a path to the argument"); }

            var fileValue = System.IO.File.ReadAllText((!string.IsNullOrWhiteSpace(stringPath) ? stringPath : string.Empty));

            long[] arrayToTest = CreateLongArray(fileValue).ToArray();
            long[] arrayToTest2 = CreateArray<long>(fileValue).ToArray();
            TrackerClass.Flipfrontiteration = 0;
            var orderedList = arrayToTest.PancakeSortLongArray();
            var flipsIteration = TrackerClass.Flipfrontiteration;
            TrackerClass.Flipfrontiteration = 0;
            var orderedList2 = arrayToTest2.PancakeSort().ToArray();
            var flipsIteration2 = TrackerClass.Flipfrontiteration;
            (var isTheListOrdered, var outOfOrderList) = orderedList.CheckToSeeIfLongArrayIsInOrder();
            (var isTheListOrdered2, var outOfOrderList2) = orderedList2.CheckToSeeIfArrayIsInOrder<long>();

            timer.Stop();
            if (isTheListOrdered) Console.WriteLine($"It took {flipsIteration} Iterations");
            if (isTheListOrdered2) Console.WriteLine($"It took {flipsIteration2} Iterations");
            Console.WriteLine($"It took {timer.Elapsed} to process");

        }

        private static IEnumerable<long> CreateLongArray(string fileValue)
        {
            var regex = new Regex("[\\n\\r\\s]+");
            var listOfValues = regex.Split(fileValue).ToList();

            var returnIEnumerable = Enumerable.Empty<long>();
            for (var i = 0; i < listOfValues.Count; i++)
            {
                var value = listOfValues[i];
                var didItParse = long.TryParse(value, out var valueLong);
                if (didItParse)
                {
                    returnIEnumerable = returnIEnumerable.Concat((new[] { valueLong }));
                }
            }

            return returnIEnumerable;
        }

        private static IEnumerable<T> CreateArray<T>(string fileValue) where T : IComparable<T> 
        {
            var regex = new Regex("[\\n\\r\\s]+");
            var listOfValues = regex.Split(fileValue).ToList();

            var returnIEnumerable = Enumerable.Empty<T>();
            for (var i = 0; i < listOfValues.Count; i++)
            {
                var value = listOfValues[i];
                var genericValue = (T)Convert.ChangeType(value, typeof(T));
                returnIEnumerable = returnIEnumerable.Concat((new[] { genericValue }));
            }

            return returnIEnumerable;
        }

    }


}