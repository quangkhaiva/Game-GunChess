using UnityEngine;

[System.Serializable]
public class StaticBase
{
    //Player
    public int PlayerHP = 20;
    public int PlayerArmor = 0;
    public float PlayerMoveSpeed = 2f;
    public float DashSpeed = 1f;

    //Gun
    public float BulletSpeed = 6f;
    public int DamBullet = 5;

    //Pawn
    public int PawnHP = 15;
    public int PawnArmor = 0;
    public float PawnMoveSpeed = 1f;
    public int PawnDam = 3;
    public int PawnCoin = 1;

    //Bishop
    public int BishopHP = 20;
    public int BishopArmor = 0;
    public float BishopMoveSpeed = 1f;
    public int BishopDam = 4;
    public float BishopBulletSpeed = 4f;
    public int BishopCoin = 3;
    //Knight
    public int KnightHP = 20;
    public int KnightArmor = 0;
    public float KnightMoveSpeed = 1f;
    public int KnightDam = 4;
    public int KnightCoin = 3;

    //Rook
    public int RookHP = 25;
    public int RookArmor = 1;
    public float RookMoveSpeed = 1.25f;
    public int RookDam = 5;
    public float RockSafeTimeDuration = 0.5f;
    public int RookCoin = 5;

    //Queen
    public int QueenHP = 50;
    public int QueenArmor = 2;
    public float QueenMoveSpeed = 1.5f;
    public int QueenDam = 10;
    public int QueenCoin = 10;
}
