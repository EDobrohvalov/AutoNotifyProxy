using System;
using System.ComponentModel;
using NUnit.Framework;

namespace AutoNotifyProxy.Tests
{
    [TestFixture]
    public class Tests
    {
        private NotifyProxyGenerator _generator;

        [SetUp]
        public void Setup()
        {
            _generator = new NotifyProxyGenerator();
        }

        [Test]
        public void New_ConcreteClass_Object_IsNotified()
        {
            var notifiableFoo = _generator.Make<Foo>();

            Assert.That(notifiableFoo, Is.InstanceOf<INotifyPropertyChanged>());
        }

        [Test]
        public void Passed_Argument_Setted_To_Property()
        {
            var notifiableFoo = _generator.Make<FooArgument>(ctorArgs:"value");
            Assert.AreEqual("value", notifiableFoo.Value);
        }

        [Test]
        public void FireChanged_WhenVirtualPropertySet()
        {
            var wasCalled = false;
            var foo = _generator.Make<Foo>();
            
            ((INotifyPropertyChanged) foo).PropertyChanged += (sender, args) => { wasCalled = true;};
            
            foo.Value = "value";
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void FireChanged_WhenVirtualPropertySet_OnAbstractClass()
        {
            var wasCalled = false;
            var foo = _generator.Make<AbsFoo>();

            ((INotifyPropertyChanged)foo).PropertyChanged += (sender, args) => { wasCalled = true; };

            foo.Value = "value";
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void NotFireChanged_WhenVirtualPropertySet_WithChangeOnlyOption()
        {
            var wasCalled = false;
            var foo = _generator.Make<Foo>();
            foo.Value = "value";
            ((INotifyPropertyChanged) foo).PropertyChanged += (sender, args) => { wasCalled = true;};
            foo.Value = "value";
            
            Assert.IsFalse(wasCalled);
        }
        
        [Test]
        public void FireChanged_WhenVirtualPropertySet_WithAlwaysOption()
        {
            var wasCalled = false;
            var foo = _generator.Make<Foo>(NotifyMode.Always);
            foo.Value = "value";
            ((INotifyPropertyChanged) foo).PropertyChanged += (sender, args) => { wasCalled = true;};
            foo.Value = "value";
            
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ShouldNotFireChangedWhenNonVirtualPropertyChangedOnMadeObject()
        {
            var wasCalled = false;
            var foo = _generator.Make<Foo>();
            
            ((INotifyPropertyChanged) foo).PropertyChanged += (sender, args) => { wasCalled = true;};
            
            foo.NonVirtualValue = "value";
            Assert.IsFalse(wasCalled);
        }

        public class FooArgument
        {
            public FooArgument(string arg)
            {
                Value = arg;
            }
            public virtual string Value { get; set; }
        }


        public class Foo
        {
            public virtual string Value { get; set; }
            public string NonVirtualValue { get; set; }
        }

        public abstract class AbsFoo
        {
            public virtual string Value { get; set; }
            public string NonVirtualValue { get; set; }
        }

        public interface IFoo
        {
            string Value { get; set; }
        }
    }
}