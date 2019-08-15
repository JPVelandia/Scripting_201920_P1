namespace Parcial1_Base.Logic
{
    /// <summary>
    /// Bracelets are a kind of accessory that can be stacked up to a certain amount, and are compatible with all dresses.
    /// </summary>
    public class Bracelet : Accessory
    {
        /// <summary>
        /// Whether this bracelet applies style penalty or not for being equipped along an uncompatible dress
        /// </summary>
        public bool ApplyStylePenalty { get; set; }

        /// <summary>
        /// The style score bonus percentage, if any applies.
        /// If this accessory applies penalty, this value decreases its style by 75%
        /// </summary>
        protected override float StyleMod => ApplyStylePenalty ? -0.75F : 0F;

        public Bracelet(int style) : base(style)
        {
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Accessory object with the same values of this instance</returns>
        public override Accessory Copy()
        {
            return new Bracelet(style);
        }
    }
}