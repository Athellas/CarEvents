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

            Car c2 = new Car("Evencik Handlercik", 100, 10);

            // Register event handlers.
            c2.AboutToBlow += CarAboutToBlow;
            c2.AboutToBlow += CarIsAlmostDoomed;

            EventHandler<CarEventArgs> ev = new EventHandler<CarEventArgs>(CarExploded);
            c2.Exploded += ev;
            Console.ReadLine();

            // anonymous method
            Car c3 = new Car("Anonimowa Metodka", 100, 10);

            // Register event handlers as anonymous methods
            c3.AboutToBlow += delegate
            {
                Console.WriteLine("Eek! Going too fast!");
            };

            c3.AboutToBlow += delegate (object sender, CarEventArgs e)
             {
                 Console.WriteLine("Message from Car: {0}", e.msg);
             };
            c3.Exploded += delegate (object sender, CarEventArgs e)
            {
                Console.WriteLine("Fatal Message from Car: {0}", e.msg);
            };

            // this will eventually trigger the events
            for (int i= 0;i< 6;i++)
            {
                Console.WriteLine("Speed before accelerate: {0}", c3.CurrentSpeed);
                Console.WriteLine("Speeding up...");
                c3.Accelerate(20);
                Console.ReadLine();

            }
            Console.ReadLine();

            Console.WriteLine("*****Anonymous Methods *****\n");
            int aboutToBlowCounter = 0;

            Car c4 = new Car("Anonimik4", 100, 10);

            c4.AboutToBlow += delegate
            {
                aboutToBlowCounter++;
                Console.WriteLine("Eek!Going too fast!");
            };

            c4.AboutToBlow += delegate (object sender, CarEventArgs e)
            {
                aboutToBlowCounter++;
                Console.WriteLine("Critical message from Car: {0}", e.msg);
            };

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("Speed before accelerate: {0}", c3.CurrentSpeed);
                Console.WriteLine("Speeding up...");
                c4.Accelerate(20);
                Console.ReadLine();

            }
            Console.ReadLine();

            Console.WriteLine("AboutToBlow event was fired {0} times.", aboutToBlowCounter);
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
