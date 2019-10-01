using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleDartboard.PAL.Core
{
    public class Mediator
    {
        static IDictionary<MessageType, List<Action<object>>> _actionDictionary =
            new Dictionary<MessageType, List<Action<object>>>();

        public static void Register(MessageType messageType, Action<object> callback)
        {
            if (!_actionDictionary.ContainsKey(messageType))
            {
                var actions = new List<Action<object>>();
                actions.Add(callback);
                _actionDictionary.Add(messageType, actions);
                return;
            }

            if (!_actionDictionary[messageType].Exists(x => x.Method.ToString() == callback.Method.ToString()))
            {
                _actionDictionary[messageType].Add(callback);
            }
            else
            {
                Trace.WriteLine("Die Methode "+ callback.Method.ToString()+" ist bereits registriert!");
            }
        }

        public static void Unregister(MessageType messageType, Action<object> callback)
        {
            if (_actionDictionary.ContainsKey(messageType))
                _actionDictionary[messageType].Remove(callback);
        }

        public static void NotifyColleagues(MessageType messageType, object argument)
        {
            if (!_actionDictionary.ContainsKey(messageType)) return;
            foreach (var callback in _actionDictionary[messageType])
                callback(argument);
        }
    }
}