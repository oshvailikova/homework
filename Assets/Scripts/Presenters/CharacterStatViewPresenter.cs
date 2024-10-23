using Data;
using Presenters.Interfaces;
using UniRx;

namespace Presenters
{
    public class CharacterStatPropertyPresenter : IStatPropertyPresenter
    {
        public IReadOnlyReactiveProperty<string> StatInfo { get => _statInfo; }

        private readonly CharacterStat _stat;

        private readonly StringReactiveProperty _statInfo;

        public CharacterStatPropertyPresenter(CharacterStat stat)
        {
            _stat = stat;
            _statInfo = new StringReactiveProperty(GetStatInfo(stat.Value));

            _stat.OnValueChanged += OnStatValueChanged;
        }

        ~CharacterStatPropertyPresenter()
        {
            _stat.OnValueChanged -= OnStatValueChanged;
        }

        private void OnStatValueChanged(int newValue)
        {
            _statInfo.Value = GetStatInfo(newValue);
        }

        private string GetStatInfo(int value)
        {
            return $"{_stat.Name}: {value}";
        }
    }
}
