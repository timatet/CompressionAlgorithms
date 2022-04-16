using System;

namespace AlgorithmsLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Для того чтобы здесь, что то работало необходимо переключить тип проекта 
            //на консольное приложение
            //я понимаю что это не правильно так делать но пофиг
            LZ78Algm.Encode("жужжит_жужелица,_жужжит,_да_не_кружится");

            Console.ReadKey();
        }
    }
}
