namespace AniMate_app.Utils
{
    public class Command<T>(T paremeter, Action<T> action, Func<T, bool> checkFunc)
    {
        private T Paremeter { get; } = paremeter;

        private readonly Action<T> _action = action;

        private readonly Func<T, bool> _checkFunc = checkFunc;

        public bool CanExecute() => _checkFunc.Invoke(Paremeter);

        public void Execute() => _action.Invoke(Paremeter);
    }

    class CommandCollection<T>
    {
        private Queue<Command<T>> _commands = new();

        public int CommandCount => _commands.Count;

        private Task _task;

        private CancellationTokenSource _cts = null;

        private void StartExecuting()
        {
            _task = Task.Factory.StartNew(() =>
            {
                while(_commands.Count > 0)
                {
                    var command = _commands.Dequeue();

                    while (!command.CanExecute()) 
                    {
                        if (_cts.Token.IsCancellationRequested)
                            return;
                    }

                    if (_cts.Token.IsCancellationRequested)
                        return;

                    command.Execute();
                }
            });
        }

        public void Add(Command<T> command)
        {
            _commands.Enqueue(command);

            if(_task is null || _task.IsCompleted)
            {
                if (_cts is not null && !_cts.IsCancellationRequested)
                    _cts.TryReset();
                    
                else
                    _cts = new CancellationTokenSource();

                StartExecuting();
            }       
        }

        public void Clear()
        {
            _commands.Clear();

            if(_task is not null)
                _cts?.Cancel();
        }
    }
}
