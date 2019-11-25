using System.Collections.Generic;

namespace ZigZag.GameCore
{
    public abstract class BaseObjectsPool<ObjectType, ObjectKey>
        where ObjectType: class
    {
        private IDictionary<ObjectKey, IList<ObjectType>> _gamePool;

        public BaseObjectsPool()
        {
            _gamePool = new Dictionary<ObjectKey, IList<ObjectType>>();
        }

        public virtual ObjectType GetObjectInPool(ObjectKey key)
        {
            ObjectType poolObject;
            IList<ObjectType> currentObjectsList;

            if(!_gamePool.TryGetValue(key, out currentObjectsList))
            {
                poolObject = CreateNewObject(key);
            }
            else
            {
                if(currentObjectsList.Count > 0)
                {
                    poolObject = currentObjectsList[0];
                    currentObjectsList.Remove(poolObject);
                }
                else
                {
                    poolObject = CreateNewObject(key);
                }
            }
            ActiveObjectInScene(poolObject);
            return poolObject;
        }

        public abstract ObjectType CreateNewObject(ObjectKey key);
        public abstract void ActiveObjectInScene(ObjectType poolObject);

        public virtual void ReturnObjectInPool(ObjectKey key, ObjectType poolObject)
        {
            IList<ObjectType> currentObjectsList;

            if(!_gamePool.TryGetValue(key, out currentObjectsList))
            {
                currentObjectsList = new List<ObjectType>();
                currentObjectsList.Add(poolObject);
                _gamePool.Add(key, currentObjectsList);
            }
            else
            {
                if (currentObjectsList.Contains(poolObject)) return;
                currentObjectsList.Add(poolObject);
            }
            HideObjectInScene(poolObject);
        }

        public abstract void HideObjectInScene(ObjectType poolObject);
        
    }
}
