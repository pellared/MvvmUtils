# Project Description
MVVM Utils is a set of classes which extends existing MVVM Frameworks such as MVVM Light, Prism, Cinch. Only WPF supported.

## Features
* **Validation** - cutomizable error and validation management by using ErrorContainer, DataErrorInfoProvider and ValidationProvider
* **DialogService** and **WindowService** - UI interaction code without any reference to the View layer
* **UiDispatcher** - UI dispatcher using SynchronizationContext
* **ConcurrentObservableCollection** - dispatched thread-safe observable collection
* **PropetyObserver** - strongly typed property observer on INotifyPropertyChanged

## Source description
* **MvvmLightSample** - a little WPF MvvmLight application which uses MvvmUtils main features
* **MvvmUtils**
  * **Pellared.Common** - small, but universal utlility library (you can check the code by your own)
  * **MvvmUtils** - **MVVM Utils library**
  * **SalaryBook** - a sample application where I test the MVVM Utils (uses MVVM Light ,but with ViewModel first approach, and FluentValidation)

## Resources
* Docs: https://mvvmutils.codeplex.com/documentation
* NuGet: http://www.nuget.org/packages/Pellared.MvvmUtils
* GitHub: http://github.com/Pellared/MvvmUtils

# License
The MIT License (MIT)
Copyright (c) 2013 Robert Pajak

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

# Contact
Robert Pająk<br>
pellared@hotmail.com<br>
http://rpajak.com/