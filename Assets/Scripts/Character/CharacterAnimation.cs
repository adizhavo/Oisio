using UnityEngine;
using System.Collections;

public class CharacterAnimation
{
    public static readonly string RunKey = "run";

    protected Animator characterAnim;

    public CharacterAnimation(Animator characterAnim)
    {
        this.characterAnim = characterAnim;
    }

    public virtual void SetRun(float speed)
    {
        characterAnim.SetFloat(RunKey, speed);
    }
}
