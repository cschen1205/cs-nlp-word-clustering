using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordClustering
{
    public class SortUtil
    {
        public delegate int CompareToHandler<T>(T i, T j);
        private static Random mStdRandom = new Random();

        public static bool IsLessThan<T>(T i, T j, CompareToHandler<T> compareTo)
        {
            if (compareTo != null) return compareTo(i, j) < 0;
            if (i is IComparable && j is IComparable)
            {
                return (i as IComparable).CompareTo((j as IComparable)) < 0;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void Shuffle<T>(T[] a)
        {
            int n = a.Length - 1;
            while(n > 0)
            {
                int k = mStdRandom.Next(n);
                Swap(a, k, n);
                n--;
            }
        }

        public static void Shuffle<T>(List<T> a)
        {
            int n = a.Count - 1;
            while (n > 0)
            {
                int k = mStdRandom.Next(n);
                Swap(a, k, n);
                n--;
            }
        }

        public static int Compare<T>(T i, T j, CompareToHandler<T> compareTo)
        {
            if (compareTo != null)
            {
                return compareTo(i, j);
            }
            if (i is IComparable && j is IComparable)
            {
                return (i as IComparable).CompareTo((j as IComparable));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static bool IsGreaterThan<T>(T i, T j, CompareToHandler<T> compareTo)
        {
            if (compareTo != null)
            {
                return compareTo(i, j) > 0;
            }
            if (i is IComparable && j is IComparable)
            {
                return (i as IComparable).CompareTo((j as IComparable)) > 0;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void Swap<T>(T[] pi, int i, int j)
        {
            T temp = pi[i];
            pi[i] = pi[j];
            pi[j] = temp;
        }

        public static void Swap<T>(List<T> pi, int i, int j)
        {
            T temp = pi[i];
            pi[i] = pi[j];
            pi[j] = temp;
        }

        public static bool IsSorted<T>(T[] pi, int lo, int hi, CompareToHandler<T> compareTo)
        {
            for (int k = lo; k < hi; ++k)
            {
                if (IsGreaterThan(pi[k], pi[k + 1], compareTo))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
