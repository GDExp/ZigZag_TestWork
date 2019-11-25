using System.Collections.Generic;
using ZigZag.GameCore.GameInterface;

namespace ZigZag.GameCore
{
    class Observer: ISubject
    {
        private IList<IObserver> _observers;

        public Observer()
        {
            _observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            if (_observers.Contains(observer)) return;
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            if (!_observers.Contains(observer)) return;
            _observers.Remove(observer);
        }

        public void Notify()
        {
            for (int i = 0; i < _observers.Count; ++i)
            {
                _observers[i].UpdateObserver();
            }
        }
    }
}
