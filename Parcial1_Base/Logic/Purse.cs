namespace Parcial1_Base.Logic
{
    public class Purse : Accessory
    {
        /// <summary>
        /// Whether this purse will apply style bonus if used with a compatible dress.
        /// </summary>
        public bool ApplyStyleBonus { get; set; }

        /// <summary>
        /// The style score bonus percentage, if any applies.
        /// If this accessory applies a bonus, base style score is increased by 50%
        /// </summary>
        protected override float StyleMod => ApplyStyleBonus ? 0.5F : 0F;

        public Purse(int style) : base(style)
        {
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Accessory object with the same values of this instance</returns>
        public override Accessory Copy()
        {
            return new Purse(style);
        }
    }
}