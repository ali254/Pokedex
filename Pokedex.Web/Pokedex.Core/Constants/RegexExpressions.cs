namespace Pokedex.Core.Constants
{
    public static class RegexExpressions
    {
        public const string ValidatePokeApiName = @"^[A-Za-z-]+$";

        public const string NoSpecialEscapedCharacters = @"[^a-zA-Z0-9_. ]+";
    }
}
