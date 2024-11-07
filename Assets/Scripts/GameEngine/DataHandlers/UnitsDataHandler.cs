using GameEngine;
using GameEngine.Data;
using GameEngine.Helpers;
using SaveSystem.Data;
using UnityEngine;
using Zenject;

namespace DataHandlers
{
    public class UnitsDataHandler : DataHandler<UnitsSaveData>, IInitializable
    {
        private const string _key = "Units";

        private readonly UnitManager _unitManager;
        private readonly UnitsContainer _unitsContainer;

        protected override string Key => _key;

        [Inject]
        public UnitsDataHandler(UnitManager unitManager, UnitsContainer unitsContainer)
        {
            _unitManager = unitManager;
            _unitsContainer = unitsContainer;
        }

        public void Initialize()
        {
            _unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
        }

        protected override void SetupData(UnitsSaveData data)
        {
            var units = _unitManager.GetAllUnits();

            foreach (var unit in units)
            {
                string key = unit.gameObject.name;
                if (data.Units.ContainsKey(key))
                {
                    UnitsSaveData.UnitData unitData = data.Units[key];
                    if (unitData.Type != unit.Type)
                    {
                        throw new System.ArgumentException($"Unit Type mismatch! Type in Unit: {unit.Type}, Type in UnitData: {unitData.Type}");
                    }
                    unit.transform.rotation = Quaternion.Euler(unitData.Rotation.ToVector3());
                    unit.transform.position = unitData.Position.ToVector3();
                    unit.HitPoints = unitData.HitPoints;
                    data.Units.Remove(key);
                }
                else
                {
                    _unitManager.DestroyUnit(unit);
                }
            }

            if (data.Units.Count > 0)
            {
                foreach (var unitDataPair in data.Units)
                {
                    string type = unitDataPair.Value.Type;

                    Unit unitPrefab = _unitsContainer.GetPrefabByType(type);

                    _unitManager.SpawnUnit(unitPrefab, unitDataPair.Value.Position.ToVector3(),
                        Quaternion.Euler(unitDataPair.Value.Rotation.ToVector3()));
                }
                data.Units.Clear();
            }
        }

        protected override UnitsSaveData ConvertToData()
        {
            var data = new UnitsSaveData();
            var units = _unitManager.GetAllUnits();

            foreach (var unit in units)
            {
                var key = unit.gameObject.name;

                data.Units.Add(key, new UnitsSaveData.UnitData
                {
                    ID = unit.gameObject.name,
                    Type = unit.Type,
                    Position = new Vector3Data(unit.Position),
                    Rotation = new Vector3Data(unit.Rotation),
                    HitPoints = unit.HitPoints
                });
            }

            return data;
        }
    }
}