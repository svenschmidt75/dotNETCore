using System;

namespace MazeSolver
{
    public class DisposableHelper : IDisposable
    {
        private readonly Action _disposeAction;

        public DisposableHelper(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction();
        }
    }
}