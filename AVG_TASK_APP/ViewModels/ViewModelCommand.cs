using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AVG_TASK_APP.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        // Fields 
        private readonly Action<object> _excuteAction;
        private readonly Predicate<object> _canExcuteAction;

        // constructor 
        public ViewModelCommand(Action<object> executeAction)
        {
            _excuteAction = executeAction;
            _canExcuteAction = null;
        }

        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _excuteAction = executeAction;
            _canExcuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExcuteAction == null ? true : _canExcuteAction(parameter);
        }

        public void Execute(object? parameter)
        {
            _excuteAction(parameter);
        }
    }
}
