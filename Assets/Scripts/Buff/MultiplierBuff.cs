public class MultiplierBuff
{
    private float _value;
    private string _name;

    public float Value
    {
        get
        {
            if (IsActive)
            {
                return _value;
            }

            return 0f;
        }
    }
    
    public string Name => _name;

    public bool IsActive { get; set; } = true;

    public MultiplierBuff(string name, float value)
    {
        _value = value;
        _name = name;
    }
}