﻿using System;

namespace ExUnity
{
    public class Singleton<T> where T : class
    {
        #region Static Readonly

        private static readonly object LockObj = new object();

        #endregion

        #region Fields

        private static T _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Returns instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    CreateInstance();
                return _instance;
            }
        }

        #endregion

        #region Create Instance

        /// <summary>
        /// Creates instance.
        /// </summary>
        private static void CreateInstance()
        {
            lock (LockObj)
            {
                if (_instance == null)
                {
                    var type = typeof(T);
                    var ctors = type.GetConstructors();
                    if (ctors.Length > 0)
                    {
                        throw new InvalidOperationException(type.Name +
                            " has at least one accessible ctor making it impossible to enforce singleton behavior.");
                    }

                    _instance = (T) Activator.CreateInstance(type, nonPublic: true);
                }
            }
        }

        #endregion
    }
}