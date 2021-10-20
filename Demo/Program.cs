using System;
using System.Collections.Generic;
using System.Linq;
using CodingHelmet.Optional;
using CodingHelmet.Optional.Extensions;
using Demo.Models;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ImplicitOperatorsDemo();
            MappingDemo();
            ObjectNoneIfNullDemo();
            ObjectWhenDemo();
            OptionOfTypeDemo();
            EnumerableFirstOrNoneDemo();
            EnumerableSelectOptionalDemo();
            DictionaryTryGetValueDemo();
            OptionEqualityDemo();
            OptionalInterfaceDemo();

            Console.ReadLine();
        }

        private static void ImplicitOperatorsDemo()
        {
            Console.WriteLine("*** Implicit conversion demo:");
            Option<Car> converted = new Car("red car", Color.Red);
            Some<Car> convertedSome = new Car("blue car", Color.Blue);
            Car extracted = convertedSome;

            Console.WriteLine(converted);
            Console.WriteLine(extracted);
        }

        private static void MappingDemo()
        {
            Console.WriteLine("\n*** Mapping demo:");

            Person child = new Person("Jill", 12, Color.Red);
            Person grownUp = new Person("Joe", 46, Color.Blue);

            Option<Car> none = child.TryGetCar();  // None
            Option<Car> some = grownUp.TryGetCar();

            Console.WriteLine($"{none}, {some}");

            Option<Person> noPerson = None.Value;
            Option<Person> someChild = child;
            Option<Person> someGrownUp = grownUp;

            Option<Car> noCar = noPerson.MapOptional(person => person.TryGetCar());
            Option<Car> noChildCar = someChild.MapOptional(person => person.TryGetCar());
            Option<Car> someGrownUpCar = someGrownUp.MapOptional(person => person.TryGetCar());

            Console.WriteLine($"{noCar}, {noChildCar}, {someGrownUpCar}");
        }

        private static void ObjectNoneIfNullDemo()
        {
            Console.WriteLine("\n*** Object.NoneIfNull() demo:");
            Color color = Color.Red;
            Option<Color> maybeColor1 = color.NoneIfNull();  // Some(Red)
            Console.WriteLine($"{color} -> {maybeColor1}");

            color = null;
            Option<Color> maybeColor2 = color.NoneIfNull();  // None
            Console.WriteLine($"{color} -> {maybeColor2}");
        }

        private static void ObjectWhenDemo()
        {
            Console.WriteLine("\n*** Object.When() demo:");
            Color red = Color.Red;
            Option<Color> beautiful = red.When(red == Color.Red); // Some(Red)
            Console.WriteLine($"{red} -> {beautiful}");

            Color blue = Color.Blue;
            Option<Color> ugly = blue.When(c => c == Color.Red);     // None
            Console.WriteLine($"{blue} -> {ugly}");
        }

        private static void OptionOfTypeDemo()
        {
            Console.WriteLine("\n*** Option.OfType() demo:");
            Option<Car> someCar = new Car("car", Color.Red);
            Option<Car> noCar = None.Value;

            Console.WriteLine(someCar.OfType<Vehicle>()); // Some
            Console.WriteLine(noCar.OfType<Vehicle>());   // None
            Console.WriteLine(someCar.OfType<Truck>());   // None
        }

        private static void EnumerableFirstOrNoneDemo()
        {
            Console.WriteLine("\n*** IEnumerable.FirstOrNone() demo:");
            IEnumerable<Color> colors = new[]
            {
                Color.Red, Color.Blue
            };

            Option<Color> color1 = new Color[0].FirstOrNone();                // None
            Option<Color> color2 = colors.FirstOrNone();                      // Some(Red)
            Option<Color> color3 = colors.FirstOrNone(c => c == Color.Green); // None

            Console.WriteLine($"{color1}, {color2}, {color3}");
        }

        private static void EnumerableSelectOptionalDemo()
        {
            Console.WriteLine("\n*** IEnumerable.SelectOptional() demo:");

            IEnumerable<Person> people = new[]
            {
                new Person("Jack", 9, Color.Green), // No car
                new Person("Jill", 19, Color.Red),  // Has a red car
                new Person("Joe", 22, Color.Blue)   // Has a blue car
            };

            IEnumerable<Color> carColors =
                people.SelectOptional(person => person.TryGetCar())
                    .Select(car => car.Color); // [Red, Blue]

            Console.WriteLine(string.Join(", ", carColors.Select(c => c.Label).ToArray()));
        }

        private static void DictionaryTryGetValueDemo()
        {
            Console.WriteLine("\n*** IDictionary.TryGetValue() demo:");

            IEnumerable<Person> people = new[]
            {
                new Person("Jack", 9, Color.Green), // No car
                new Person("Jill", 19, Color.Red),  // Has a red car
                new Person("Joe", 22, Color.Blue)   // Has a blue car
            };

            IDictionary<string, Car> nameToCar = people             // IEnumerable<Person>
                .SelectOptional(person =>
                        person.TryGetCar()                              // Option<Car>
                            .Map(car => (name: person.Name, car: car))  // Option<(string, Car)>
                )                                               // IEnumerable<(string, Car)>
                .ToDictionary(
                    tuple => tuple.name, //   key = name
                    tuple => tuple.car); // value = car

            Console.WriteLine(nameToCar.TryGetValue("Jill"));  // Prints Some
            Console.WriteLine(nameToCar.TryGetValue("Jimmy")); // Prints None
        }

        private static void OptionEqualityDemo()
        {
            Console.WriteLine("\n*** Option.Equals() demo:");

            Option<Color> red = Color.Red;
            Option<Color> equal = Color.Red;
            Option<Color> blue = Color.Blue;

            Option<Color> none = None.Value;
            Option<Color> andNone = None.Value;

            Console.WriteLine($"HashCode {red.GetHashCode()} : {equal.GetHashCode()}");

            Console.WriteLine($"{red} {(red.Equals(equal) ? "==" : "!=")} {equal}");
            Console.WriteLine($"{red} {(red.Equals(Color.Red) ? "==" : "!=")} {Color.Red}");
            Console.WriteLine($"{Color.Red} {(Color.Red.Equals(red) ? "==" : "!=")} {red}");
            Console.WriteLine($"{red} {(red.Equals(blue) ? "==" : "!=")} {blue}");

            Console.WriteLine($"{none} {(none.Equals(andNone) ? "==" : "!=")} {andNone}");
            Console.WriteLine($"{none} {(none.Equals(None.Value) ? "==" : "!=")} {None.Value}");
            Console.WriteLine($"{None.Value} {(None.Value.Equals(none) ? "==" : "!=")} {none}");

            List<string> list = new List<string>() {"a", "b", "c"};
            string listString = "[" + string.Join(", ", list.ToArray()) + "]";
            Console.WriteLine($"{None.Value} {(None.Value.Equals(list) ? "==" : "!=")} {listString}");
            Console.WriteLine($"{listString} {(list.Equals(None.Value) ? "==" : "!=")} {None.Value}");
            Console.WriteLine($"{none} {(none.Equals(list) ? "==" : "!=")} {listString}");
            Console.WriteLine($"{listString} {(list.Equals(none) ? "==" : "!=")} {None.Value}");
        }

        private static void PrintName(Car car)
        {
            Console.WriteLine(car.Name);
        }

        private static void PrintAbstractName(ICar car)
        {
            Console.WriteLine(car.Name);
        }

        private class MyType
        {

        }

        private static void OptionalInterfaceDemo()
        {
            Console.WriteLine("\n*** Optional Interface Demo:");

            Person jill = new Person("Jill", 19, Color.Red); // Has a car
            Option<Car> jillsCar = jill.TryGetCar();         // Some

            if (jillsCar is Some<Car> some1)
            {
                PrintName(some1);
            }

            Option<ICar> abstractCar =
                jill.TryGetCar().OfType<ICar>();  // Some<ICar>

            if (abstractCar is Some<ICar> someOther)
            {
                PrintAbstractName(someOther.Content);
            }
        }
    }
}
