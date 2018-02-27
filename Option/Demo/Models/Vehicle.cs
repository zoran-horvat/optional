namespace Demo.Models
{
    public abstract class Vehicle
    {
        public Color Color { get; }

        protected Vehicle(Color color)
        {
            this.Color = color;
        }
    }
}
