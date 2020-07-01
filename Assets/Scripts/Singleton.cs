﻿using UnityEngine;

namespace Utilites
{

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public const string PARENT_NAME = "[Singleton]";
        protected static GameObject _parent = null;
        public static GameObject parent
        {
            get
            {
                if (_parent == null) _parent = GameObject.Find(PARENT_NAME) ?? new GameObject(PARENT_NAME);
                return _parent;
            }
        }

        protected static T _instance = null;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Log("Can't find " + typeof(T).Name+ " in scene! Creating new...");

                        _instance = new GameObject(PARENT_NAME + " " + typeof(T).Name).AddComponent<T>();
                        if (_instance.isLoadFromPrefab)
                        {
                            Log("Loading from prefab (path = "+_instance.GetPrefabPath+") ");

                            var _prefab = Resources.Load(_instance.GetPrefabPath);
                            if (_prefab != null)
                            {
                                DestroyImmediate(_instance.gameObject);
                                var _go = Instantiate(_prefab, parent.transform) as GameObject;
                                _go.name = _prefab.name;
                                _instance = _go.GetComponentInChildren<T>();

                                if (_instance == null)
                                {
                                    Log("Error! Can't find " + typeof(T).Name + " instance in prefab");
                                }
                            }
                            else
                            {
                                Log("Error! Can't find prefab by path = " + _instance.GetPrefabPath);
                            }

                        }
                    }
                }

                return _instance;
            }
        }

        public virtual string GetPrefabPath { get; }
        protected virtual bool isLoadFromPrefab { get { return false; } }
        protected virtual bool isDontDestroy { get { return false; } }

        protected virtual void Awake()
        {
            _instance = CheckInstance(_instance) as T;

            if (_instance.isDontDestroy) DontDestroyOnLoad(_instance.transform.root.gameObject);
        }

        protected virtual Singleton<T> CheckInstance(Singleton<T> _instanceCheck)
        {
            if (_instanceCheck != null && _instanceCheck != this)
            {
                Log("Destroing duplicate of type: " + this.GetType().FullName);
                DestroyImmediate(this);
                return _instanceCheck;
            }
            return this;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        protected static void Log(string message)
        {
            Debug.Log(PARENT_NAME + " " + message);
        }
    }


    public abstract class SingletonPersistent<T> : Singleton<T> where T : SingletonPersistent<T>
    {
        protected override bool isDontDestroy { get { return true; } }
    }

    public abstract class SingletonSelfCreator<T> : Singleton<T> where T : SingletonSelfCreator<T>
    {
        protected abstract string prefabPath { get; }
        public override string GetPrefabPath { get { return prefabPath; } }
        protected override bool isLoadFromPrefab { get { return true; } }
        protected override bool isDontDestroy { get { return true; } }
    }

}
