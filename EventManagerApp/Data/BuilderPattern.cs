using System.Drawing;

namespace MonApp.Data
{
    public class Seat { 
    }

    public class Car
    {
        private Color _color;
        private Seat[] _seat;

        internal Car(Color color, Seat[] seats)
        {
            _color = color;
            _seat= seats;
            
        }
    }

    public class CarBuilder
    {
        private int _numberOfSeats;
        private Color _color;

        public CarBuilder SetColor(Color color)   {
            _color = color;
            return this;
        }

        public CarBuilder SetNumberOfSeats(int numberOfSeats) { 
            _numberOfSeats = numberOfSeats;
            return this;
        }

        public Car Build()
        {
            var seats= Enumerable
                .Repeat(new Seat(),_numberOfSeats)
                .ToArray(); 
            return new Car(_color, seats);
        }
    }

    public class Test
    {
        public void Methode1()
        {
            var builder= new CarBuilder()
                .SetColor(Color.Aqua)
                .SetNumberOfSeats(4);

            var car1 = builder.Build();

            var car2 = builder.Build();
        }
    }
}
