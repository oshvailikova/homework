using Data;
using Presenters.Interfaces;
using System.Collections.Generic;
using UI.Interfaces.Presenters;
using UniRx;
using CharacterInfo = Data.CharacterInfo;

namespace Presenters
{
    public class CharacterStatsContainerPresenter : IStatsContainerPresenter
    {
        public IReadOnlyReactiveCollection<IStatPropertyPresenter> StatPropertyPresenters { get => _statPropertyPresenters; }

        private CharacterInfo _characterInfo;

        private readonly ReactiveCollection<IStatPropertyPresenter> _statPropertyPresenters = new();
        private readonly Dictionary<CharacterStat, IStatPropertyPresenter> _statPropertyPresentersDict = new();

        public CharacterStatsContainerPresenter(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            _characterInfo.OnStatAdded += AddStatPresenter;
            _characterInfo.OnStatRemoved += RemoveStatPresenter;

            var stats = _characterInfo.GetStats();
            for (int i = 0; i < stats.Length; i++)
            {
                AddStatPresenter(stats[i]);
            }
        }

        ~CharacterStatsContainerPresenter()
        {
            _characterInfo.OnStatAdded -= AddStatPresenter;
            _characterInfo.OnStatRemoved -= RemoveStatPresenter;
        }

        private void AddStatPresenter(CharacterStat stat)
        {
            var statPresenter = new CharacterStatPropertyPresenter(stat);
            _statPropertyPresenters.Add(statPresenter);

            _statPropertyPresentersDict.Add(stat, statPresenter);
        }

        private void RemoveStatPresenter(CharacterStat stat)
        {
            var presenter = _statPropertyPresentersDict[stat];

            if (presenter != null)
            {
                _statPropertyPresenters.Remove(presenter);
                _statPropertyPresentersDict.Remove(stat);
            }
        }
    }
}
