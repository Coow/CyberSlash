public delegate void SelectedEvent(object sender);

public interface ISelectable
{
    event SelectedEvent Selected;
    bool IsSelected { get; set; }
}
