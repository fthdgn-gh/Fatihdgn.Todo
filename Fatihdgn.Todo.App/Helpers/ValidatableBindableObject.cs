namespace Fatihdgn.Todo.App.Helpers;

public class ValidatableBindableObject<T> : BindableObject, IValidatableObject<T>
{
    public ValidatableBindableObject(T value = default)
    {
        this.value = value;
    }
    private T value;
    public T Value 
    {
        get => value;
        set
        {
            this.value = value;
            OnPropertyChanged();
            UpdateMessage();
        }
    }

    public bool HasMessage => !string.IsNullOrEmpty(message);

    string message;
    public string Message 
    {
        get => message;
        set { this.message = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasMessage)); }
    }

    private bool isFirstValidation = true;
    private void UpdateMessage()
    {
        Message = string.Empty;
        if(isFirstValidation) { isFirstValidation = false; return; }
        foreach (var validator in Validators)
        {
            var result = validator.Validate(Value);
            if (result)
            {
                Message = validator.Message;
                return;
            }
        }
    }

    public ICollection<IObjectValidator<T>> Validators { get; set; } = new List<IObjectValidator<T>>();

}
