namespace IDED_Scripting_202610_P1
{
    internal class TestMethods
    {
        public static void SeparateElements(Queue<int> input, out Stack<int> included, out Stack<int> excluded)
        {
            included = new Stack<int>();
            excluded = new Stack<int>();

            foreach (int number in input)
            {
                int absValue = Math.Abs(number);
                int root = (int)Math.Sqrt(absValue);

                bool isPerfectSquare = root * root == absValue;
                bool belongs = false;

                if (isPerfectSquare)
                {
                    if (root % 2 != 0 && number < 0)
                        belongs = true;

                    if (root % 2 == 0 && number > 0)
                        belongs = true;
                }

                if (belongs)
                    included.Push(number);
                else
                    excluded.Push(number);
            }
        }

        public static List<int> GenerateSortedSeries(int n)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < n; i++)
            {
                int value = i * i;

                if (i % 2 != 0)
                    value = -value;

                result.Add(value);
            }

            result.Sort();
            return result;
        }

        public static bool FindNumberInSortedList(int target, in List<int> list)
        {
            List<int> sorted = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                sorted.Add(list[i]);
            }

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = 0; j < sorted.Count - 1 - i; j++)
                {
                    if (sorted[j] < sorted[j + 1])
                    {
                        int temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < sorted.Count; i++)
            {
                if (sorted[i] == target)
                    return true;
            }

            return false;
        }

        public static int FindPrime(in Stack<int> list)
        {
            foreach (int number in list)
            {
                if (IsPrime(number))
                    return number;
            }

            return 0;
        }

        public static bool IsPrime(int n)
        {
            if (n <= 1)
                return false;

            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        public static Stack<int> RemoveFirstPrime(in Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>();
            bool removed = false;

            foreach (int number in stack)
            {
                if (!removed && IsPrime(number))
                {
                    removed = true;
                    continue;
                }

                temp.Push(number);
            }

            return new Stack<int>(temp);
        }

        public static Queue<int> QueueFromStack(Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>(stack);
            Queue<int> result = new Queue<int>();

            while (temp.Count > 0)
            {
                result.Enqueue(temp.Pop());
            }

            return result;
        }
    }
}