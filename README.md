# Pellared.MvvmUtils

> MVVM Utils is a set of classes which extends existing MVVM Frameworks such as MVVM Light, Prism, Cinch. Only WPF supported.

[![Nuget](https://img.shields.io/nuget/v/Pellared.MvvmUtils)](https://www.nuget.org/packages/Pellared.MvvmUtils)

## Source code description

* **MvvmLightSample** - a little WPF MvvmLight application which uses MvvmUtils main features
* **MvvmUtils**
  * **Pellared.Common** - small, but universal utlility library (you can check the code by your own)
  * **MvvmUtils** - **MVVM Utils library**
  * **SalaryBook** - a sample application where I test the MVVM Utils (uses MVVM Light, but with ViewModel first approach and FluentValidation)

## Features

* [**Validation**](#validation) - cutomizable error and validation management by using ErrorContainer, DataErrorInfoProvider and ValidationProvider
* **DialogService** and [**WindowService**](#windowservice) - UI interaction code without any reference to the View layer
* **UiDispatcher** - UI dispatcher using SynchronizationContext
* **ConcurrentObservableCollection** - dispatched thread-safe observable collection
* **PropetyObserver** - strongly typed property observer on INotifyPropertyChanged

### Validation 

MVVM Utils provides a set of classes that helps implementing validation.

* **ErrorContainer** stores property errors
* **DataErrorInfoProvider** exposes errors taken from IErrorContainer via IDataErrorInfo and INotifyDataErrorInfo
By combining these classes you can easily create your own ValidatiableViewModel that can support your requirements.

Below there is a sample ValidatiableViewModel inherting MVVM Light's ViewModelBase, which fires validation after invoking RaisePropertyChanged. When the validation sets errors, then the ErrorContainer executes OnErrorsChanged, which also invokes RaisePropertyChanged for notifying the view about the errors.

```csharp
public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
{
	public const string ObjectErrorPropertyName = "";

	private readonly DataErrorInfoProvider dataErrorInfoProvider;

	protected ValidatableViewModel(ArrayFormat errorFormat = ArrayFormat.First)
	{
		ErrorsContainer = new ErrorsContainer();
		ErrorsContainer.ErrorsChanged += OnErrorsChanged;

		dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
	}

	protected ValidatableViewModel(IMessenger messenger, ArrayFormat errorFormat = ArrayFormat.First)
		: base(messenger)
	{
		ErrorsContainer = new ErrorsContainer();
		ErrorsContainer.ErrorsChanged += OnErrorsChanged;

		dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
	}

	protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer, ArrayFormat errorFormat = ArrayFormat.First)
	{
		ErrorsContainer = errorsContainer;
		ErrorsContainer.ErrorsChanged += OnErrorsChanged;

		dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
	}

	public IErrorsContainer<ValidationError> ErrorsContainer { get; private set; }

	public virtual bool HasErrors
	{
		get { return ErrorsContainer.HasErrors; }
	}

	public virtual string Error
	{
		get { return dataErrorInfoProvider.Error; }
	}

	public virtual string this[string columnName](string-columnName)
	{
		get {	return dataErrorInfoProvider[columnName](columnName); }
	}

	public void Validate()
	{
		IEnumerable<ValidationError> errors = Validation();
		ErrorsContainer.ClearAndSetErrors(errors);
	}

	protected abstract IEnumerable<ValidationError> Validation();

	protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
	{
		base.RaisePropertyChanged(propertyExpression);
		Validate();
	}

	protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
	{
		base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
		Validate();
	}

	protected override void RaisePropertyChanged<T>(string propertyName, T oldValue, T newValue, bool broadcast)
	{
		base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
		Validate();
	}

	protected override void RaisePropertyChanged(string propertyName)
	{
		base.RaisePropertyChanged(propertyName);
		Validate();
	}

	private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
	{
		OnErrorsChanged(e.PropertyName);
	}

	private void OnErrorsChanged(string propertyName)
	{
		if (propertyName == ObjectErrorPropertyName)
		{
			// object error
			propertyName = "Error";
		}

		// notify for IDataErrorInfo
		base.RaisePropertyChanged(propertyName);
		base.RaisePropertyChanged("HasErrors");
	}
}
```

And here is a sample ViewModel that supports validation:

```csharp
public sealed class PersonViewModel : ValidatableViewModel
{
	public PersonViewModel()
	{
		// validate on creation
		Validate();
	}
	private string _name;
	public string Name
	{
		get { return _name; }
		set { Set(() => Name, ref _name, value); }
	}
	protected override IEnumerable<ValidationError> Validation()
	{
		if (string.IsNullOrWhiteSpace(Name))
		{
			return new []() { ValidationError.Create(() => Name, "Name cannot be empty") };
		}
		return null;
	}
}
```

### WindowService

To use a custom dialog in a lously coupled way, only a few steps have to be done.

Create a ViewModel for the dialog implementing IWindowViewModel:

```csharp
public class AddedViewModel : IWindowViewModel
{
	public bool Closed { get; set; }

	public string Title
	{
		get { return "Added!"; }
	}

	public string Message
	{
		get { return "A person has been added to the table."; }
	}
}
```

**Remark**: Setting Closed property to true closes the dialog.

Create a View for the dialog as a UserControl:

```xml
<UserControl x:Class="MvvmLightSample.AddedView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:vm="clr-namespace:MvvmLightSample.ViewModel"
             mc:Ignorable="d"
             d:DesignHeight="20" d:DesignWidth="300"
             d:DataContext="{d:DesignInstance vm:AddedViewModel, IsDesignTimeCreatable=True}">
    <Grid>
        <TextBlock Text="{Binding Message}" />
    </Grid>
</UserControl>
```
