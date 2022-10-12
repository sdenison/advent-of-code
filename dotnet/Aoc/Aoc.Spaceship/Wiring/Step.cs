
namespace Aoc.Spaceship.Hardware
{
    public class Step 
    {
        public Coordinate Position { get; set; }
        public Axis Axis { get; set; }
        public int TotalStepsSoFar { get; set; }

        public Step(Coordinate position, Axis axis, int totalStepsSoFar)
        {
            Position = position;
            Axis = axis;
            TotalStepsSoFar = totalStepsSoFar;
        }
    }
}
