
namespace Fatihdgn.Todo.App.Behaviors;

public class CheckboxCommandBehavior : Behavior<CheckBox>
{
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(
            nameof(Command),
            typeof(Command),
            typeof(CheckboxCommandBehavior),
            null);

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(CheckboxCommandBehavior),
            null);

    public Command Command
    {
        get => (Command)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    protected override void OnAttachedTo(CheckBox bindable)
    {
        base.OnAttachedTo(bindable);
        bindable.CheckedChanged += OnCheckedChanged;
    }

    protected override void OnDetachingFrom(CheckBox bindable)
    {
        base.OnDetachingFrom(bindable);
        bindable.CheckedChanged -= OnCheckedChanged;
    }

    private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (Command != null && Command.CanExecute(CommandParameter))
            Command.Execute(CommandParameter);
    }
}
