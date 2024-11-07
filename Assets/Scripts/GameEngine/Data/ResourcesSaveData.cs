using System;
using System.Collections.Generic;

namespace GameEngine.Data
{
    [Serializable]
    public class ResourcesSaveData
    {
        public List<ResourceData> Resources = new List<ResourceData>();

        public struct ResourceData
        {
            public string ID;
            public int Amount;
        }
    }
}
