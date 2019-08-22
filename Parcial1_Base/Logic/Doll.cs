using System.Collections.Generic;
using static Parcial1_Base.Logic.Dress;

namespace Parcial1_Base.Logic
{
    /// <summary>
    /// Definition for the player's avatar. Players dress up a doll to win the contest.
    /// </summary>
    public class Doll : IClonable<Doll>
    {
        //Constants for execution

        /// <summary>
        /// The max amount of bracelets that can be equipped
        /// </summary>
        private const int MAX_BRACELETS = 5;

        /// <summary>
        /// The max amount of bracelets that can be equipped when wearing a party dress and without applying penalties.
        /// </summary>
        private const int MAX_BRACELETS_WITH_PARTY_DRESS = 3;

        /// <summary>
        /// The max amount of bracelets that can be equipped when wearing a suit dress and without applying penalties.
        /// </summary>
        private const int MAX_BRACELETS_WITH_SUIT_DRESS = 1;

        /// <summary>
        /// The accessories collection.
        /// </summary>
        private List<Accessory> accessories = new List<Accessory>();

        /// <summary>
        /// The bracelets collection.
        /// </summary>
        private List<Bracelet> Bracelets { get; set; } = new List<Bracelet>();

        /// <summary>
        /// Reference to the doll's dress
        /// </summary>
        public Dress Dress { get; protected set; }

        /// <summary>
        /// Reference to the doll's necklace
        /// </summary>
        public Necklace Necklace { get; protected set; }

        /// <summary>
        /// Reference to the doll's purse.
        /// </summary>
        public Purse Purse { get; protected set; }

        /// <summary>
        /// The doll's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Whether the doll can be included in the contest.
        /// </summary>
        public bool CanParticipate { get => Dress != null; }

        /// <summary>
        /// The amount of accessories worn by the doll.
        /// </summary>
        public int TotalAccessories { get => accessories.Count; }

        /// <summary>
        /// The amount of bracelets worn by the doll
        /// </summary>
        public int BraceletCount { get => Bracelets.Count; }

        /// <summary>
        /// The total style score, affected by each worn accessory.
        /// </summary>
        public int Style
        {
            get
            {
                int totalStye = 0;

                foreach (Accessory accessory in accessories)
                {
                    totalStye += accessory.Style;
                }

                return totalStye;
            }
        }

        public Doll(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Removes the given accessory.
        /// </summary>
        /// <param name="a">The accessory to be removed</param>
        /// <returns>True if the accessory was being worn, then removed. False otherwise</returns>
        public bool Remove(Accessory a)
        {
            bool result = false;

            if (accessories.Contains(a))
            {
                if (a is Bracelet bracelet)
                {
                    Bracelets.Remove(bracelet);
                }
                else if (a is Purse)
                {
                    Purse = null;
                }
                else if (a is Necklace)
                {
                    Necklace = null;
                }
                else if (a is Dress)
                {
                    Dress = null;
                    Necklace = null;
                    Purse = null;
                    Bracelets.Clear();
                    accessories.Clear();
                }

                if (accessories.Contains(a))
                {
                    accessories.Remove(a);
                }

                result = true;
            }

            return result;
        }

        /// <summary>
        /// Makes the doll wear the given accessory
        /// </summary>
        /// <param name="a">The accessory to be worn by the doll</param>
        /// <returns>True if the doll successfully wore the accessory. False otherwise</returns>
        public bool Wear(Accessory a)
        {
            if (Dress == null)
            {
                #region EquipDress

                if (a is Dress newDress)
                {
                    Dress = newDress;

                    // goto instruction allows jumping to code section without duplicating same code.
                    goto EquipAccessory;
                }
                else
                {
                    goto CantEquip;
                }

                #endregion EquipDress
            }
            else
            {
                if (a is Necklace newNecklace)
                {
                    #region EquipNecklace

                    if (Necklace == null)
                    {
                        switch (Dress.Color)
                        {
                            case EColor.Black:
                            case EColor.White:
                            case EColor.Red:
                                if (Dress.Category != Dress.EDressCategory.Suit)
                                {
                                    Necklace = newNecklace;
                                    goto EquipAccessory;
                                }
                                else
                                {
                                    goto CantEquip;
                                }

                            default:
                                goto CantEquip;
                        }
                    }
                    else
                    {
                        goto CantEquip;
                    }

                    #endregion EquipNecklace
                }
                else
                {
                    if (a is Purse newPurse)
                    {
                        #region EquipPurse

                        newPurse.ApplyStyleBonus = false;

                        if (Purse == null)
                        {
                            if (Dress.Color != EColor.Black && Dress.Color != EColor.White)
                            {
                                //Increase purse style by 50%
                                newPurse.ApplyStyleBonus = true;
                            }

                            Purse = newPurse;
                            goto EquipAccessory;
                        }
                        else
                        {
                            goto CantEquip;
                        }

                        #endregion EquipPurse
                    }
                    else
                    {
                        if (a is Bracelet newBracelet && Bracelets.Count < MAX_BRACELETS)
                        {
                            #region EquipBracelet

                            newBracelet.ApplyStylePenalty = false;
                            Bracelets.Add(newBracelet);

                            switch (Dress.Category)
                            {
                                case Dress.EDressCategory.Suit:

                                    if (Bracelets.Count > MAX_BRACELETS_WITH_SUIT_DRESS)
                                    {
                                        //This accessory's style is decreased by 75%
                                        newBracelet.ApplyStylePenalty = true;
                                    }
                                    goto EquipAccessory;

                                case Dress.EDressCategory.Party:
                                    if (Bracelets.Count > MAX_BRACELETS_WITH_PARTY_DRESS)
                                    {
                                        //This accessory's style is decreased by 75%
                                        newBracelet.ApplyStylePenalty = true;
                                    }
                                    goto EquipAccessory;

                                case Dress.EDressCategory.Casual:
                                    // No style penalty is applied when using a casual dress
                                    goto EquipAccessory;

                                default:
                                    goto CantEquip;
                            }

                            #endregion EquipBracelet
                        }
                        else
                        {
                            goto CantEquip;
                        }
                    }
                }
            }

        EquipAccessory:
            accessories.Add(a);
            return true;

        CantEquip:
            return false;
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Doll object with the same values of this instance</returns>
        public Doll Copy()
        {
            return new Doll(Name);
        }
    }
}