using Servidor.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servidor.Evaluador.Combinaciones
{
    class Combination : ICombinatorial
    {
        private readonly int items;
        private readonly int[] indexes;
        public Combination(int subItems, int items)
        {
            //ExceptionUtil.checkMinValueArgument(subItems, 1, "subItems");
            //ExceptionUtil.checkMinValueArgument(itmes, subItems, "items");
            this.items = items;
            this.indexes = new int[subItems];
            init();
        }
        public void clear()
        {
            init();
        }

        public long combinations()
        {
            return combinations(indexes.Length, items);
        }

        public bool hasNext(int index)
        {
            return indexes[index] + (indexes.Length - index) < items;
        }

        public int[] next(int[] items)
        {
            if (hasNext())
            {
                move(indexes.Length - 1);
                System.Array.Copy(indexes,items,indexes.Length);
            }
            return items;
        }

        public int size()
        {
            return indexes.Length;
        }

        public int getSubItems()
        {
            return indexes.Length;
        }

        public int getItems()
        {
            return items;
        }

        public void move(int index)
        {
            if (hasNext(index))
            {
                indexes[index]++;
                int last = indexes[index];
                for (int i = index + 1; i < indexes.Length; i++)
                {
                    this.indexes[i] = ++last;
                }
            }
            else
            {
                move(index - 1);
            }
        }

        public bool hasNext()
        {
            return hasNext(0) || hasNext(indexes.Length - 1);
        }

        public void init()
        {
            int index = indexes.Length;
            for (int i = 0; i < indexes.Length; i++)
            {
                this.indexes[i] = i;
            }
            this.indexes[index - 1]--;
        }

        public static long combinations(int subItems, int items)
        {
            long result = 1;
            int sub = Math.Max(subItems, items - subItems);
            for (int i = sub + 1; i <= items; i++)
            {
                result = (result * i) / (i - sub);
            }
            return result;
        }
    }
}
