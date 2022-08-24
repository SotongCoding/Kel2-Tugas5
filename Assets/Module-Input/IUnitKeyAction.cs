using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitKeyAction
{
    bool MoveUp { get; }
    bool MoveDown { get; }
    bool MoveLeft { get; }
    bool MoveRight { get; }

    bool RotateLeft { get; }
    bool RotateRight { get; }

    bool ShootBullet { get; }
    bool PlaceBomb { get; }
}
