using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            Car c1 = new Car("SlugBug", 100, 10);

            // NameOfObject.NameOfEvent += new RelatedDelegate(functionToCall);

            // register event handlers.
            c1.AboutToBlow += CarIsAlmostDoomed;
            c1.AboutToBlow += CarAboutToBlow;

            Car.CarEngineHandler d = new Car.CarEngineHandler(CarExploded);
            c1.Exploded += CarExploded;

            Console.WriteLine("***** Speeding up! *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            // Remove CarExploded method from invocation list
            c1.Exploded -= CarExploded;

            Console.WriteLine("***** Speeding up! *****");
            for (int i = 0; i < 6; i++)
                c1.Accelerate(20);

            Console.ReadLine();


        }



        public static void CarAboutToBlow(object sender, CarEventArgs e)
        {
            Console.WriteLine($"{sender} says: {e.msg}");
        }

        public static void CarIsAlmostDoomed(object sender, CarEventArgs e)
        {
            if (sender is Car)
            {
                Car c = (Car)sender;
                Console.WriteLine("=> Critical Message from {0}: {1}", c.PetName, e.msg);
            }
        }

        public static void CarExploded(object sender, CarEventArgs e)
        {
            Console.WriteLine(e.msg);
        }
    }
}
