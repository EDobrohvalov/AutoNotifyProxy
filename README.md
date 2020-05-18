# Getting started
This is a library for dynamic creation a instances of proxy classes with the INotifyPropertyChanged interface implementation

## Basic examples of usages

### Goal
For example your have some POCO class, like this:

``` C#
public class Foo
{
    public string FirstValue{ get; set; }
    public string SecondValue { get; set; }
}
```

And you want handle **set;** methods for proprerties with `INotityPropertyChanged` pattern.

### Steps

#### Mark your properties as `virtual`:

``` C#
public class Foo
{
    public virtual string FirstValue{ get; set; }
    public virtual string SecondValue { get; set; }
}
```

#### Replace call of `ctor` to `NotifyProxyGenerator.Make` method

``` C#
// before
Foo foo = new Foo();
...
// after
Foo foo = NotifyProxyGenerator.Make<Foo>();
```

#### You can get access to `ProperyChanged` event after cast to `INotityPropertyChanged`

``` C#
Foo foo = NotifyProxyGenerator.Make<Foo>();
(foo as INotityPropertyChanged).PropertyChanged += ...
```

Or set object like `DataContext` if your use XAML based UI technology

#### Pass arguments into `ctor` if it need

``` C#
// before
Foo foo = new Foo("arg1", 1.0, 2f);
...
// after
Foo foo = NotifyProxyGenerator.Make<Foo>(ctorArgs:"arg1", 1.0, 2f);
```

#### You could select mode of notifying Always

``` C#
Foo foo = new Foo(NotifyMode.OnlyOnChange);
```
Or
``` C#
Foo foo = new Foo(NotifyMode.Always);
```

#### This lib have a public interface 

``` C#
public interface INotifyProxyGenerator
{
    T Make<T>(NotifyMode mode, params object[] ctorArgs) where T : class;
}
```

and you could use it for register `NotifyProxyGenerator` into your IoC Container.

Example for Castle.Windsor

``` C#
...
Component.For<INotifyProxyGenerator>().ImplementedBy<NotifyProxyGenerator>()
...
```
