using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace BetterCLRHost.Tests
{
    [TestFixture]
    public class VerifyCompatibility
    {
        private IEnumerable<Type> origTypes;
        private IEnumerable<Type> bttrTypes;

        [TestFixtureSetUp]
        public void Init()
        {
            //figure out paths, can't reference this stuff directly
            var origCLRHostPath = Path.Combine(
                Environment.CurrentDirectory,
                @"..\..\..\OriginalCLRHost",
                @"CHP-1.0-3-g4e483c5-" + (Environment.Is64BitProcess ? "x64" : "x86")
            );
            var bttrCLRHostPath = Path.Combine(
                Environment.CurrentDirectory,
                @"..\..\..\..\BetterCLRHost.Interop\bin\",
                (Environment.Is64BitProcess ? "x64" : "x86"),
#if DEBUG
                @"Debug"
#else
                @"Release"
#endif
            );

            //handle loading reflection-only dependencies
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, a) => {
                try
                {
                    return Assembly.ReflectionOnlyLoad(a.Name);
                }
                catch
                {
                    return Assembly.ReflectionOnlyLoadFrom(bttrCLRHostPath + @"\CLRHostPlugin.dll");
                }
            };

            //load assemblies in reflection-only mode and get the CLROBS namespace types
            origTypes = Assembly.ReflectionOnlyLoadFrom(origCLRHostPath + @"\CLRHost.Interop.dll").GetExportedTypes().Where( x => x.Namespace == "CLROBS" );
            bttrTypes = Assembly.ReflectionOnlyLoadFrom(bttrCLRHostPath + @"\CLRHost.Interop.dll").GetExportedTypes().Where( x => x.Namespace == "CLROBS" );
        }

        [Test]
        public void VerifyInterfaces()
        {
            //Need to do it this way because http://stackoverflow.com/a/1522698/489071
            Func<IEnumerable<Type>, Func<Type, bool>, IEnumerable<dynamic>> crappyGetMembers = (collection, predicate) => {
                return collection.Where( x=> predicate(x) )
                    .SelectMany( x => x.GetMembers()
                        .Where( y => 
                            (y is MethodBase && ((y as MethodBase).IsFamily || (y as MethodBase).IsPublic)) ||
                            (y is FieldInfo && ((y as FieldInfo).IsFamily  || (y as FieldInfo).IsPublic)) ||
                            (y is PropertyInfo) || (y is EventInfo)
                        )
                        .Select( y => Tuple.Create(x.FullName, y.ToString(), attributes(y)) ));
            };

            //compare interface names
            {
                Func<IEnumerable<Type>, IEnumerable<string>> func = t =>
                    t.Where( x => x.IsInterface )
                    .Select( x => x.FullName );

                var origInterfaceNames = func(origTypes);
                var bttrInterfaceNames = func(bttrTypes);
                var missing = origInterfaceNames.Except(bttrInterfaceNames).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify interface names\n" + String.Join("\n", missing));
            }

            //compare interface members
            {
                var origInterfaceMembers = crappyGetMembers(origTypes, x => x.IsInterface);
                var bttrInterfaceMembers = crappyGetMembers(bttrTypes, x => x.IsInterface);
                var missing = origInterfaceMembers.Except(bttrInterfaceMembers).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify interface members\n" + String.Join("\n", missing));
            }
        }
        
        [Test]
        public void VerifyAbstracts()
        {
            //compare abstract names
            {
                Func<IEnumerable<Type>, IEnumerable<string>> func = t =>
                    t.Where( x => x.IsAbstract && x.IsClass )
                    .Select( x => x.FullName );

                var origAbstractNames = func(origTypes);
                var bttrAbstractNames = func(bttrTypes);
                var missing = origAbstractNames.Except(bttrAbstractNames).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify abstract names\n" + String.Join("\n", missing));
            }

            //compare abstract members
            {
                var origInterfaceMembers = getMembers(origTypes, x => x.IsAbstract && x.IsClass);
                var bttrInterfaceMembers = getMembers(bttrTypes, x => x.IsAbstract && x.IsClass);
                var missing = origInterfaceMembers.Except(bttrInterfaceMembers).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify abstract members\n" + String.Join("\n", missing));
            }
        }

        [Test]
        public void VerifyConcretes()
        {
            //compare concrete names
            {
                Func<IEnumerable<Type>, IEnumerable<string>> func = t =>
                    t.Where( x => !x.IsAbstract && x.IsClass )
                    .Select( x => x.FullName );

                var origAbstractNames = func(origTypes);
                var bttrAbstractNames = func(bttrTypes);
                var missing = origAbstractNames.Except(bttrAbstractNames).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify concrete names\n" + String.Join("\n", missing));
            }

            //compare concrete members
            {
                var origInterfaceMembers = getMembers(origTypes, x => !x.IsAbstract && x.IsClass);
                var bttrInterfaceMembers = getMembers(bttrTypes, x => !x.IsAbstract && x.IsClass);
                var missing = origInterfaceMembers.Except(bttrInterfaceMembers).ToList();

                Assert.IsFalse(missing.Any(), "Failed to verify concrete members\n" + String.Join("\n", missing));
            }
        }

        private IEnumerable<dynamic> getMembers(IEnumerable<Type> collection, Func<Type, bool> predicate)
        {
            return collection.Where( x=> predicate(x) )
                .SelectMany( x => x.BetterGetMembers()
                    .Where( y => 
                        (y is MethodBase && ((y as MethodBase).IsFamily || (y as MethodBase).IsPublic)) ||
                        (y is FieldInfo && ((y as FieldInfo).IsFamily  || (y as FieldInfo).IsPublic)) ||
                        (y is PropertyInfo) || (y is EventInfo)
                    )
                    .Select( y => Tuple.Create(x.FullName, y.ToString(), attributes(y)) ));
        }

        private string attributes(MemberInfo memberInfo)
        {
            //PrivateScope == 0, so it'll always show up
            if( memberInfo is MethodBase )
                return ((memberInfo as MethodBase).Attributes & (MethodAttributes.Abstract |MethodAttributes.Family | MethodAttributes.FamORAssem | MethodAttributes.Final | MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.Virtual)).ToString();

            //PrivateScope == 0, so it'll always show up
            if( memberInfo is FieldInfo )
                return ((memberInfo as FieldInfo).Attributes & (FieldAttributes.Assembly | FieldAttributes.Family | FieldAttributes.Public | FieldAttributes.Static)).ToString();

            //Pretty sure the important parts of these get implemented as methods anyway
            if( memberInfo is PropertyInfo || memberInfo is EventInfo )
                return "";

            throw new NotImplementedException("How the fuck did we get here?");
        }
    }
}
