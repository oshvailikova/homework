public interface IButtonController
{
    void Show();
    void Hide();
    void AddClickListener(UnityEngine.Events.UnityAction listener);
    void RemoveListeners();
}