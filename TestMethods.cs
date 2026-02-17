namespace IDED_Scripting_202610_P1
{
    internal class TestMethods
    {
        public static void SeparateElements(Queue<int> input, out Stack<int> included, out Stack<int> excluded)
        {
            included = new Stack<int>();
            excluded = new Stack<int>();

            Stack<int> tempIncluded = new Stack<int>(); //para voltearlo al final
            Stack<int> tempExcluded = new Stack<int>();

            foreach (int number in input)
            {
                //las condiciones que tengo que revisar es que: 1. sea cuadrado perfecto y 2. Si la raíz es impar, el número debe ser negativo y vis.
                int absValue = Math.Abs(number); 
                int root = (int)Math.Sqrt(absValue);

                bool isPerfectSquare = root * root == absValue; //si no es ==, es porque la raíz tenía decimales y NO es cuadrado perfe :P
                bool belongs = false;

                if (isPerfectSquare)
                {
                    if (root % 2 != 0 && number < 0) //si es impar, debe ser neg
                        belongs = true;

                    if (root % 2 == 0 && number > 0) //si es par, debe ser pos
                        belongs = true;
                }

                //lo meto en los temp
                if (belongs)
                    tempIncluded.Push(number);
                else
                    tempExcluded.Push(number);

                //y aquí ya quedan invertidos :>
                while (tempIncluded.Count > 0)
                    included.Push(tempIncluded.Pop());

                while (tempExcluded.Count > 0)
                    excluded.Push(tempExcluded.Pop());
            }
        }

        public static List<int> GenerateSortedSeries(int n)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < n; i++) //n términos
            {
                int value = i * i;

                if (i % 2 != 0) //aplico la fórmula. Si el número era par, queda pos. Si era impar, queda par.
                    value = -value;

                int j = 0;
                while (j < result.Count && result[j] < value) //me muevo en el array mientras los numeritos sean menores que value
                {
                    j++;
                }

                result.Insert(j, value);
            }

            return result;
        }

        public static bool FindNumberInSortedList(int target, in List<int> list)
        {
            List<int> sorted = new List<int>(list); // copio la lista para ordenarla sin dañar la original

            // ordeno
            for (int i = 0; i < sorted.Count - 1; i++) //pasadas (si tengo 5 elementos, solamente necesito 4 pasadas para que quede ordenado)
            {
                for (int j = 0; j < sorted.Count - 1 - i; j++) //comparaciones por pasadas (-1-i porque cada vez que pasa, ya un elemento quedó en su pos def)
                {
                    if (sorted[j] < sorted[j + 1]) //reviso si el actual es menor que el siguiente y swappea
                    {
                        int temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                }
            }

            // buscar en la lista ordenada descendentemente
            foreach (int num in sorted)
            {
                if (num == target)
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
            if (n == 2)
                return true;
            if (n % 2 == 0) 
                return false; //me salto los casos especiales

            int sqrt = (int)Math.Sqrt(n); //reviso solamente hasta raíz
            for (int i = 3; i <= sqrt; i += 2)
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
                    continue; // me salto este número
                }
                temp.Push(number);
            }

            Stack<int> result = new Stack<int>();
            while (temp.Count > 0)
            {
                result.Push(temp.Pop());
            }
            return result;
        }

        public static Queue<int> QueueFromStack(Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>();
            Queue<int> result = new Queue<int>();

            foreach (int number in stack)
            {
                temp.Push(number);
            }

            while (temp.Count > 0)
            {
                result.Enqueue(temp.Pop());
            }

            return result;
        }
    }
}