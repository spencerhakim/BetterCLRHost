using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BetterCLRHost.Tests
{
    internal static class FixShittyInterfaceReflection
    {
        public static MemberInfo[] BetterGetMembers(this Type type)
        {
            var bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;

            if( !type.IsInterface )
                return type.GetMembers(bindingFlags);

            var memberInfos = new List<MemberInfo>();
            var considered = new List<Type>();
            var queue = new Queue<Type>();

            considered.Add(type);
            queue.Enqueue(type);

            while( queue.Count > 0 )
            {
                var subType = queue.Dequeue();
                foreach( var subInterface in subType.GetInterfaces() )
                {
                    if( considered.Contains(subInterface) )
                        continue;

                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }

                var typeMembers = subType.GetMembers(bindingFlags);
                var newMemberInfo = typeMembers.Where( x => !memberInfos.Contains(x) );

                memberInfos.InsertRange(0, newMemberInfo);
            }

            return memberInfos.ToArray();
        }
    }
}
