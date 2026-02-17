namespace IDED_Scripting_202610_P1
{
    internal class TestMethods
    {
        public static void SeparateElements(Queue<int> input, out Stack<int> included, out Stack<int> excluded)
        {
            included = null;
            excluded = null;

            Stack<int> tempIncluded = new Stack<int>(); //para invertirlas después manualmente :P
            Stack<int> tempExcluded = new Stack<int>();

            int count = input.Count;

            for (int i = 0; i < count; i++)
            {
                int number = input.Dequeue(); //me devuelve el último elemento de la lista

                int absValue = number < 0 ? -number : number; //si es negativo, invierte el signo; si es pos, lo deja igual

                int root = (int)Math.Sqrt(absValue); //chequeo condición con la raíz cuadrada, entonces la saco COMO ENTERO para ver si tiene decimales

                bool isPerfectSquare = root * root == absValue; //condición 1. tiene que ser cuadrado perfecto. Si el root que saqué no coincide, tenía decimales

                bool belongs = false; //condición para sortearlo después

                if (isPerfectSquare)
                {
                    if (root % 2 != 0 && number < 0) //si al raíz es impar, el num debe ser negativo
                        belongs = true;

                    if (root % 2 == 0 && number > 0) //si la raíz es par, el num debe ser pos
                        belongs = true;
                }

                if (belongs)
                    tempIncluded.Push(number);
                else
                    tempExcluded.Push(number);
            }

            //aquí los invierto en el stack final :>
            while (tempIncluded.Count > 0)
                included.Push(tempIncluded.Pop());

            while (tempExcluded.Count > 0)
                excluded.Push(tempExcluded.Pop());
        }

        public static List<int> GenerateSortedSeries(int n)
        {
            List<int> result = new List<int>();

            // Generar la sucesión
            for (int i = 1; i <= n; i++)
            {
                int value = i * i;

                if (i % 2 != 0)
                    value = -value;

                result.Add(value);
            }

            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = 0; j < result.Count - 1 - i; j++)
                {
                    if (result[j] > result[j + 1])
                    {
                        int temp = result[j];
                        result[j] = result[j + 1];
                        result[j + 1] = temp;
                    }
                }
            }

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
            Stack<int> copy = new Stack<int>();

            foreach (int value in list)
            {
                copy.Push(value);
            }

            while (copy.Count > 0)
            {
                int number = copy.Pop();

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
            Stack<int> copy = new Stack<int>();
            Stack<int> temp = new Stack<int>();

            foreach (int value in stack)
            {
                copy.Push(value);
            }

            while (copy.Count > 0)
            {
                temp.Push(copy.Pop());
            }

            Stack<int> result = new Stack<int>();
            bool removed = false;

            while (temp.Count > 0)
            {
                int number = temp.Pop();

                if (!removed && IsPrime(number))
                {
                    removed = true; 
                    continue;
                }

                result.Push(number);
            }

            return result;
        }

        public static Queue<int> QueueFromStack(Stack<int> stack)
        {
            Stack<int> copy = new Stack<int>();
            Stack<int> temp = new Stack<int>();
            Queue<int> result = new Queue<int>();

            foreach (int value in stack)
            {
                copy.Push(value);
            }

            while (copy.Count > 0)
            {
                temp.Push(copy.Pop());
            }

            while (temp.Count > 0)
            {
                result.Enqueue(temp.Pop());
            }

            return result;
        }
    }
}