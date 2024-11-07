using System.Linq;
using GameEngine;
using GameEngine.Data;
using SaveSystem.Data;
using UnityEngine;
using Zenject;

namespace DataHandlers
{
    public sealed class ResourcesDataHandler : DataHandler<ResourcesSaveData>, IInitializable
    {
        private const string _key = "Resources";

        private readonly ResourceService _resourceService;

        protected override string Key => _key;

        [Inject]
        public ResourcesDataHandler(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public void Initialize()
        {
            _resourceService.SetResources(Object.FindObjectsOfType<Resource>());
        }

        protected override void SetupData(ResourcesSaveData resourcesData)
        {
            var resources = resourcesData.Resources.Select(resourceData =>
            {
                var resource = _resourceService.GetResources().FirstOrDefault(r => r.ID == resourceData.ID);
                if (resource != null)
                {
                    resource.Amount = resourceData.Amount; 
                }
                return resource;
            }).Where(r => r != null).ToList();

            _resourceService.SetResources(resources);
        }

        protected override ResourcesSaveData ConvertToData()
        {
            var data = new ResourcesSaveData();

            foreach (var resource in _resourceService.GetResources())
            {
                data.Resources.Add(new ResourcesSaveData.ResourceData
                {
                    ID = resource.ID,
                    Amount = resource.Amount
                });
            }

            return data;
        }
    }
}