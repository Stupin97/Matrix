using System;
using System.Collections;

/*
 +Исключения, +min, +max,  +IFormattable, +ICloneable, +IEnumarable,
 инициализация (Что бы можно было вводить поэлементно или с использованием некого алгоритма), 
 +переделать операторы (без использования += и т.д. ) и +повторить функции т.е. (если было int, Matrix/ сделать Matrix, int)
 */
namespace Matrix
{
    static class RandomExtension
    {
        public static double NextDouble(this Random r, double min, double max)
        {
            return min + r.NextDouble() * (max - min);
        }
    }

    class Matrix : IFormattable, ICloneable, IEnumerable, IEnumerator
    {
        private double[,] matrix;
        private int rows, columns;
        private int numberV;
        private static int number = 0;
        
        public Matrix() : this(0, 0)
        {}
        
        public Matrix(int sizeA) : this(sizeA, sizeA)
        {}

        public Matrix(int sizeA, int sizeB)
        {
            numberV = number++;
            Console.WriteLine("Конструктор № {0}", numberV);
            Initialization(sizeA, sizeB);
        }

        public Matrix(int sizeA, int sizeB, Func<int,int,double> alg)
        {
            //alg()
        }

        public object Clone()
        {
            return matrix.Clone();
        }

        // Задание перечислителя для использования foreach    
        public IEnumerable Where(Predicate<double> pred)
        {
           // Matrix pr = new Matrix();
            //Console.WriteLine("IEnumerable");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (pred(matrix[i, j])) yield return matrix[i, j];
                }
                //Console.WriteLine();
            }                   
        } 

        ~Matrix()
        {
            Console.WriteLine("Деструктор № {0}", numberV);
            //GC.GetTotalMemory(true);
        }

        public void Initialization(int sizeA, int sizeB)
        {
            CheckSize(sizeA, sizeB);
            rows = sizeA;
            columns = sizeB;
            matrix = new double[sizeA, sizeB];
        }

        public void My_Write(int sizeA, int sizeB)
        {
            Console.WriteLine("My_Write");
                Random rnd = new Random();
                //Initialization(sizeA, sizeB);
                for (int i = 0; i < sizeA; i++)
                {
                    for (int j = 0; j < sizeB; j++)
                    {
                        matrix[i, j] = rnd.NextDouble(-1, 10);
                    }
                }
        }

        /*public static double[,] Zero(Matrix a)
        {
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    a.matrix[i, j] = 0;
                }
            }
            return a.matrix;
        }*/

        public void Print()
        {
            Console.WriteLine("Матрица");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("{0:0.###\t}", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        

        public void CheckSize(int sizeA, int sizeB)
        {
            if (sizeA < 0 || sizeB < 0)
            {
#if DEBUG
               Console.WriteLine("Введите корректное значение rows /  columns ");
#endif
                //sizeA = Convert.ToInt32(Console.ReadLine());
                //sizeB = Convert.ToInt32(Console.ReadLine());
            }
        }

        public double Rows
        {
            get { return rows; }
        }

        public double Columns
        {
            get { return columns; }
        }

        public static bool CheckMultiply(Matrix a, Matrix b)
        {
            return (b.rows == a.rows);
        }

        public static bool CheckPlus(Matrix a, Matrix b)
        {
            return a.rows == b.rows && a.columns == b.columns;
        }

        public double GetMaxElement(Matrix a)
        {
            double max = a.matrix[0,0];
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    if (max <= a.matrix[i, j])
                    {
                        max = a.matrix[i, j];
                    }
                }
            }
            return max;
        }
        
        public double GetMinElement(Matrix a)
        {
            double min = a.matrix[0, 0];
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    if (min >= a.matrix[i, j])
                    {
                        min = a.matrix[i, j];
                    }
                }
            }
            return min;
        }
        
        //Сделать выход, если невозможное действие
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix summ = new Matrix();
            if (!CheckPlus(a, b))
            {
#if DEBUG
                Console.WriteLine("Нельзя!");
#endif
                Console.Read();
                Environment.Exit(0);
            }
            for (int i = 0;  i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    summ.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];

                }
            }
            return summ;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix sub = new Matrix();
            if (!CheckPlus(a, b))
            {
#if DEBUG
                Console.WriteLine("Нельзя!");
#endif
                Console.Read();
                Environment.Exit(0);
            }
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    sub.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];

                }
            }
            return sub;
        }

        public static Matrix operator *(Matrix a, int scalar)
        {
            Matrix mul = new Matrix();
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    mul.matrix[i, j] = a.matrix[i, j] * scalar;
                }
            }
            return a;
        }

        public static Matrix operator *(int scalar, Matrix a)
        {
            Matrix mul = new Matrix();
            for (int i = 0; i < a.rows; i++)
            {
                for (int j = 0; j < a.columns; j++)
                {
                    mul.matrix[i, j] = a.matrix[i, j] * scalar;
                }
            }
            return a;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (!CheckMultiply(a, b))
            {
#if DEBUG
                Console.WriteLine("Нельзя!");
#endif
                Console.Read();
                Environment.Exit(0);
            }
                double tmp;
            Matrix result = new Matrix(a.rows, b.columns);
                for (int i = 0; i < a.rows; i++)
                {
                    for (int j = 0; j < b.columns; j++)
                    {
                        tmp = result.matrix[i, j];
                        for (int k = 0; k < result.Rows; k++)
                        {
                            tmp += a.matrix[i, k] * b.matrix[k, j];
                        }
                        result.matrix[i, j] = tmp;
                    }
                }
                return result;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    str += matrix[i, j].ToString("0.###") + "\t";
                }
                str += "\n";
            }
            return str;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format != null)
            {
                if (format.ToUpperInvariant() == "C") {
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            string.Format("{0}", matrix[i, j]);
                        }
                    }
                }
            }
#if DEBUG
            Console.WriteLine("!!!");
            return "";
#endif
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this; // matrix.GetEnumerator();
        }

        bool IEnumerator.MoveNext()
        {
            Console.WriteLine();
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }

        public object Current
        {
            get
            {
                return matrix;
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

    }
}
