namespace Splorr.Kibbutz.Model

type Settlement = 
    {
        turnCounter : uint64
        vowels : string list
        consonants : string list
        nameLengthGenerator : Map<int, float>
        nameStartGenerator : Map<bool, float>
    }