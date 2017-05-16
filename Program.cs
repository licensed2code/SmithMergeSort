using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMerge t;
            t = new TestMerge();

            Random rand;
            rand = new Random();

            int[] numbersArray = { rand.Next(), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20), rand.Next(20) };

            string str = string.Empty;

            for (int j = 0; j < numbersArray.Length; j++)
            {
                str = str + numbersArray[j].ToString();

                if (j < numbersArray.Length - 1)
                    str = str + ",";
            }

            Console.Write("Input Array:  " + str);
            Console.ReadLine();

            t.DoMergeSort(numbersArray, 0, numbersArray.Length - 1);

            str = string.Empty;

            for (int j = 0; j < numbersArray.Length; j++)
            {
                str = str + numbersArray[j].ToString();

                if (j < numbersArray.Length - 1)
                    str = str + ",";
            }

            Console.Write("Sorted Array:  " + str);
            Console.ReadLine();
        }
    }

    class TestMerge
    {
        public void DoMergeSort(int[] arrayToSort, int l, int r)
        {
            if (l < r)
            {
                int midPoint = l + (r - l) / 2;

                DoMergeSort(arrayToSort, l, midPoint);
                DoMergeSort(arrayToSort, midPoint + 1, r);

                Merge(arrayToSort, l, midPoint, r);
            }
        }

        private void Merge(int[] arrayToSort, int left, int midPoint, int right)
        {
            int i, j, k;
            int number1 = midPoint - left + 1;
            int number2 = right - midPoint;

            int[] Left = new int[number1];
            int[] Right = new int[number2];

            Thread t1 = new Thread(delegate()
                {
                    for (i = 0; i < number1; i++)
                        Left[i] = arrayToSort[left + i];
                });

            Thread t2 = new Thread(delegate()
               {
                   for (j = 0; j < number2; j++)
                       Right[j] = arrayToSort[midPoint + 1 + j];
            });


            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            t1.Abort();
            t2.Abort();

            i = 0;
            j = 0;
            k = left;

            while (i < number1 && j < number2)
            {
                if (Left[i] <= Right[j])
                {
                    arrayToSort[k] = Left[i];
                    i++;
                }
                else
                {
                    arrayToSort[k] = Right[j];
                    j++;
                }
                k++;
            }

            Thread t3 = new Thread(delegate()
               {
                   while (i < number1)
                   {
                       arrayToSort[k] = Left[i];
                       i++;
                       k++;
                   }
               });

            Thread t4 = new Thread(delegate()
              {
                  while (j < number2)
                  {
                      arrayToSort[k] = Right[j];
                      j++;
                      k++;
                  }
              });

            t3.Start();
            t4.Start();

            t3.Join();
            t4.Join();

            t3.Abort();
            t4.Abort();

        }
    }
}
