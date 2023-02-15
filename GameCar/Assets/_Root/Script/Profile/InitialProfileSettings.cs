using UnityEngine;

namespace GameCarProfile
{
    [CreateAssetMenu(fileName = nameof(InitialProfileSettings), menuName = "Configs/" + nameof(InitialProfileSettings))]
    internal sealed class InitialProfileSettings : ScriptableObject
    {
        [field: SerializeField] public GameState InitialState {get; private set;}


        [field: SerializeField] public float SpeedCar { get; private set; }
        [field: SerializeField] public float Jump { get; private set; }

    }
}