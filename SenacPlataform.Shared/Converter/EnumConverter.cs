namespace SenacPlataform.Shared.Converter;
public static class EnumConverter
{
    public static IEnumerable<TEnum> SNToList<TEnum>() where TEnum : Enum
    {
        //Enum.GetValues(typeof(CARGO)).Cast<CARGO>();
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
    }
}
