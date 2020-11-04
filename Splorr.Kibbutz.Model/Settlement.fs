namespace Splorr.Kibbutz.Model

type TurnCounter = uint64

type Settlement = 
    {
        turnCounter : TurnCounter
        vowels : Map<string, float>
        consonants : Map<string, float>
        nameLengthGenerator : Map<int, float>
        nameStartGenerator : Map<bool, float>
    }