using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class TetrisModel : ITetrisModel
    {
        public int NextMoveTryes = 0;
        private float ItemSize = 0;

        public ArrayList GameShapes = new ArrayList();
        public GridVO[,] GridList = new GridVO[10, 10];
        private List<GameObject> ShapesInSlots = new List<GameObject>();

        public List<List<GameObject>> LinesToRemove { get; set; }
        public bool NextMoveIsPossible { get; set; }

     
        /*
         * Loads shapes prefabs
         * */
        public void CreateGameShapes()
        {

            UnityEngine.Object ShapeI = Resources.Load("Shapes/I");
            if (ShapeI)
            {
                GameShapes.Add(ShapeI);
            }

            UnityEngine.Object ShapeJ = Resources.Load("Shapes/J");
            if (ShapeJ)
            {
                 GameShapes.Add(ShapeJ);
            }

            UnityEngine.Object ShapeL = Resources.Load("Shapes/L");
            if (ShapeL)
            {
                GameShapes.Add(ShapeL);
            }

            UnityEngine.Object ShapeO = Resources.Load("Shapes/O");
            if (ShapeO)
            {
                GameShapes.Add(ShapeO);
            }

            UnityEngine.Object ShapeS = Resources.Load("Shapes/S");
            if (ShapeS)
            {
                GameShapes.Add(ShapeS);
            }

            UnityEngine.Object ShapeT = Resources.Load("Shapes/T");
            if (ShapeT)
            {
                GameShapes.Add(ShapeT);
            }

            UnityEngine.Object ShapeZ = Resources.Load("Shapes/Z");
            if (ShapeZ)
            {
                GameShapes.Add(ShapeZ);
            }


        }

        /*
         * Selects random shape
         * */
        public GameObject SelectRandomShape()
        {
            int Num = (int)UnityEngine.Random.Range(0, GameShapes.Count-1);//2; //
            ShapesInSlots.Add(GameShapes[Num] as GameObject);
            return GameShapes[Num] as GameObject;
        }

        // checks if user can place the new shape to the selected place
       public bool IsNewShapeCanBeAdded(List<ItemViewVO> listShapeItems) {

            bool CanBeAdded = false;
            int counter = 0;
            foreach (ItemViewVO item in listShapeItems)
            {
                if (CanBeAdded = CheckItemOnGrid(item))
                {
                    counter++;
                }
            }

            if (counter == listShapeItems.Count)
            {
                AddItemToGrid(listShapeItems);
                return true;
            }

           // ShapesInSlots.Remove()

            return false;
        }

        /**
         * checks if Shape can be added to grid
         */
        private bool CheckItemOnGrid(ItemViewVO item)
        {
            try
            {
                if (ItemSize == 0)
                {
                    ItemSize = item.Size;
                }
                int PlaceX = Mathf.RoundToInt(item.Pos.x / ItemSize);
                int PlaceY = Mathf.RoundToInt(item.Pos.y / ItemSize);

                if (!(GridList.GetValue(PlaceX, PlaceY) as GridVO).hasItem)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch
            {
                return false;
            }
        }

        /**
         * adds user item to grid
         */
        private void AddItemToGrid(List<ItemViewVO> listShapeItems)
        {
            foreach (ItemViewVO item in listShapeItems)
            { 
                int PlaceX = Mathf.RoundToInt(item.Pos.x / item.Size);
                int PlaceY = Mathf.RoundToInt(item.Pos.y / item.Size);
                (GridList.GetValue(PlaceX, PlaceY) as GridVO).Item = item.gameObject;
                (GridList.GetValue(PlaceX, PlaceY) as GridVO).hasItem = true;
            }
        }

        /**
         * checks all added items and finds out if any can be removed
         */
        public List<List<GameObject>> GetItemsToRemove()
        {
            LinesToRemove = new List<List<GameObject>>();

            // check horysontal lines
            for (int j = 0; j < 10; j++)
            {
                List<GameObject> Line = new List<GameObject>();
                for (int i = 0; i < 10; i++)
                {
                    if ((GridList[i, j] as GridVO).hasItem)
                    {
                        Line.Add((GridList[i, j] as GridVO).Item);
                    }
                }

                if (Line.Count == 10)
                {
                    LinesToRemove.Add(Line);

                    for (int i = 0; i < 10; i++)
                    {
                        (GridList[i, j] as GridVO).hasItem = false;
                    }
                }
            }

            //check vertical lines

            for (int j = 0; j < 10; j++)
            {
                List<GameObject> Line = new List<GameObject>();
                for (int i = 0; i < 10; i++)
                {
                    if ((GridList[j, i] as GridVO).hasItem)
                    {
                        Line.Add((GridList[j, i] as GridVO).Item);
                    }
                }

                if (Line.Count == 10)
                {
                    LinesToRemove.Add(Line);
                    for (int i = 0; i < 10; i++)
                    {
                        (GridList[j, i] as GridVO).hasItem = false;
                    }
                }
            }

            return LinesToRemove;
        }

        /**
         * creates game grid
         */
        public void CreateGrid() {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    GridVO gridVO = new GridVO();
                    gridVO.IndexX = j;
                    gridVO.InxexY = i;
                    GridList[j, i] = gridVO;
                }
            }

        }

        /*
         * increase counter Steps when Shape is added to grid 
         * shecks if game slots needs to be updated
         */
        public bool CheckIsSlotsFree(GameObject Shape)
        {
            for(int i = 0; i <  ShapesInSlots.Count; i++)
            {
                char ds = ShapesInSlots[i].gameObject.name.ToCharArray()[0];
                if (Shape.gameObject.name.ToCharArray()[0] == ds)
                {
                    ShapesInSlots.RemoveAt(i);
                    break;
                }
            }

            if (ShapesInSlots.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /*
         проверяем возможность добавления фигуры на сетку
          1 - идем по сетке, если клетка занята - не проверяем ее
          2 - если клетка свободна, делаем список с элементами Фигуры, и ставим им координаты такие,
          какие они были бы если бы Фигуру переместили в данную клетку
          3 - проверяем, может ли быть Фигура поставлена на поле

           достаточно проверить одну Фигуру, если можно поставить - на этом проверку можно окончить

        **/
        //public bool IsNextMovePossible(GameObject shape)
         public bool IsNextMovePossible()
        {
            // check all shapes in slots
            foreach(GameObject shape in ShapesInSlots)
            {
                // run grid items
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        GridVO gridVO = GridList[j, i] as GridVO;

                        if (!gridVO.hasItem)
                        {
                            int count = 0;
                            int itemsCount = shape.transform.childCount;

                            for (int k = 0; k < itemsCount; k++)
                            {
                                ItemViewVO item = shape.transform.GetChild(k).GetComponent<ItemViewVO>();
                                if (item)
                                {
                                    int PosX = (int)(gridVO.IndexX + item.gameObject.transform.localPosition.x);
                                    int PosY = (int)(gridVO.InxexY + item.gameObject.transform.localPosition.y);

                                    try
                                    {
                                        if (!(GridList.GetValue(PosX, PosY) as GridVO).hasItem)
                                            count++;
                                    }
                                    catch { }
                                }

                                if (count == itemsCount)
                                {
                                    NextMoveIsPossible = true;
                                    return NextMoveIsPossible;
                                }
                            }
                        }
                    }

                }


            }
           
            NextMoveIsPossible = false;
            return NextMoveIsPossible;
        }
    }
}

