using System;
using System.ComponentModel;

public interface ISkill : INotifyPropertyChanged
{

    event EventHandler CanExecuteChanged;

    bool CanExecute(object parameter);
    void Execute(object parameter);
}