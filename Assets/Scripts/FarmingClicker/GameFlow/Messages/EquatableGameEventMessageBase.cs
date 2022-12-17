namespace FarmingClicker.GameFlow.Messages
{
    using System;

    [Serializable]
    public abstract class EquatableGameEventMessageBase : GameEventMessageBase
    {
        #region Equality check
        public override bool Equals(object comparedObject)
        {
            if(ReferenceEquals(null, comparedObject))
            {
                return false;
            }

            if(ReferenceEquals(this, comparedObject))
            {
                return true;
            }

            return comparedObject.GetType() == GetType() && Equals((EquatableGameEventMessageBase) comparedObject);
        }

        protected bool Equals(EquatableGameEventMessageBase other)
        {
            return CompareEquality(other);
        }

        protected abstract bool CompareEquality(EquatableGameEventMessageBase other);

        public abstract override int GetHashCode();

        public static bool operator ==(EquatableGameEventMessageBase left, EquatableGameEventMessageBase right)
        {
            return Equals(left, right); 
        }

        public static bool operator !=(EquatableGameEventMessageBase left, EquatableGameEventMessageBase right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}