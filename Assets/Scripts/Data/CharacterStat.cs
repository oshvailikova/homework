using System;
using Sirenix.OdinInspector;

namespace Data
{
    [Serializable]
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged;
        
        public CharacterStat(string name, int value)
        {
            Name = name;
            ChangeValue(value);
        }

        [ShowInInspector, ReadOnly]
        public string Name { get; private set; }

        [ShowInInspector, ReadOnly]
        public int Value { get; private set; }

        [Button]
        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }
    }
}