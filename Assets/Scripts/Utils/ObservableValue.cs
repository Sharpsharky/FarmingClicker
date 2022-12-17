namespace Utils
{
    using System;
    using System.Collections.Generic;

    public class ObservableValue<T>
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                if(EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    return;
                }

                OnValueChanged?.Invoke(value);
                this.value = value;
            }
        }

        public event Action<T> OnValueChanged;
    }
}