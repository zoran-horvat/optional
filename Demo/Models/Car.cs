namespace Demo.Models
{
    public class Car : Vehicle, ICar
    {
        public string Name { get; }

        public Car(string name, Color color) : base(color)
        {
            this.Name = name;
        }

        public override string ToString() => this.Name;
    }
}
