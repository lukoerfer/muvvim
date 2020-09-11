# MuVViM
Utilities for Windows Presentation Framework (WPF) and the Model-View-ViewModel (MVVM) pattern

## Motivation
Working with the Windows Presentation Framework (WPF) and the Model-View-ViewModel (MVVM) pattern in particular often requires writing boilerplate or unnecessary verbose code both in the XAML views and the C# view models. In addition, some features required from time to time are not provided out of the box. This library is a very individual collection of utilities and extensions that may be helpful when working with WPF and MVVM. Some components originate from various former projects, others arised out of sudden inspiration. Therefore, this library does not aim to serve a single purpose, but as a toolbox for future WPF projects.

## Installation


## Usage
The library contains a lot of different components and is partitioned into the following namespaces:

* `Binding` - Functionality regarding WPF bindings
* `Command` - Implementations of the `ICommand` interface
* `Converter` - Type-safe converte templates as well as various predefined converters
* `Dialogs` - Classic WPF dialogs ported to the MVVM pattern
* `Extensions` - XAML extensions (attached properties, behaviors, ...)
* `PropertyChanged` - Functionality regarding the `PropertyChanged` event
* `ViewModel` - Template and handling for view models

Check out [the wiki](https://github.com/lukoerfer/muvvim/wiki) for detailed descriptions of the different components.

## License
The software is licensed under the [MIT license](https://github.com/lukoerfer/muvvim/blob/master/LICENSE).
