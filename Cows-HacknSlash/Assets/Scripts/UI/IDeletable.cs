public delegate void DeleteRequestedEvent(object sender);

public interface IDeletable
{
    event DeleteRequestedEvent DeleteRequested;
}
