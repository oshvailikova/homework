using Presenters.Interfaces.Common;

namespace UI.Popup
{
    public interface IPopup
    {
        void Show(IPresenter presenter);
        void Hide();
    }
}