using CodingHelmet.Optional;

namespace Demo.Models
{
    public class Person
    {
        public string Name { get; }
        private int Age { get; }
        private Color FavoriteColor { get; }

        public Person(string name, int age, Color favoriteColor)
        {
            this.Name = name;
            this.Age = age;
            this.FavoriteColor = favoriteColor;
        }

        public Option<Car> TryGetCar() =>
            this.Age >= 18 
                ? (Option<Car>)new Car($"{this.Name}'s {this.FavoriteColor.Label.ToLower()} car", this.FavoriteColor) 
                : None.Value;
    }
}
