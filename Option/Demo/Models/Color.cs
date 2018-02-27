using System;

namespace Demo.Models
{
    public class Color : IEquatable<Color>
    {
        public string Label { get; }

        private Color(string label)
        {
            this.Label = label;
        }

        public static Color Red => new Color("Red");
        public static Color Blue => new Color("Blue");
        public static Color Green => new Color("Green");

        public override string ToString() => this.Label;

        public bool Equals(Color other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Label, other.Label);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Color) obj);
        }

        public static bool operator ==(Color a, Color b) =>
            (a is null && b is null) ||
            (!(a is null) && a.Equals(b));

        public static bool operator !=(Color a, Color b) => !(a == b);

        public override int GetHashCode()
        {
            return (Label != null ? Label.GetHashCode() : 0);
        }
    }
}
