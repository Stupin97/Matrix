using System;

namespace Matrix
{
    class Program
    {
        /*static Matrix OptMesod(FunkDelegate funk, Matrix X)
        {
            Matrix result = new Matrix();
            for (int i = 0; i < X.Rows; i++)
            {
                for (int j = 0; j < X.Columns; j++)
                    X[i,j] = funk(X[i,j]); ///Спросить!!!!!!!!!!!
            }
            return result;
        }*/

        static double Funk(double X)
        {
            return X;
        }

        delegate Matrix FunkDelegate(Matrix X);


        static void Main(string[] args)
        {
            //FunkDelegate funk = Funk;
            
            int sizeA, sizeB;
            Console.WriteLine("Введите значение rows /  columns ");
            sizeA = Convert.ToInt32(Console.ReadLine());
            sizeB = Convert.ToInt32(Console.ReadLine());
            //b.Rows = 10;
            //Console.WriteLine("{0}", b.Rows);
            //Console.WriteLine("{0}", b.Columns);
            Matrix b = new Matrix(sizeA, sizeB);
            Matrix c = new Matrix();
            
            b.My_Write(sizeA, sizeB);
            //Matrix result = OptMesod(funk, b);

            //Console.WriteLine("Матрица  с = ");
            //Console.WriteLine(Zero(c));
            //c.Print();


            //Matrix pr = (Matrix)c.Clone();

            b.Print();
            Matrix a = new Matrix(sizeA);
            Console.WriteLine(b);
            Console.WriteLine("Возможность умножения {0}", Matrix.CheckMultiply(a, b));
            Console.WriteLine("Возможность сложения {0}", Matrix.CheckPlus(a, c));
            Console.WriteLine("Максимальный элемент: {0}", a.GetMaxElement(b));
            Console.WriteLine("Минимальный элемент: {0}", a.GetMinElement(b));
            Matrix d = new Matrix(sizeA, sizeB);
            d.Print();

            foreach (object o in d.MyNumerator_a())
            {
                Console.Write(Math.Round((double)o, 2) + "\t");
            }
            Console.WriteLine("---------");

            //foreach (double p in d)
                //Console.Write((Math.Round(p, 2) + "\t"));
                       
            Console.WriteLine("-----!---");
            a.Print();
            b.Print();
            d = a * b;
            d.Print();
            Console.WriteLine("---------");
            d = a + c;
            d.Print();
            Console.WriteLine("---------");
            a = d - d;
            a.Print();
            c = c * 3;
            c.Print();
            Console.WriteLine("Перегруженный оператор ToString()");
            Console.WriteLine("{0:0.##\t}", c);
            Console.Read();
            foreach (double x in c.Where(el => el > 0))
            {

            }
            
        }
    }
}
