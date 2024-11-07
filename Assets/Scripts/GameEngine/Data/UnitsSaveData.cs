using GameEngine.Helpers;
using System;
using System.Collections.Generic;

namespace GameEngine.Data
{
    [Serializable]
    public class UnitsSaveData
    {
        public Dictionary<string, UnitData> Units = new();

        public struct UnitData
        {
            public string ID;
            public string Type;
            public Vector3Data Position;
            public Vector3Data Rotation;
            public int HitPoints;
        }
    }
}
