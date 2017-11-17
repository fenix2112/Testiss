using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public interface ITetrisModel
    {
        List<List<GameObject>> LinesToRemove { get; set; }
        bool NextMoveIsPossible { get; set; }

        void CreateGrid();
        void CreateGameShapes();
       // bool IsNextMovePossible(GameObject shape);
        bool IsNextMovePossible();
        bool CheckIsSlotsFree(GameObject Shape);
        bool IsNewShapeCanBeAdded(List<ItemViewVO> listShapeItems);

        List<List<GameObject>> GetItemsToRemove();
        GameObject SelectRandomShape();

    }
}
