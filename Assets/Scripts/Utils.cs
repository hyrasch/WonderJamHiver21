﻿using UnityEngine;

public static class Utils
{
    public static readonly int Grounded = Animator.StringToHash("Grounded");
    public static readonly int Jump = Animator.StringToHash("Jump");
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int FallSpeed = Animator.StringToHash("Fall Speed");
    public static readonly int IsJumping = Animator.StringToHash("IsJumping");
    public static readonly int IsFalling = Animator.StringToHash("IsFalling");
}