namespace Splorr.Kibbutz.Model

type Settlement = 
    {
        turnCounter : uint64
        vowels : Map<string, float>
        consonants : Map<string, float>
        nameLengthGenerator : Map<int, float>
        nameStartGenerator : Map<bool, float>
    }