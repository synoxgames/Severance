using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionHandiler
{
    public static Dictionary<Emotion, NumberType> emotionTable = new Dictionary<Emotion, NumberType>() {
        { Emotion.Scary, NumberType.DREAD },
        { Emotion.Angry, NumberType.MALICE },
        { Emotion.Sad, NumberType.WOE },
        { Emotion.Happy, NumberType.FROLIC }
    };


    public static Emotion GetEmotion(float value) {
        if (value == 1) return Emotion.Null;
        Emotion returnEmotion = (Emotion)(value * 3);
        return returnEmotion;
    }
    // enemy= (Enemy)Random.Range(0, System.Enum.GetValues(typeof(Enemy)).Length);
}

public enum Emotion {
    Scary,
    Angry,
    Sad,
    Happy,
    Null
}

