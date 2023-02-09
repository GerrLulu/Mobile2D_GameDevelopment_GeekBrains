using Features.AbilitySystem;
using GameCarTool;
using UnityEngine;

namespace GameCar.Car
{
    internal sealed class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Car");
        private readonly CarModel _model;
        private readonly CarView _view;

        float IAbilityActivator.JumpHeight => _model.Jump;
        GameObject IAbilityActivator.ViewGameObject => _view.gameObject;


        public CarController(CarModel model)
        {
            _model = model;
            _view = LoadView();
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
