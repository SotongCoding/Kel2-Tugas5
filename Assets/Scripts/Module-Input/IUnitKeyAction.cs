using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitKeyAction
{
    bool _moveUp { get; }
    bool _moveDown { get; }
    bool _moveLeft { get; }
    bool _moveRight { get; }

    bool _rotateLeft { get; }
    bool _rotateRight { get; }

    bool _shootBullet { get; }
    bool _placeBomb { get; }
}
