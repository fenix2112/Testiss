using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace Game
{
    public class GameView : View
    {

        public GameObject CellPrefab;
        public Vector2 GridSize = new Vector2(10, 10);
        public GameObject Game;

        // Update is called once per frame
        public void CreateGrig()
        {
            int Width = (int)GridSize.x;
            int Height = (int)GridSize.y;

            for (int i = 0; i < Height; i++)
            {
                float PozY = (CellPrefab.GetComponent<Renderer>().bounds.size.y * i);
                for (int j = 0; j < Width; j++)
                {
                    float PozX = (CellPrefab.GetComponent<Renderer>().bounds.size.x * j);
                    GameObject Cell = Instantiate(CellPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    Cell.transform.parent = Game.transform;
                    Cell.transform.position = new Vector3((PozX + Game.transform.position.x), (PozY + Game.transform.position.y), 0.0f);
                }
            }
        }
    }


}
