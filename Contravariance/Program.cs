using System;

namespace ConsoleApplication
{
    public interface IBase
    {
    }

    public class A : IBase
    {
    }

    public interface ICovariant<out T> where T : class
    {
        T Get();
    }

    public interface IContravariant<in T> where T : class
    {
        void Put(T t);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // covariance
            {
                ICovariant<A> ia = null;
                ICovariant<IBase> ibase = null;

                // this assignment is legal because the type-parameter T in Contravariant<T>
                // is declated covariant
                ibase = ia;
            }

            // contravariance
            {
                IContravariant<A> ia = null;
                IContravariant<IBase> ibase = null;

                // this assignment is legal because the type-parameter T in Contravariant<T>
                // is declated contravariant
                ia = ibase;
            }

            Console.WriteLine("Hello World!");
        }
    }
}
