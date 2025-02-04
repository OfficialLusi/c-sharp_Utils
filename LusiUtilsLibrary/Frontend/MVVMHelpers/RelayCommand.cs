using System.Windows.Input;

namespace LusiUtilsLibrary.Frontend.MVVMHelpers;

public class RelayCommand(Func<Task> execute, Func<bool> canExecute = null) : ICommand
{
    private readonly Func<Task> _execute = execute;
    private readonly Func<bool> _canExecute = canExecute;

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    public async void Execute(object parameter) => await _execute();

    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
