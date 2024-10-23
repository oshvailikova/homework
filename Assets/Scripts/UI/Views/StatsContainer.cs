using Presenters.Interfaces;
using System.Collections.Generic;
using UI.Utils;
using UniRx;
using UnityEngine;

namespace UI.Views
{

    public class StatsContainer : DisposableView
    {
        [SerializeField]
        private Transform _holder;
        [Header("Prefabs")]
        [SerializeField] private StatProperty _statPropertyPrefab;

        private IStatsContainerPresenter _currentPresenter;

        private readonly List<StatProperty> _currentStats = new();

        public void Initialize(IStatsContainerPresenter presenter)
        {
            Dispose();

            _currentPresenter = presenter;
            SubscribeToPresenter(_currentPresenter);

            UpdateStats(_currentPresenter.StatPropertyPresenters);
        }

        private void SubscribeToPresenter(IStatsContainerPresenter presenter)
        {
            presenter.StatPropertyPresenters.ObserveAdd().Subscribe(OnStatAdded).AddTo(_disposable);
            presenter.StatPropertyPresenters.ObserveRemove().Subscribe(OnStatRemoved).AddTo(_disposable);
        }

        private void OnStatRemoved(CollectionRemoveEvent<IStatPropertyPresenter> removeEvent)
        {
            UpdateStats(_currentPresenter.StatPropertyPresenters);
        }

        private void OnStatAdded(CollectionAddEvent<IStatPropertyPresenter> addEvent)
        {
            AddStatView(addEvent.Value);
        }

        private void UpdateStats(IEnumerable<IStatPropertyPresenter> statPresenters)
        {
            for (int i = 0; i < _currentStats.Count; i++)
                Destroy(_currentStats[i].gameObject);

            _currentStats.Clear();

            foreach (var presenter in statPresenters)
                AddStatView(presenter);
        }

        private void AddStatView(IStatPropertyPresenter presenter)
        {
            var statView = Instantiate(_statPropertyPrefab, _holder);
            statView.Initialize(presenter);

            _currentStats.Add(statView);
        }
    }
}
