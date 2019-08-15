namespace Parcial1_Base.Logic
{
    /// <summary>
    /// Dress is the base accessory for all dolls. Dolls can't participate without one.
    /// </summary>
    public class Dress : Accessory
    {
        /// <summary>
        /// Dress categories.
        /// </summary>
        public enum EDressCategory
        {
            None,
            Suit,
            Party,
            Casual
        }

        /// <summary>
        /// Dress colors
        /// </summary>
        public enum EColor
        {
            None,
            Black,
            White,
            Red,
            Blue,
            Yellow,
            Green,
            Pink
        }

        /// <summary>
        /// This dress' color
        /// </summary>
        public EColor Color { get; protected set; }

        /// <summary>
        /// This dress' category
        /// </summary>
        public EDressCategory Category { get; protected set; }

        public Dress(int style, EColor color, EDressCategory category) : base(style)
        {
            Color = color;
            Category = category;
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Accessory object with the same values of this instance</returns>
        public override Accessory Copy()
        {
            return new Dress(style, Color, Category);
        }
    }
}