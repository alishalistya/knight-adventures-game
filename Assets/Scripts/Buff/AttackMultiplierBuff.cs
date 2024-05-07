public class AttackMultiplierBuff
{
    private float _value;
    private string _name;

    public float Value => _value;
    
    public string Name => _name;

    public AttackMultiplierBuff(string name, float value)
    {
        _value = value;
        _name = name;
    }
}
