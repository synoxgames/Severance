using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionHandiler : MonoBehaviour
{
    public Dictionary<Emotion, NumberType> emotionTable = new Dictionary<Emotion, NumberType>() {
        { Emotion.Scary, NumberType.DREAD },
        { Emotion.Angry, NumberType.MALICE },
        { Emotion.Sad, NumberType.WOE },
        { Emotion.Happy, NumberType.FROLIC }
    };

}

public enum Emotion {
    Scary,
    Angry,
    Sad,
    Happy
}
