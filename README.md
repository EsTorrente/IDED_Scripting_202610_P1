hola profe :(
El parcial lo hice yo sola, pero
Cometí un error horrible

En esta parte:  
<img width="568" height="85" alt="image" src="https://github.com/user-attachments/assets/aa9b3bc9-0bcb-49aa-8825-4d5215ca34c6" />  
Malinterpreté la información. Estaba muy tostada, venía de estar trabajando todo el día y se me olvidó que el foreach estaba entre las cosas que no permitías usar. Lo usé literalmente en TODOS los puntos (menos GenerateSortedSeries) :(
Pensé que se prohibía el uso de el linq, .ToList(), .ToArray(), .Sort, esas cosas... y en el momento, mi cabeza puso el foreach bajo la misma categoría que el while, do, esas cosas. Me concentré tan solo en ordenarlas manualmente, no RECORRERLAS manualmente. :(
Por ende, todos los ejercicios (menos 1) están 100% malos.

Si lo fuera a hacer ahora, usaría un while (stack.Count > 0)

Igual, hice la corrección (ya tarde) como disculpa por un error tan básico:

```.cs
namespace IDED_Scripting_202610_P1
{
    internal class TestMethods
    {
        public static void SeparateElements(Queue<int> input, out Stack<int> included, out Stack<int> excluded)
        {
            Stack<int> tempIncluded = new Stack<int>(); //para voltearlo al final
            Stack<int> tempExcluded = new Stack<int>();

            int count = input.Count;
            int i = 0;
            while (i < count)
            {
                int number = input.Dequeue();
                input.Enqueue(number); //loo vuelvo a meter para no perder el queue original
                
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
                
                i++;
            }

            //y aquí ya quedan invertidos :>
            included = new Stack<int>();
            excluded = new Stack<int>();
            
            while (tempIncluded.Count > 0)
                included.Push(tempIncluded.Pop());

            while (tempExcluded.Count > 0)
                excluded.Push(tempExcluded.Pop());
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
            int i = 0;
            while (i < sorted.Count - 1) //pasadas (si tengo 5 elementos, solamente necesito 4 pasadas para que quede ordenado)
            {
                int j = 0;
                while (j < sorted.Count - 1 - i) //comparaciones por pasadas (-1-i porque cada vez que pasa, ya un elemento quedó en su pos def)
                {
                    if (sorted[j] < sorted[j + 1]) //reviso si el actual es menor que el siguiente y swappea
                    {
                        int temp = sorted[j];
                        sorted[j] = sorted[j + 1];
                        sorted[j + 1] = temp;
                    }
                    j++;
                }
                i++;
            }

            //busco en la lista ordenada descendentemente
            int k = 0;
            while (k < sorted.Count)
            {
                if (sorted[k] == target)
                    return true;
                k++;
            }
            return false;
        }

        public static int FindPrime(in Stack<int> list)
        {
            Stack<int> temp = new Stack<int>();
            int result = 0;
            
            while (list.Count > 0)
            {
                int number = list.Pop();
                temp.Push(number);
                
                if (result == 0 && IsPrime(number))
                    result = number;
            }
            
            //restauro el stack original
            while (temp.Count > 0)
            {
                list.Push(temp.Pop());
            }

            return result;
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
            int i = 3;
            while (i <= sqrt)
            {
                if (n % i == 0)
                    return false;
                i += 2; //solamente reviso impares
            }
            return true;
        }

        public static Stack<int> RemoveFirstPrime(in Stack<int> stack)
        {
            Stack<int> temp = new Stack<int>();
            bool removed = false;

            //vacio el stack original en temp
            Stack<int> copy = new Stack<int>();
            while (stack.Count > 0)
            {
                copy.Push(stack.Pop());
            }
            
            //restauro el stack original
            while (copy.Count > 0)
            {
                int number = copy.Pop();
                stack.Push(number);
                
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

            //copio a temp invirtiendo
            Stack<int> copy = new Stack<int>();
            while (stack.Count > 0)
            {
                copy.Push(stack.Pop());
            }
            
            while (copy.Count > 0)
            {
                int number = copy.Pop();
                stack.Push(number); //restauro el original
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

```

Y ya :(
