using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ObservableProperties : INotifyPropertyChanged
{
    /// <summary>
    /// Fires events telling listeners that a property's value has changed
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Calls the event handler with the appropriate property name
    /// This should not need to be called
    /// Prefer using SetField
    /// </summary>
    /// <param name="propertyName">The name of the property</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// The main body of the system
    /// To be called when an observable property's value needs to be set
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="field">The field to update</param>
    /// <param name="value">The new value</param>
    /// <param name="propertyName">The name of the property (to be specified only if the property's name is different from the caller's)</param>
    /// <returns>True if the field was updated, otherwise false</returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        //Check if the field needs updating
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        //Let listeners know the value has been changed
        OnPropertyChanged(propertyName);

        return true;
    }
}
