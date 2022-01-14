using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameSystem.Utils
{
    public class MoveCommandProviderTypeHelper
    {
        static string[] _movementNames = new string[0];
        public static string[] FindMoveCommandProviderTypes()
        {
            if (_movementNames.Length == 0)
                _movementNames = InternalFindMoveCommandProviderTypes();

            return _movementNames;
        }
        static string[] InternalFindMoveCommandProviderTypes()
        {
            var types = new List<string>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<MoveCommandProviderAttribute>();
                    if (attribute != null)
                    {
                        types.Add(attribute.Name);
                    }
                }
            }

            return types.ToArray();
        }
    }
}
